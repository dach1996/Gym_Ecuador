using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Core;
using PersistenceDb.Utils.Extension;
using PersistenceDb.Models.Configuration;

namespace PersistenceDb;
public class PersistenceContext(
    DbContextOptions<PersistenceContext> options,
    DatabaseConfiguration databaseConfiguration) : DbContext(options)
{
    #region Constructor

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseEncryption(databaseConfiguration.AesSecret);
        base.OnModelCreating(modelBuilder);
    }

    #endregion

    #region Administration Tables

    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<PlaceUser> PlaceUsers { get; set; }
    public DbSet<FilePersistence> FilePersistences { get; set; }
    public DbSet<CooperativeBusService> BusServices { get; set; }
    public DbSet<CooperativeBus> CooperativeBuses { get; set; }
    public DbSet<Cooperative> Cooperatives { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<FloorDiagramBusCooperative> FloorDiagrams { get; set; }
    public DbSet<NotificationPush> NotificationPushes { get; set; }
    public DbSet<NotificationPushUser> NotificationPushUsers { get; set; }
    public DbSet<NotificationPushUserDevice> NotificationPushUserDevices { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<TransportPoint> TransportPoints { get; set; }
    public DbSet<CooperativeTransportPoint> CooperativeTransportPoints { get; set; }
    #endregion

    #region Authentication DbSet

    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserDevicePushToken> UserDevicePushTokens { get; set; }


    #endregion

    #region Core DbSet

    public DbSet<Companion> Companions { get; set; }
    public DbSet<ReserveSeat> ReserveSeats { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderCancelation> OrderCancelations { get; set; }
    public DbSet<OrderSeatPerson> OrderSeatPeople { get; set; }
    public DbSet<QueueMessage> QueueMessages { get; set; }
    public DbSet<CooperativeRoute> CooperativeRoutes { get; set; }

    #endregion
}