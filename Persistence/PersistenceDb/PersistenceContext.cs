using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Core;
using PersistenceDb.Utils.Extension;
using PersistenceDb.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace PersistenceDb;
public class PersistenceContext(
    DbContextOptions<PersistenceContext> options,
    IConfiguration configuration) : DbContext(options)
{
    #region Constructor

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var databaseConfiguration = configuration.GetSection("CustomConnectionStrings").Get<List<DatabaseConfiguration>>().FirstOrDefault()
            ?? throw new InvalidOperationException("No se encontró la configuración de la base de datos en el appsettings.json");
        modelBuilder.UseEncryption(databaseConfiguration.AesSecret);
        base.OnModelCreating(modelBuilder);
    }

    #endregion

    #region Administration Tables

    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<FilePersistence> FilePersistences { get; set; }
    public DbSet<NotificationPush> NotificationPushes { get; set; }
    public DbSet<NotificationPushUser> NotificationPushUsers { get; set; }
    public DbSet<NotificationPushUserDevice> NotificationPushUserDevices { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Region> Regions { get; set; }
    #endregion

    #region Authentication DbSet

    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserDevicePushToken> UserDevicePushTokens { get; set; }


    #endregion

    #region Core DbSet

    public DbSet<QueueMessage> QueueMessages { get; set; }

    #endregion

    #region Gym DbSet

    public DbSet<Gym> Gyms { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<GymPhoto> GymPhotos { get; set; }
    public DbSet<GymVideo> GymVideos { get; set; }
    public DbSet<GymMachine> GymMachines { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<GroupClass> GroupClasses { get; set; }
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<ClassReservation> ClassReservations { get; set; }
    public DbSet<GymReview> GymReviews { get; set; }
    public DbSet<TrainerRating> TrainerRatings { get; set; }
    public DbSet<PersonalGoal> PersonalGoals { get; set; }

    #endregion
}