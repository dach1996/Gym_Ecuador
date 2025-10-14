using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Configuration;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace PersistenceDb.UnitOfWork;

/// <summary>
/// Constructor
/// </summary>
public abstract class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction _transaction;
    protected PersistenceContext Context;
    protected readonly ILoggerFactory LoggerFactory;
    protected readonly IConfiguration Configuration;
    protected readonly ILogger Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="configuration"></param>
    protected UnitOfWork(
        ILoggerFactory loggerFactory,
        IConfiguration configuration)
    {
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory.CreateLogger<UnitOfWork>();
        Configuration = configuration;
    }

    public async Task BeginTransactionAsync()
    => _transaction ??= await Context.Database.BeginTransactionAsync();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="process"></param>
    /// <param name="Commit"></param>
    /// <returns></returns>
    public async Task BeginTransactionStrategyAsync(
        Func<Task> process,
        bool autoCommit = true)
    {
        using var transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);
        try
        {
            await process().ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            if (autoCommit)
                await transaction.CommitAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en ejecución de {@MethodName} - {@Message}", nameof(BeginTransactionStrategyAsync), ex.Message);
            await transaction.RollbackAsync().ConfigureAwait(false);
        }
    }


    /// <summary>
    /// Inicia una transacción
    /// </summary>
    /// <param name="process"></param>
    /// <param name="Commit"></param>
    /// <returns></returns>
    public async Task<TResult> BeginTransactionStrategyAsync<TResult>(
        Func<Task<TResult>> process,
        bool autoCommit = true,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        using var transaction = await Context.Database.BeginTransactionAsync(isolationLevel).ConfigureAwait(false);
        try
        {
            var result = await process().ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            if (autoCommit)
                await transaction.CommitAsync().ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error en ejecución de {@MethodName} - {@Message}", nameof(BeginTransactionStrategyAsync), ex.Message);
            await transaction.RollbackAsync().ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    /// Commit
    /// </summary>
    /// <returns></returns>
    public async Task CommitAsync()
    {
        if (_transaction is not null)
        {
            await Context.SaveChangesAsync();
            await _transaction.CommitAsync().ConfigureAwait(false);
            _transaction = null;
        }
    }

    /// <summary>
    /// Disponse
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disponse
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        _transaction?.Dispose();
        Context?.Dispose();
    }

    /// <summary>
    /// RollBack
    /// </summary>
    /// <returns></returns>
    public async Task RollBackAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync().ConfigureAwait(false);
            _transaction = null;
        }
    }

    /// <summary>
    /// Configuración de Conexión
    /// </summary>
    /// <param name="databaseConfiguration"></param>
    /// <returns></returns>
    public Task SetDataBaseConfigurationAsync(DatabaseConfiguration databaseConfiguration)
    {
        if (string.IsNullOrEmpty(Context?.Database?.GetConnectionString()))
            Context = new(new DbContextOptionsBuilder<PersistenceContext>()
        .UseSqlServer(databaseConfiguration.ConnectionString, optionBuilder =>
        {
            optionBuilder.UseNetTopologySuite();
            optionBuilder.CommandTimeout(databaseConfiguration.CommandTimeOut);
        })
        .UseLoggerFactory(LoggerFactory)
        .EnableSensitiveDataLogging(databaseConfiguration.EnableSensitiveDataLogging)
        .EnableDetailedErrors(databaseConfiguration.EnableDetailedErrors)
        .Options, databaseConfiguration);
        return Task.CompletedTask;
    }
}
