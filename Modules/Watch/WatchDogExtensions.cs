// using WatchDog;
// using WatchDog.src.Enums;

// public static class WatchDogExtensions
// {
//     public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.AddWatchDogServices(opt =>
//         {
//             opt.SetExternalDbConnString = configuration.GetConnectionString("DefaultConnection");
//             opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
//             opt.IsAutoClear = true;
//             opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
//             // opt.Serializer = WatchDogSerializerEnum.Newtonsoft; // Removed as 'Serializer' is not a valid property
//         });
//         return services;
//     }
// }