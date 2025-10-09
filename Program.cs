using Microsoft.EntityFrameworkCore;
using ONGManager.Controllers;
using ONGManager.Data;
using ONGManager.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<OngDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
{
    throw new InvalidOperationException("Supabase configuration is missing. Please check your User Secrets or environment variables.");
}

builder.Services.AddScoped(provider =>
    new Client(
        supabaseUrl: supabaseUrl,
        supabaseKey: supabaseKey,
        new SupabaseOptions
        {
            AutoConnectRealtime = true,
        }
    ));

builder.Services.AddScoped<ImagemService>();

builder.Services.AddScoped<CadastroAnimaisController>();

builder.Services.AddAuthentication("LoginCookie")
    .AddCookie("LoginCookie", options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AcessoNegado";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CadastroAnimais}/{action=Index}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CadastroUsuarios}/{action=Index}")
    .WithStaticAssets();

await app.RunAsync();

