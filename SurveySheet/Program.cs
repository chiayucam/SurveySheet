using SurveySheet.Repositories;
using SurveySheet.Repositories.Interfaces;
using SurveySheet.Services;
using SurveySheet.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISheetService, SheetService>();


var connectionString = builder.Configuration.GetConnectionString("SurveySheet");
builder.Services.AddSingleton<ISheetRepository>(sp => new SheetRepository(connectionString));


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
