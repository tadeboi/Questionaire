using Microsoft.EntityFrameworkCore;
using Questionaire.Infra;
using Questionaire.Interfaces;
using Questionaire.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseCosmos(
        builder.Configuration.GetConnectionString("CosmosDbConnectionString"), "Questionaire"
        );
});

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});


builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

