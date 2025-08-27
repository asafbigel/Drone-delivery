using BlApi;                 // IBL, BlFactory
using BlWebApi.Services;     // ApiMapper
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// BL to DI (IBL כ־Singleton)
builder.Services.AddSingleton<IBL>(_ => BlFactory.GetBl());

// Mapper (אם הוא static אין חובה, אבל לא מזיק)
builder.Services.AddSingleton<ApiMapper>();

// CORS from appsettings: "AllowedOrigins": ["*"] לפיתוח, או רשימת דומיינים לפרודקשן
var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "*" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins", policy =>
    {
        if (origins.Length == 1 && origins[0] == "*")
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        else
            policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// HTTPS + CORS
app.UseHttpsRedirection();
app.UseCors("AllowConfiguredOrigins");

// Swagger רק בפיתוח (ב־Azure Production תוכל להדליק אם תרצה)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// דף בית -> Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// נקודת Health פשוטה
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// Controllers
app.MapControllers();

app.Run();
