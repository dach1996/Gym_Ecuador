using Autofac;
using PersistenceDb.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Infrastructure.Modules;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QueueMessageRepository>().As<IQueueMessageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProcessTrackingRepository>().As<IProcessTrackingRepository>().InstancePerLifetimeScope();
        builder.RegisterType<TrainerGymRepository>().As<ITrainerGymRepository>().InstancePerLifetimeScope();
        builder.RegisterType<GymBranchRepository>().As<IGymBranchRepository>().InstancePerLifetimeScope();
        builder.RegisterType<GymBranchScheduleRepository>().As<IGymBranchScheduleRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EquipmentImageRepository>().As<IEquipmentImageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RoutineRepository>().As<IRoutineRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ExerciseRepository>().As<IExerciseRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ExerciseTagRepository>().As<IExerciseTagRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RoutineExerciseRepository>().As<IRoutineExerciseRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SeriesRecordRepository>().As<ISeriesRecordRepository>().InstancePerLifetimeScope();
    }
}