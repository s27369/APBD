using Api;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IMockDb, MockDb>();

// builder.Services.AddSession<IMockDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/mstudents", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAll());
});

app.MapGet("/mstudents/{id}", (IMockDb mockDb, int id) =>
{
    var student = mockDb.GetById(id);
    if (student is null) return Results.NotFound();
    
    return Results.Ok(student);
});

app.MapPost("/mstudents", (IMockDb mockDb, Student student) =>
{
    mockDb.Add(student);
    return Results.Created();
});

app.MapControllers();
app.Run();