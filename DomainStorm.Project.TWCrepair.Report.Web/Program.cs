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
using DomainStorm.Project.TWCrepair.Report.Web.ViewModel;
using DomainStorm.Project.TWCrepair.Report.Web;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;
using DotNetEnv;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using SharedMockServices = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Mock;
using SharedStagingServices = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Staging;
using StagingServices = DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;
using MockServices = DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using Radzen;


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
    builder.Services.AddRadzenComponents();

    if (builder.Configuration.GetSection("ENVIRONMENT").Value == "Staging")
    {
        builder.Services.AddOpenIdConnectCodeExchange(builder.Configuration);

        builder.Services.AddScoped<IGetService<Function, Guid>, SharedStagingServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<Function?, Uri>, SharedStagingServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<User, Guid>, SharedStagingServices.UserService>();
        builder.Services.AddScoped<IChangeIdentity, SharedStagingServices.UserService>();
        builder.Services.AddScoped<IGetService<Navbar, Guid>, NavbarService>();
        builder.Services.AddScoped<IGetService<TreeItem, Guid>, TreeItemService>();
        builder.Services.AddScoped<IGetService<Stream, ReportConvertRequest>, StagingServices.ReportService>();
        builder.Services.AddScoped<IGetService<PlotlyJson, ReportConvertRequest>, StagingServices.ReportService>();
        builder.Services.AddScoped<IGetService<AutoLoginToken, string>, AutoLoginTokenService>();
        builder.Services.AddScoped<IGetService<Department, string>, SharedStagingServices.DepartmentService>();
        builder.Services.AddScoped<IGetService<Post, string>, SharedMockServices.PostService>();
        builder.Services.AddScoped<IGetService<DA001, string>, StagingServices.DA001Service>();
        builder.Services.AddScoped<IGetService<DA002, string>, StagingServices.DA002Service>();
        builder.Services.AddScoped<IGetService<DA003, string>, StagingServices.DA003Service>();
        builder.Services.AddScoped<IGetService<DA004, string>, StagingServices.DA004Service>();
        builder.Services.AddScoped<IGetService<DA005, string>, StagingServices.DA005Service>();
        builder.Services.AddScoped<IGetService<RA001, string>, StagingServices.RA001Service>();
        builder.Services.AddScoped<IGetService<DateTime, Guid>, StagingServices.RA001Service>();

    }
    else
    {
        builder.Services.AddScoped<ICache, MockCache>();

        builder.Services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

        builder.Services.AddScoped<IGetService<Function, Guid>, MockServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<Function?, Uri>, MockServices.FunctionsService>();

        builder.Services.AddScoped<IGetService<Department, string>, SharedMockServices.DepartmentService>();
        builder.Services.AddScoped<IGetService<Post, string>, SharedMockServices.PostService>();

        builder.Services.AddScoped<IGetService<Stream, ReportConvertRequest>,MockServices.ReportService>();
        builder.Services.AddScoped<IGetService<PlotlyJson, ReportConvertRequest>, MockServices.ReportService>();
        builder.Services.AddScoped<IGetService<DA001, string>, MockServices.DA001Service>();
        builder.Services.AddScoped<IGetService<DA002, string>, MockServices.DA002Service>();
        builder.Services.AddScoped<IGetService<DA003, string>, MockServices.DA003Service>();
        builder.Services.AddScoped<IGetService<DA004, string>, MockServices.DA004Service>();
        builder.Services.AddScoped<IGetService<DA005, string>, MockServices.DA005Service>();
        builder.Services.AddScoped<IGetService<RA001, string>, MockServices.RA001Service>();
        builder.Services.AddScoped<IGetService<DateTime, Guid>, MockServices.RA001Service>();
    }

    if (!string.IsNullOrWhiteSpace(builder.Configuration["SqlDbOptions:ConnectionString"]))
    {
        builder.Services.Configure<SqlDbOptions>(builder.Configuration.GetSection("SqlDbOptions"));
        builder.Services.AddScoped<GetSession>(
            c =>
            {
                var options = c.GetRequiredService<IOptions<SqlDbOptions>>();
                var contextOption = new DbContextOptionsBuilder<Models.TWCrepairDbContext>()
                    .UseSqlServer(options.Value.ConnectionString).Options;

                DbContext GetDbContext()
                {
                    return new Models.TWCrepairDbContext(contextOption);
                }

                return GetDbContext;
            }
        );
        builder.Services.AddDbContext<Models.TWCrepairDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["SqlDbOptions:ConnectionString"]);
        });

        builder.Services
            .AddTransient<IRepository<Models.Form.Form>, SqlDbRepository<Models.Form.Form>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Form.Form>>>(
                c => c.GetRequiredService<IRepository<Models.Form.Form>>);

        builder.Services
            .AddTransient<IRepository<Models.CheckForm>, SqlDbRepository<Models.CheckForm>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckForm>>>(
                c => c.GetRequiredService<IRepository<Models.CheckForm>>);
        builder.Services
            .AddTransient<IRepository<Models.CheckFormConfirmForm>, SqlDbRepository<Models.CheckFormConfirmForm>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckFormConfirmForm>>>(
                c => c.GetRequiredService<IRepository<Models.CheckFormConfirmForm>>);
        builder.Services
            .AddTransient<IRepository<Models.CheckFormConfirmSituation>, SqlDbRepository<Models.CheckFormConfirmSituation>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckFormConfirmSituation>>>(
                c => c.GetRequiredService<IRepository<Models.CheckFormConfirmSituation>>);
        builder.Services
            .AddTransient<IRepository<Models.CheckFormTransfer>, SqlDbRepository<Models.CheckFormTransfer>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckFormTransfer>>>(
                c => c.GetRequiredService<IRepository<Models.CheckFormTransfer>>);

        builder.Services
           .AddTransient<IRepository<Models.FixForm>, SqlDbRepository<Models.FixForm>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.FixForm>>>(
                c => c.GetRequiredService<IRepository<Models.FixForm>>);

        builder.Services
            .AddTransient<IRepository<Models.CheckDailyReport>, SqlDbRepository<Models.CheckDailyReport>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckDailyReport>>>(
                c => c.GetRequiredService<IRepository<Models.CheckDailyReport>>);

        builder.Services
            .AddTransient<IRepository<Models.CheckDailyReportDetail>, SqlDbRepository<Models.CheckDailyReportDetail>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.CheckDailyReportDetail>>>(
                c => c.GetRequiredService<IRepository<Models.CheckDailyReportDetail>>);

        builder.Services
            .AddTransient<IRepository<Models.HR.HRDailyChange>, SqlDbRepository<Models.HR.HRDailyChange>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.HR.HRDailyChange>>>(
                c => c.GetRequiredService<IRepository<Models.HR.HRDailyChange>>);

        builder.Services
            .AddTransient<IRepository<Models.HR.HRCurrentPosition>, SqlDbRepository<Models.HR.HRCurrentPosition>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.HR.HRCurrentPosition>>>(
                c => c.GetRequiredService<IRepository<Models.HR.HRCurrentPosition>>);

        builder.Services
            .AddTransient<IRepository<Models.HR.HRSalary>, SqlDbRepository<Models.HR.HRSalary>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.HR.HRSalary>>>(
                c => c.GetRequiredService<IRepository<Models.HR.HRSalary>>);

        builder.Services
            .AddTransient<IRepository<Models.AttachmentFile>, SqlDbRepository<Models.AttachmentFile>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.AttachmentFile>>>(
                c => c.GetRequiredService<IRepository<Models.AttachmentFile>>);

        builder.Services
            .AddTransient<IRepository<Models.Form.FormAttachment>, SqlDbRepository<Models.Form.FormAttachment>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Form.FormAttachment>>>(
                c => c.GetRequiredService<IRepository<Models.Form.FormAttachment>>);

        builder.Services
           .AddTransient<IRepository<Models.WaterPressureCheck>, SqlDbRepository<Models.WaterPressureCheck>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.WaterPressureCheck>>>(
                c => c.GetRequiredService<IRepository<Models.WaterPressureCheck>>);

        builder.Services
           .AddTransient<IRepository<Models.WaterPressureCheckData>, SqlDbRepository<Models.WaterPressureCheckData>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.WaterPressureCheckData>>>(
                c => c.GetRequiredService<IRepository<Models.WaterPressureCheckData>>);


        builder.Services.AddTransient<IUnitOfWork, SqlDbUnitOfWork>();
        builder.Services.AddScoped<GetRepository<IUnitOfWork>>(
            c => c.GetRequiredService<IUnitOfWork>);

        builder.Services.AddHostedService<Worker>();
    }

    builder.Services.AddOptions();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();

    builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo("keys"))
        .SetApplicationName("TWCrepair");

    var app = builder.Build();

    var pathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATHBASE");

    if (!string.IsNullOrEmpty(pathBase))
    {
        app.UsePathBase(pathBase);
        Console.WriteLine("Hosting pathBase: " + pathBase);
    }

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