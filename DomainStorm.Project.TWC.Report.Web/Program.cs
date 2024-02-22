using DomainStorm.Framework;
using DomainStorm.Framework.Authentication;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Dapr;
using DomainStorm.Framework.LibreOffice;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework.WebApi;
using DomainStorm.Project.TWC.Report.Web;
using DomainStorm.Project.TWC.Report.Web.Services.Impl;
using DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock;
using DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using DomainStorm.Project.TWC.Report.Web.Views;
using DomainStorm.Project.TWC.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Repository.Models.Form;
using DotNetEnv;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;
using DepartmentService = DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging.DepartmentService;
using ReportService = DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging.ReportService;
using UserService = DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging.UserService;

try
{
    if (Environment.GetEnvironmentVariable("DomainStorm_ENVIRONMENT") != "Staging")
        Env.Load();

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddEnvironmentVariables("DomainStorm_");

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Logging.AddSerilog();
    Log.Information($"Starting web host ({builder.Environment.ApplicationName})");

// Add services to the container.
    builder.Services.AddWebApi(builder.Configuration, mvcOptions => mvcOptions.Filters.Add(new DaprExceptionFilter()));
    builder.Services.AddDapr(builder.Configuration);
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddHttpClient();
    builder.Services.AddRazorEngine(typeof(Program).Assembly);

    builder.Services.AddScoped<TokenProvider>();

    builder.Services.AddScoped<IConvert, LibreOfficeConvert>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    if (builder.Configuration.GetSection("ENVIRONMENT").Value == "Staging")
    {
        builder.Services.AddOpenIdConnectCodeExchange(builder.Configuration);

        builder.Services.AddScoped<IGetService<Function, Guid>, FunctionsService>();
        builder.Services.AddScoped<IGetService<Function?, Uri>, FunctionsService>();
        builder.Services.AddScoped<IGetService<User, Guid>, UserService>();
        builder.Services.AddScoped<IChangeIdentity, UserService>();
        builder.Services.AddScoped<IGetService<Navbar, Guid>, NavbarService>();
        builder.Services.AddScoped<IGetService<TreeItem, Guid>, TreeItemService>();
        builder.Services.AddScoped<IGetService<Stream, ReportConvertRequest>, ReportService>();
        builder.Services.AddScoped<IGetService<PlotlyJson, ReportConvertRequest>, ReportService>();
        builder.Services.AddScoped<IGetService<AutoLoginToken, string>, AutoLoginTokenService>();
        builder.Services.AddScoped<IGetService<Department, string>, DepartmentService>();

        builder.Services.AddScoped<IGetService<RA001, string>, RA001Service>();
        builder.Services.AddScoped<IGetService<RA002, string>, RA002Service>();
        builder.Services.AddScoped<IGetService<RA999, string>, RA999Service>();
        builder.Services.AddScoped<IGetService<DA001, string>, DA001Service>();
        
    }
    else
    {
        builder.Services.AddScoped<ICache, MockCache>();

        builder.Services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

        builder.Services
            .AddScoped<IGetService<Department, string>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.DepartmentService>();

        builder.Services
            .AddScoped<IGetService<Stream, ReportConvertRequest>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.ReportService>();

        //builder.Services
        //    .AddScoped<IGetService<string, ReportConvertRequest>,
        //        DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.ReportService>();

        builder.Services
            .AddScoped<IGetService<RA001, string>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.RA001Service>();
        builder.Services
            .AddScoped<IGetService<RA002, string>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.RA002Service>();
        builder.Services
            .AddScoped<IGetService<RA999, string>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.RA999Service>();
        builder.Services
            .AddScoped<IGetService<DA001, string>,
                DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock.DA001Service>();

    }

    if (!string.IsNullOrWhiteSpace(builder.Configuration["SqlDbOptions:ConnectionString"]))
    {
        //builder.Services.Configure<SqlDbOptions>(builder.Configuration.GetSection("SqlDbOptions"));
        //builder.Services.AddScoped<GetSession>(
        //    c =>
        //    {
        //        var options = c.GetRequiredService<IOptions<SqlDbOptions>>();
        //        var contextOption = new DbContextOptionsBuilder<TWCWebDbContext>()
        //            .UseSqlServer(options.Value.ConnectionString).Options;

        //        DbContext GetDbContext()
        //        {
        //            return new TWCWebDbContext(contextOption);
        //        }

        //        return GetDbContext;
        //    }
        //);
        //builder.Services.AddDbContext<TWCWebDbContext>(options =>
        //{
        //    options.UseSqlServer(builder.Configuration["SqlDbOptions:ConnectionString"]);
        //});

        //builder.Services.AddTransient<IRepository<WaterRegisterChangeForm>, SqlDbRepository<WaterRegisterChangeForm>>();
        //builder.Services.AddScoped<GetRepository<IRepository<WaterRegisterChangeForm>>>(
        //    c => c.GetRequiredService<IRepository<WaterRegisterChangeForm>>);

        builder.Services.AddTransient<IRepository<Form>, SqlDbRepository<Form>>();
        builder.Services.AddScoped<GetRepository<IRepository<Form>>>(
            c => c.GetRequiredService<IRepository<Form>>);

        builder.Services.AddTransient<IRepository<FormAttachment>, SqlDbRepository<FormAttachment>>();
        builder.Services.AddScoped<GetRepository<IRepository<FormAttachment>>>(
            c => c.GetRequiredService<IRepository<FormAttachment>>);

        builder.Services.AddHostedService<Worker>();
    }

    builder.Services.AddOptions();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();

    builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo("keys"))
        .SetApplicationName("TwcWeb");

    var app = builder.Build();

    app.UsePathBase("/twcreport");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.UseHttpsRedirection();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseOAuth2SwaggerUI(builder.Configuration);
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
    }

    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapDefaultControllerRoute();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}