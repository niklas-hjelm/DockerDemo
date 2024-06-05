using DockerDemoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PeopleRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/people", async (PeopleRepository repo, string name) =>
{
    await repo.AddPerson(name);
    return await repo.GetAllPeople();
});

app.Run();
