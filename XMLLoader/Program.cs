var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

await EnsureDbAsync(serviceProvider);
await LoadXML(serviceProvider);

async Task LoadXML(ServiceProvider serviceProvider)
{
    var XMLService = serviceProvider.GetRequiredService<XMLService>();
    await XMLService.AddOrUpdateProducts();
}

static void ConfigureServices(IServiceCollection services)
{
    IConfiguration config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .Build();

    services.AddSingleton<IConfiguration>(provider => config);
    services.AddLogging(configure => configure.AddConsole())
            .AddTransient<XMLService>();

    services.AddDbContext<ProductDbContext>(options =>
    {
        options.UseSqlServer(
            config.GetConnectionString("ProductDb") ?? throw new InvalidOperationException("Missing connection string configuration"),
            builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                builder.CommandTimeout(10);
            }
        );
    });

    services.AddScoped<IXMLService, XMLService>();
}

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<ProductDbContext>();
    await db.Database.MigrateAsync();
}

