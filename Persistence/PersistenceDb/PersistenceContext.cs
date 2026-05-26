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
    public DbSet<CatalogLanguage> CatalogLanguages { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<FilePersistence> FilePersistences { get; set; }
    public DbSet<NotificationPush> NotificationPushes { get; set; }
    public DbSet<NotificationPushUser> NotificationPushUsers { get; set; }
    public DbSet<NotificationPushUserDevice> NotificationPushUserDevices { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Parish> Parishes { get; set; }
    public DbSet<TypeIdentification> TypeIdentifications { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<ForumComment> ForumComments { get; set; }
    public DbSet<FileBasePath> FileBasePaths { get; set; }
    #endregion

    #region Authentication DbSet

    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<UserDevicePushToken> UserDevicePushTokens { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<Functionality> Functionalities { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleFunctionality> RoleFunctionalities { get; set; }
    public DbSet<UserRoleScope> UserRoleScopes { get; set; }

    #endregion

    #region Core DbSet

    public DbSet<QueueMessage> QueueMessages { get; set; }
    public DbSet<ProcessTracking> ProcessTrackings { get; set; }
    public DbSet<ProcessTrackingImage> ProcessTrackingImages { get; set; }
    public DbSet<PhysicalParameter> PhysicalParameters { get; set; }
    public DbSet<ProcessTrackingMeasurement> ProcessTrackingMeasurements { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseTag> ExerciseTags { get; set; }
    public DbSet<RoutineExercise> RoutineExercises { get; set; }
    public DbSet<SeriesRecord> SeriesRecords { get; set; }

    #endregion

    #region Gym DbSet

    public DbSet<Gym> Gyms { get; set; }
    public DbSet<GymBranch> GymBranches { get; set; }
    public DbSet<GymBranchSchedule> GymBranchSchedules { get; set; }
    public DbSet<GymBranchImage> GymBranchImages { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<GymPhoto> GymPhotos { get; set; }
    public DbSet<GymVideo> GymVideos { get; set; }
    public DbSet<GymMachine> GymMachines { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<TrainerGym> TrainerGyms { get; set; }
    public DbSet<GroupClass> GroupClasses { get; set; }
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<ClassReservation> ClassReservations { get; set; }
    public DbSet<GymReview> GymReviews { get; set; }
    public DbSet<TrainerRating> TrainerRatings { get; set; }
    public DbSet<PersonalGoal> PersonalGoals { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<EquipmentImage> EquipmentImages { get; set; }
    public DbSet<BranchPlan> BranchPlans { get; set; }
    public DbSet<ClientGymBranch> ClientGymBranches { get; set; }
    public DbSet<ClientMembership> ClientMemberships { get; set; }
    public DbSet<PlanFeature> PlanFeatures { get; set; }

    #endregion
}