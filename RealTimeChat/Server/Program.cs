using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.ResponseCompression;
using RealTimeChat.Server.Hubs;
using System.Data.SqlTypes;
using System;
using RealTimeChat.Server.Data;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddScoped(serviceProvider =>
{
    string key = builder.Configuration["AzureCognitiveServices:Key"] ?? string.Empty;
    string endpoint = builder.Configuration["AzureCognitiveServices:Endpoint"] ?? string.Empty;

    return new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));
});

builder.Services.AddScoped<ITextAnalyzeService, TextAnalyzeService>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chatHub");

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
