using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WebApi.Miscellaneous;

[assembly: ApiConventionType(typeof(ApiConventions))]
TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
    options.Filters.Add(new ProducesAttribute("application/json"));
});

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddFluentValidationAutoValidation();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTheme(ScalarTheme.Solarized)
            .WithClientButton(false);
    });
}

app.Use(async (context, next) =>
{
    if (app.Logger.IsEnabled(LogLevel.Debug))
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        Log.LogRequest(
            app.Logger,
            context.Request.Method,
            context.Request.Path,
            string.Join(", ", context.Request.Headers.Select(x => $"{x.Key}={x.Value}")),
            body);
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
