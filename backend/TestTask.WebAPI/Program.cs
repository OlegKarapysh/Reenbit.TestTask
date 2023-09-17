using TestTask.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureBlobClient(builder.Configuration);
builder.Services.AddBlobStorageService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors => cors
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(app.Configuration["WebClientUrl"]));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();