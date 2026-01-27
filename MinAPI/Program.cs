using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PeopleDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () => "API is running!");

app.MapGet("/people/count", (PeopleDbContext db) =>
{
    var count = db.People.Count();
    return Results.Ok(new { Count = count });
});

app.MapGet("/people/{id}", (PeopleDbContext db, int id) =>
{
    var person = db.People.Include(x => x.Address).Include(x => x.Contracts).Where(p => p.Id == id).SingleOrDefault();
    if (person == null)
        return Results.NotFound();
    return Results.Ok(person);
});

//endpoint, which will search by endpoint if the email adress .Contains some string 

app.MapGet("/people/email/{email}", (PeopleDbContext db, string email) =>
{
    var people = db.People.Include(x => x.Address).Include(x => x.Contracts).Where(p => p.Email.ToLower().Contains(email.ToLower()));
    if (people == null)
        return Results.NotFound();
    return Results.Ok(people);
});

app.Run();
