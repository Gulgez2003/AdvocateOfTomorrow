using Core.Entities.DataAccess.Abstract;
using Core.Entities.DataAccess.Concrete;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Business.Utilities.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IAcademicRepository, AcademicRepository>();
            services.AddScoped<IAcademicService, AcademicService>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<FileExtension>();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IMongoClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new MongoClient(configuration["MongoDB:ConnectionString"]);
            });

            // Регистрация репозитория
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepositoryBase<>));

            return services;
        }
    }
}
