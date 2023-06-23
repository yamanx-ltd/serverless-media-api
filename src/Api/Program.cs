using System.Reflection;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Api.Extensions;
using Api.Infrastructure.Context;
using Domain.Options;
using Domain.Repositories;
using Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<UploadSettings>(builder.Configuration.GetSection("UploadSettings"));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddScoped<IApiContext, ApiContext>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddDefaultAWSOptions(new AWSOptions
{
    Profile = "serverless",
    Region = RegionEndpoint.EUCentral1
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEndpointsCore(AppDomain.CurrentDomain.GetAssemblies());

app.Run();

static IEnumerable<Assembly> GetAssembly()
{
    yield return typeof(Program).Assembly;
}