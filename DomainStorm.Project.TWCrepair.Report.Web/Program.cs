using DomainStorm.Framework;
using DomainStorm.Framework.Authentication;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Dapr;
using DomainStorm.Framework.LibreOffice;
using DomainStorm.Framework.Pdf;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework.WebApi;
using DomainStorm.Project.TWCrepair.Report.Web;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;
using DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;
using DomainStorm.Project.TWCrepair.Report.Web.ViewModel;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using DotNetEnv;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Radzen;
using Serilog;
using static DomainStorm.Framework.BlazorComponent.CommandModel.SysManagementLog.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using MockServices = DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using SharedMockService = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Mock;
using SharedMockServices = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Mock;
using SharedStagingService = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Staging;
using SharedStagingServices = DomainStorm.Project.TWCrepair.Shared.Services.Impl.Staging;
using StagingServices = DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;


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
    builder.Services.AddScoped<IMerge, LibreOfficeMerge>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddRadzenComponents();

    if (builder.Configuration.GetSection("ENVIRONMENT").Value == "Staging")
    {
        builder.Services.AddOpenIdConnectCodeExchange(builder.Configuration);

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });

        builder.Services.AddScoped<IGetService<Function, Guid>, SharedStagingServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<Function?, Uri>, SharedStagingServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<User, Guid>, SharedStagingServices.UserService>();
        builder.Services.AddScoped<IChangeIdentity, SharedStagingServices.UserService>();
        builder.Services.AddScoped<IGetService<Navbar, Guid>, NavbarService>();
        builder.Services.AddScoped<IGetService<DomainStorm.Project.TWCrepair.Report.Web.ViewModel.TreeItem, Guid>, TreeItemService>();
        builder.Services.AddScoped<IGetService<Stream, ReportConvertRequest>, SharedStagingService.ReportService>();
        builder.Services.AddScoped<IGetService<PlotlyJson, ReportConvertRequest>, SharedStagingService.ReportService>();
        builder.Services.AddScoped<IGetService<AutoLoginToken, string>, AutoLoginTokenService>();
        builder.Services.AddScoped<IGetService<Department, string>, SharedStagingServices.DepartmentService>();
        builder.Services.AddScoped<IGetService<Post, string>, SharedStagingServices.PostService>();
        builder.Services.AddScoped<IGetService<DA001, string>, StagingServices.DA001Service>();
        builder.Services.AddScoped<IGetService<DA002, string>, StagingServices.DA002Service>();
        builder.Services.AddScoped<IGetService<DA003, string>, StagingServices.DA003Service>();
        builder.Services.AddScoped<IGetService<DA004, string>, StagingServices.DA004Service>();
        builder.Services.AddScoped<IGetService<DA005, string>, StagingServices.DA005Service>();
        builder.Services.AddScoped<IGetService<DA006, string>, StagingServices.DA006Service>();
        builder.Services.AddScoped<IGetService<DA007, string>, StagingServices.DA007Service>();
        builder.Services.AddScoped<IGetService<DA008, string>, StagingServices.DA008Service>();
        builder.Services.AddScoped<IGetService<RA001, string>, StagingServices.RA001Service>();
        builder.Services.AddScoped<IGetService<DateTime, Guid>, StagingServices.RA001Service>();
        builder.Services.AddScoped<IGetService<RA002, string>, StagingServices.RA002Service>();
        builder.Services.AddScoped<IGetService<RA003, string>, StagingServices.RA003Service>();
        builder.Services.AddScoped<IGetService<RA004, string>, StagingServices.RA004Service>();
        builder.Services.AddScoped<IGetService<RA005, string>, StagingServices.RA005Service>();
        builder.Services.AddScoped<IGetService<RA006, string>, StagingServices.RA006Service>();
        builder.Services.AddScoped<IGetService<RA007, string>, StagingServices.RA007Service>();
        builder.Services.AddScoped<IGetService<RA008, string>, StagingServices.RA008Service>();
        builder.Services.AddScoped<IGetService<RA009, string>, StagingServices.RA009Service>();
        builder.Services.AddScoped<IGetService<RA010, string>, StagingServices.RA010Service>();
        builder.Services.AddScoped<IGetService<RA011, string>, StagingServices.RA011Service>();
        builder.Services.AddScoped<IGetService<RA012, string>, StagingServices.RA012Service>();
        builder.Services.AddScoped<IGetService<RA013, string>, StagingServices.RA013Service>();
        builder.Services.AddScoped<IGetService<RA014, string>, StagingServices.RA014Service>();
        builder.Services.AddScoped<IGetService<RA015, string>, StagingServices.RA015Service>();
        builder.Services.AddScoped<IGetService<RA016, string>, StagingServices.RA016Service>();
        builder.Services.AddScoped<IGetService<RA017, string>, StagingServices.RA017Service>();
        builder.Services.AddScoped<IGetService<RA018, string>, StagingServices.RA018Service>();
        builder.Services.AddScoped<IGetService<RA019, string>, SharedStagingServices.RA019Service>();
        builder.Services.AddScoped<IGetService<RA019FixForm, string>, SharedStagingServices.RA019Service>();
        builder.Services.AddScoped<IGetService<RA020, string>, StagingServices.RA020Service>();
        builder.Services.AddScoped<IGetService<RA021, string>, StagingServices.RA021Service>();
        builder.Services.AddScoped<IGetService<RA022, string>, StagingServices.RA022Service>();
        builder.Services.AddScoped<IGetService<RA023, string>, StagingServices.RA023Service>();
        builder.Services.AddScoped<IGetService<RA024, string>, StagingServices.RA024Service>();
        builder.Services.AddScoped<IGetService<RA025, string>, StagingServices.RA025Service>();
        builder.Services.AddScoped<IGetService<RA026, string>, StagingServices.RA026Service>();
        builder.Services.AddScoped<IGetService<RA027, string>, StagingServices.RA027Service>();
        builder.Services.AddScoped<IGetService<RA028, string>, StagingServices.RA028Service>();
        builder.Services.AddScoped<IGetService<RA029, string>, StagingServices.RA029Service>();
        builder.Services.AddScoped<IGetService<RA030, string>, StagingServices.RA030Service>();
        builder.Services.AddScoped<IGetService<RA031, string>, StagingServices.RA031Service>();
        builder.Services.AddScoped<IGetService<RA032, string>, StagingServices.RA032Service>();
        builder.Services.AddScoped<IGetService<RA033, string>, StagingServices.RA033Service>();
        builder.Services.AddScoped<IGetService<RA041, string>, StagingServices.RA041Service>();
        builder.Services.AddScoped<IGetService<RA041MeasureDate, Guid>, StagingServices.RA041Service>();
        builder.Services.AddScoped<IGetService<BudgetDocResourceStatistics, Guid>, SharedStagingServices.BudgetDocResourceStatisticsService>();
        builder.Services.AddScoped<IGetService<BudgetDocOutSourceResourceStatistics, Guid>, SharedStagingServices.BudgetDocOutSourceResourceStatisticsService>();
        builder.Services.AddScoped<IGetService<BudgetDocContractResourceStatistics, Guid>, SharedStagingServices.BudgetDocContractResourceStatisticsService>();
        builder.Services.AddScoped<ICommandService<CreateSysManagementLog, DeleteSysManagementLog>, SharedMockServices.SysManagementLogService>(); //故意用mock,上面的 service 會用到,但不會去寫 log
    }
    else
    {
        builder.Services.AddScoped<ICache, MockCache>();

        builder.Services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

        builder.Services.AddScoped<IGetService<Function, Guid>, MockServices.FunctionsService>();
        builder.Services.AddScoped<IGetService<Function?, Uri>, MockServices.FunctionsService>();

        builder.Services.AddScoped<IGetService<Department, string>, SharedMockServices.DepartmentService>();
        builder.Services.AddScoped<IGetService<Post, string>, SharedMockServices.PostService>();

        builder.Services.AddScoped<IGetService<Stream, ReportConvertRequest>, SharedMockService.ReportService>();
        builder.Services.AddScoped<IGetService<PlotlyJson, ReportConvertRequest>, SharedMockService.ReportService>();
        builder.Services.AddScoped<IGetService<DA001, string>, MockServices.DA001Service>();
        builder.Services.AddScoped<IGetService<DA002, string>, MockServices.DA002Service>();
        builder.Services.AddScoped<IGetService<DA003, string>, MockServices.DA003Service>();
        builder.Services.AddScoped<IGetService<DA004, string>, MockServices.DA004Service>();
        builder.Services.AddScoped<IGetService<DA005, string>, MockServices.DA005Service>();
        builder.Services.AddScoped<IGetService<DA006, string>, MockServices.DA006Service>();
        builder.Services.AddScoped<IGetService<DA007, string>, MockServices.DA007Service>();
        builder.Services.AddScoped<IGetService<DA008, string>, MockServices.DA008Service>();
        builder.Services.AddScoped<IGetService<RA001, string>, MockServices.RA001Service>();
        builder.Services.AddScoped<IGetService<DateTime, Guid>, MockServices.RA001Service>();
        builder.Services.AddScoped<IGetService<RA002, string>, MockServices.RA002Service>();
        builder.Services.AddScoped<IGetService<RA003, string>, MockServices.RA003Service>();
        builder.Services.AddScoped<IGetService<RA004, string>, MockServices.RA004Service>();
        builder.Services.AddScoped<IGetService<RA005, string>, MockServices.RA005Service>();
        builder.Services.AddScoped<IGetService<RA006, string>, MockServices.RA006Service>();
        builder.Services.AddScoped<IGetService<RA007, string>, MockServices.RA007Service>();
        builder.Services.AddScoped<IGetService<RA008, string>, MockServices.RA008Service>();
        builder.Services.AddScoped<IGetService<RA009, string>, MockServices.RA009Service>();
        builder.Services.AddScoped<IGetService<RA010, string>, MockServices.RA010Service>();
        builder.Services.AddScoped<IGetService<RA011, string>, MockServices.RA011Service>();
        builder.Services.AddScoped<IGetService<RA012, string>, MockServices.RA012Service>();
        builder.Services.AddScoped<IGetService<RA013, string>, MockServices.RA013Service>();
        builder.Services.AddScoped<IGetService<RA014, string>, MockServices.RA014Service>();
        builder.Services.AddScoped<IGetService<RA015, string>, MockServices.RA015Service>();
        builder.Services.AddScoped<IGetService<RA016, string>, MockServices.RA016Service>();
        builder.Services.AddScoped<IGetService<RA017, string>, MockServices.RA017Service>();
        builder.Services.AddScoped<IGetService<RA018, string>, MockServices.RA018Service>();
        builder.Services.AddScoped<IGetService<RA019, string>, SharedMockService.RA019Service>();
        builder.Services.AddScoped<IGetService<RA019FixForm, string>, SharedMockService.RA019Service>();
        builder.Services.AddScoped<IGetService<RA020, string>, MockServices.RA020Service>();
        builder.Services.AddScoped<IGetService<RA021, string>, MockServices.RA021Service>();
        builder.Services.AddScoped<IGetService<RA022, string>, MockServices.RA022Service>();
        builder.Services.AddScoped<IGetService<RA023, string>, MockServices.RA023Service>();
        builder.Services.AddScoped<IGetService<RA024, string>, MockServices.RA024Service>();
        builder.Services.AddScoped<IGetService<RA025, string>, MockServices.RA025Service>();
        builder.Services.AddScoped<IGetService<RA026, string>, MockServices.RA026Service>();
        builder.Services.AddScoped<IGetService<RA027, string>, MockServices.RA027Service>();
        builder.Services.AddScoped<IGetService<RA028, string>, MockServices.RA028Service>();
        builder.Services.AddScoped<IGetService<RA029, string>, MockServices.RA029Service>();
        builder.Services.AddScoped<IGetService<RA030, string>, MockServices.RA030Service>();
        builder.Services.AddScoped<IGetService<RA031, string>, MockServices.RA031Service>();
        builder.Services.AddScoped<IGetService<RA032, string>, MockServices.RA032Service>();
        builder.Services.AddScoped<IGetService<RA033, string>, MockServices.RA033Service>();
        builder.Services.AddScoped<IGetService<RA041, string>, MockServices.RA041Service>();
        builder.Services.AddScoped<IGetService<RA041MeasureDate, Guid>, MockServices.RA041Service>();
        builder.Services.AddScoped<IGetService<BudgetDocResourceStatistics, Guid>, SharedMockService.BudgetDocResourceStatisticsService>();
        builder.Services.AddScoped<IGetService<BudgetDocOutSourceResourceStatistics, Guid>, SharedMockService.BudgetDocOutSourceResourceStatisticsService>();
        builder.Services.AddScoped<IGetService<BudgetDocContractResourceStatistics, Guid>, SharedMockService.BudgetDocContractResourceStatisticsService>();
        builder.Services.AddScoped<ICommandService<CreateSysManagementLog, DeleteSysManagementLog>, SharedMockServices.SysManagementLogService>();
    }

    builder.Services.AddScoped<GetMerge>(
        c =>
        {
            return GetMerge;

            IMerge GetMerge(FileExtension extension)
            {
                switch (extension)
                {
                    case FileExtension.PDF:
                        var invokeMethod = c.GetRequiredService<IInvokeMethod>();
                        return new GotenbergMerge(invokeMethod);
                    case FileExtension.ODS:
                    case FileExtension.ODT:
                    case FileExtension.XLSX:
                    case FileExtension.HTML:
                    case FileExtension.JSON:
                    default:
                        return new LibreOfficeMerge();
                }
            }
        });

    builder.Services.AddScoped<GetConvert>(
        c =>
        {
            return GetConvert;

            IConvert GetConvert(FileExtension extension)
            {
                switch (extension)
                {
                    case FileExtension.PDF:
                        var invokeMethod = c.GetRequiredService<IInvokeMethod>();
                        return new GotenbergConvert(invokeMethod);
                    case FileExtension.ODS:
                    case FileExtension.ODT:
                    case FileExtension.XLSX:
                    case FileExtension.HTML:
                    case FileExtension.JSON:
                    default:
                        return new LibreOfficeConvert();
                }
            }
        });

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

        builder.Services
           .AddTransient<IRepository<Models.WaterFlowCheck>, SqlDbRepository<Models.WaterFlowCheck>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.WaterFlowCheck>>>(
                c => c.GetRequiredService<IRepository<Models.WaterFlowCheck>>);

        builder.Services
           .AddTransient<IRepository<Models.WaterFlowCheckData>, SqlDbRepository<Models.WaterFlowCheckData>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.WaterFlowCheckData>>>(
                c => c.GetRequiredService<IRepository<Models.WaterFlowCheckData>>);

        builder.Services
           .AddTransient<IRepository<Models.Budget.BudgetDoc>, SqlDbRepository<Models.Budget.BudgetDoc>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Budget.BudgetDoc>>>(
                c => c.GetRequiredService<IRepository<Models.Budget.BudgetDoc>>);

        builder.Services
           .AddTransient<IRepository<Models.Budget.BudgetDocOutSource>, SqlDbRepository<Models.Budget.BudgetDocOutSource>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Budget.BudgetDocOutSource>>>(
                c => c.GetRequiredService<IRepository<Models.Budget.BudgetDocOutSource>>);

        builder.Services
           .AddTransient<IRepository<Models.Budget.BudgetDocContract>, SqlDbRepository<Models.Budget.BudgetDocContract>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Budget.BudgetDocContract>>>(
                c => c.GetRequiredService<IRepository<Models.Budget.BudgetDocContract>>);

        builder.Services
          .AddTransient<IRepository<Models.Word>, SqlDbRepository<Models.Word>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Word>>>(
                c => c.GetRequiredService<IRepository<Models.Word>>);

        builder.Services
           .AddTransient<IRepository<Models.Budget.ResourceWorkMaterial>, SqlDbRepository<Models.Budget.ResourceWorkMaterial>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Budget.ResourceWorkMaterial>>>(
                c => c.GetRequiredService<IRepository<Models.Budget.ResourceWorkMaterial>>);

        builder.Services
           .AddTransient<IRepository<Models.Budget.BudgetDocUnitPrice>, SqlDbRepository<Models.Budget.BudgetDocUnitPrice>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Budget.BudgetDocUnitPrice>>>(
                c => c.GetRequiredService<IRepository<Models.Budget.BudgetDocUnitPrice>>);

        builder.Services
           .AddTransient<IRepository<Models.YearPlan.YearPlanSetAllZone>, SqlDbRepository<Models.YearPlan.YearPlanSetAllZone>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.YearPlan.YearPlanSetAllZone>>>(
                c => c.GetRequiredService<IRepository<Models.YearPlan.YearPlanSetAllZone>>);


        builder.Services
           .AddTransient<IRepository<Models.YearPlan.YearPlanSetAllZoneItem>, SqlDbRepository<Models.YearPlan.YearPlanSetAllZoneItem>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.YearPlan.YearPlanSetAllZoneItem>>>(
                c => c.GetRequiredService<IRepository<Models.YearPlan.YearPlanSetAllZoneItem>>);

        builder.Services
           .AddTransient<IRepository<Models.YearPlan.YearPlanReport>, SqlDbRepository<Models.YearPlan.YearPlanReport>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.YearPlan.YearPlanReport>>>(
                c => c.GetRequiredService<IRepository<Models.YearPlan.YearPlanReport>>);


        builder.Services
           .AddTransient<IRepository<Models.Import.ImportPipe>, SqlDbRepository<Models.Import.ImportPipe>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Import.ImportPipe>>>(
                c => c.GetRequiredService<IRepository<Models.Import.ImportPipe>>);

        builder.Services
           .AddTransient<IRepository<Models.TWCrepairPost>, SqlDbRepository<Models.TWCrepairPost>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.TWCrepairPost>>>(
                c => c.GetRequiredService<IRepository<Models.TWCrepairPost>>);

        builder.Services
           .AddTransient<IRepository<Models.Check31Form>, SqlDbRepository<Models.Check31Form>>();
        builder.Services
            .AddScoped<GetRepository<IRepository<Models.Check31Form>>>(
                c => c.GetRequiredService<IRepository<Models.Check31Form>>);


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

    builder.Services.Configure<List<ResourceWorkMaterialCodeMapping>>(builder.Configuration.GetSection("ResourceWorkMaterialCodeMappings"));

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

    app.UseSwagger();
    app.UseOAuth2SwaggerUI(builder.Configuration);

    if (app.Environment.IsDevelopment())
    {
        
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
    }

    app.UseStaticFiles();
    app.UseCookiePolicy();
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