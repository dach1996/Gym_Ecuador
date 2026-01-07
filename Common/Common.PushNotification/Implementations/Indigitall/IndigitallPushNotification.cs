using System.Net;
using System.Diagnostics;
using System.Net.Http.Json;
using Common.PushNotification.Configuration;
using Common.PushNotification.Configuration.Indigitall;
using Common.PushNotification.Implementations.Indigitall.Model.Enum;
using Common.PushNotification.Implementations.Indigitall.Model.Request;
using Common.PushNotification.Implementations.Indigitall.Model.Response;
using Common.PushNotification.Model;
using Common.PushNotification.PushNotificationException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.PushNotification.Implementations.Indigitall.Model.Request.Common;
using Common.PushNotification.Model.Request;
namespace Common.PushNotification.Implementations.Indigitall;

public class IndigitallPushNotification : PushNotificationBase, IPushNotification
{
    protected override PushNotificationImplementationNames Implementation => PushNotificationImplementationNames.Indigitall;
    protected readonly IndigitallConfiguration Configuration;
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="webHostEnvironment"></param>
    public IndigitallPushNotification(
        ILogger<IndigitallPushNotification> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
        : base(
            logger)
    {
        Configuration = configuration.GetSection(nameof(PushNotificationConfiguration)).Get<PushNotificationConfiguration<IndigitallConfiguration>>()
           ?.Implementations?.FirstOrDefault(where => where.Identifier == Implementation)?.Information
            ?? throw new CustomPushNotificationException($"No se encontró configuración para Notificaciones Push: {nameof(IndigitallConfiguration)}");
        HttpClient = httpClientFactory.CreateClient();
        HttpClient.Timeout = TimeSpan.FromSeconds(Configuration.Timeout);
        HttpClient.DefaultRequestHeaders.Add("authorization", $"ServerKey {Configuration.ServerKey}");
    }


    /// <summary>
    /// Envío de notificación a Token
    /// </summary>
    /// <param name="notificationToken"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationByTokenRequest notificationTokens)
    {
        var typeConfiguration = Configuration.SecuryConfiguration;
        HttpClient.BaseAddress = new Uri(typeConfiguration.BaseUrl);
        var notificationItems = Enumerable.Empty<NotificationItem>().ToList();
        foreach (var token in notificationTokens.Tokens.SelectMany(token => token.Value))
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();
            var request = new SendSecureNotificationRequest
            {
                ApplicationId = Configuration.ApplicationId,
                IsDisposable = true,
                Silent = false,
                CustomFields = new()
                {
                    Title = notificationTokens.Title,
                    Body = notificationTokens.Body,
                },
                Data = JsonConvert.SerializeObject(new AdditionalDataRequest()),
                IdType = "deviceId",
                Device = token
            };
            var path = $"/v1/campaign/{typeConfiguration.CampaignId}/send/one?encrypted=false";
            Logger.LogInformation("Envía notificación a: {@Path} - Body: {@Body}", path, JsonConvert.SerializeObject(request));
            var response = await HttpClient.PostAsJsonAsync(path, request).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Logger.LogInformation("Se recibe la respuesta en '{@DurationTime}ms' - Response: {@HttpCode} {@Response}", stopwatch.ElapsedMilliseconds, response.StatusCode, responseContent);
            var data = JsonConvert.DeserializeObject<SendSecureNotificationResponse>(responseContent)
                    ?.Data?.FirstOrDefault();
            notificationItems.Add(!response.IsSuccessStatusCode || data?.Status?.ToUpper() != $"{ResponseResultType.Ok.ToString().ToUpper()}"
            ? NotificationItem.Fail(token, responseContent)
            : NotificationItem.Success(responseContent, data?.DeviceId));
        }
        return await Task.FromResult(new NotificationResponse(notificationItems)).ConfigureAwait(false);
    }


    /// <summary>
    /// Envío de notificación a Tópico
    /// </summary>
    /// <param name="notificationTopic"></param>
    /// <returns></returns>
    public async Task<NotificationResponse> SendNotificationAsync(NotificationByTopicRequest notificationTopic)
    {
        var typeConfiguration = Configuration.CampaignConfiguration;
        HttpClient.BaseAddress = new Uri(typeConfiguration.BaseUrl);
        var platforms = notificationTopic.Topic switch
        {
            "Android" => new[] { "android", "harmony" },
            "Ios" => new[] { "ios" },
            _ => new[] { "android", "harmony", "ios" },
        };
        var request = new UpdateCampaignRequest(
            notificationTopic.Title,
            notificationTopic.Body,
            JsonConvert.SerializeObject(new AdditionalDataRequest()),
            platforms
        );
        //Actualizar Campaña
        var path = $"/v1/campaign/{typeConfiguration.CampaignId}";
        Logger.LogInformation("Envía notificación a: {@Path} - Body: {@Body}", path, JsonConvert.SerializeObject(request));
        var stopwatch = new Stopwatch();
        stopwatch.Restart();
        var response = await HttpClient.PutAsJsonAsync(path, request).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Se recibe la respuesta en '{@DurationTime}ms' - Response: {@HttpCode} {@Response}", stopwatch.ElapsedMilliseconds, response.StatusCode, responseContent);
        var updateResponse = JsonConvert.DeserializeObject<IndigitallGenericResponse>(responseContent);
        if (updateResponse.StatusCode != (int)HttpStatusCode.OK)
            throw new CustomPushNotificationException($"Error al actualizar campaña: {updateResponse.Message}");
        //Enviar Notificación
        path = $"/v1/campaign/{typeConfiguration.CampaignId}/send/all";
        Logger.LogInformation("Envía notificación a: {@Path} - Body: {@Body}", path, JsonConvert.SerializeObject(request));
        stopwatch.Restart();
        response = await HttpClient.PostAsJsonAsync(path, request).ConfigureAwait(false);
        responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Logger.LogInformation("Se recibe la respuesta en '{@DurationTime}ms' - Response: {@HttpCode} {@Response}", stopwatch.ElapsedMilliseconds, response.StatusCode, responseContent);
        var sendAllResponse = JsonConvert.DeserializeObject<SendAllCampaignResponse>(responseContent);
        if (!response.IsSuccessStatusCode || sendAllResponse.StatusCode != (int)HttpStatusCode.OK)
            throw new CustomPushNotificationException($"Existió un error en el envío de la notificación por tópico: {notificationTopic.Topic} - {sendAllResponse?.Message}");
        //Enviar notificación
        return new NotificationResponse($"{sendAllResponse.Data?.SendingId ?? 0}", notificationTopic.Topic);
    }


}
