using CSharpClicker.BackgroundServices;
using CSharpClicker.Hubs;
using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.Infrastructure.Implementations;
using CSharpClicker.Intitialization;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    DbContextInitializer.InitializeDataBase(appDbContext);
}
// UserRolesInitializer.SeedRolesAsync(app.Services);

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapHub<ClickerHub>("/clickerHub");
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    IdentityInitializer.Initialize(builder.Services);
    DbContextInitializer.InitializeDbContext(builder.Services);

    services.AddSingleton<IConnectedUsersRegistry, ConnectedUsersRegistry>();
    services.AddScoped<ICurrentUserIdAccessor, CurrentUserIdAccessor>();
    services.AddScoped<IAppDbContext, AppDbContext>();
    services.AddScoped<IScoreNotificationService, ScoreNotificationService>();
    services.AddHostedService<AutoProfitService>();

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
    services.ConfigureApplicationCookie(opt =>
    {
        opt.LoginPath = "/auth/login";
        opt.LogoutPath = "/auth/logout";

        opt.Cookie.MaxAge = TimeSpan.FromDays(14);
        opt.ExpireTimeSpan = TimeSpan.FromDays(14);
        opt.SlidingExpiration = true;
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
    });

    services.AddAutoMapper(typeof(Program).Assembly);
    services.AddMediatR(typeof(Program).Assembly);
    services.AddSwaggerGen();
    services.AddControllersWithViews();
    services.AddSignalR();
    services.AddHttpContextAccessor(); 
}
