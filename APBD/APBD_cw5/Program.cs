using APBD_cw5;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddControllers();
builder.Services.AddSingleton<IMockDb, MockDb>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/animals", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAll());
});
app.MapGet("/animals/{id}", (IMockDb mockDb, int id) =>
{
    var animal = mockDb.GetById(id);
    if (animal is null) return Results.NotFound();
    
    return Results.Ok(animal);
});

app.MapGet("/appointments/{id}", (IMockDb mockDb, int id) =>
{
    return Results.Ok(mockDb.GetAppointmentsByAnimalId(id));
});
app.MapPost("/appointments", (IMockDb mockDb, Appointment appointment) =>
{
    mockDb.Add(appointment);
    return Results.Created();
});


app.MapPost("/animals", (IMockDb mockDb, Animal animal) =>
{
    mockDb.Add(animal);
    return Results.Created();
});

app.MapDelete("/animals", (IMockDb mockDb, int id) =>
    {
        if (mockDb.Delete(id))
            return Results.Ok();
        else
            return Results.NotFound();
        
    }
);

app.MapPut("/animals/{id}", (IMockDb mockDb, int id, Animal animal) =>
{
    var existingAnimal = mockDb.GetById(id);
    if (existingAnimal is null)
    {
        return Results.NotFound();
    }

    existingAnimal.Name = animal.Name;
    existingAnimal.Category = animal.Category;
    existingAnimal.Mass = animal.Mass;
    existingAnimal.FurColor = animal.FurColor;

    mockDb.Update(existingAnimal);

    return Results.Ok();
});

app.Run();


// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }