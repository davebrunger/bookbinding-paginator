using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddCommandLine(args, new Dictionary<string, string>
        {
            ["-i"] = "InputFile",
            ["-o"] = "OutputFile",
            ["-s"] = "SheetsPerSignature",
        });
    })
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddOptions<Configuration>().Bind(hostingContext.Configuration);
        services.AddScoped<IPageSequenceGenerator, PageSequenceGenerator>();
        services.AddScoped<IStreamPaginator, StreamPaginator>();
        services.AddScoped<Paginator>();
    })
    .Build();

var paginator = host.Services.GetRequiredService<Paginator>();

paginator.Paginate();