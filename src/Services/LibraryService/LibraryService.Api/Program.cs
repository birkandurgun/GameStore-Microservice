using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using LibraryService.Api.EventBusConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEventBusConfigurations();

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

var app = builder.Build();

app.Services.AddEventBusSubscription();

// Configure the HTTP request pipeline.

app.Run();
