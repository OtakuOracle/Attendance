using presence.data.RemoteData.RemoteDataBase;
using presence_api.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<RemoteDatabaseContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureGroup();
builder.Services.ConfigureAdmin();
builder.Services.ConfigurePresence();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();



app.Run();


