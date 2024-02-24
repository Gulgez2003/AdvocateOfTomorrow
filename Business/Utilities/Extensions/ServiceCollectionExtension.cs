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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<FileExtension>();

            services.AddSingleton<IMongoClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new MongoClient(configuration["MongoDB:ConnectionString"]);
            });

            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepositoryBase<>));
            services.AddHttpContextAccessor();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization();

            return services;
        }
    }
}
