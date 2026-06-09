using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext.
// ASP.NET Core will automatically create and inject it when needed.
builder.Services.AddDbContext<CareBridgeScaffoldContext>();

// Add Swagger support.
// Swagger gives us a testing screen for APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Vue.js running on another port
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS.
app.UseCors();

// Simple health-check endpoint.
app.MapGet("/", () =>
{
    return "CareBridge API is running";
});

// Return first 20 patients.
// EF Core converts this LINQ query into SQL.
app.MapGet("/api/patients/search",
    (CareBridgeScaffoldContext db,string? name) =>
    {
        return db.Patients
        .Where(p => p.City == "Pune")
                 .Where(s => s.IsActive == true)
                  .Where(p => string.IsNullOrEmpty(name) || p.FullName.Contains(name))

                 // Select only columns we need.
                 .Select(p => new
                 {
                     p.PatientId,
                     p.FullName,
                     p.City,
                     p.IsActive
                 })

                 // Return only first 20 rows.
                 


                 // Execute query.
                 .ToList();
    });


app.MapGet("/api/analytics/department-load",
    (CareBridgeScaffoldContext db) =>
    {
        var cutoff = DateTime.Now.AddDays(-60);
        var result =
        (
            from e in db.Encounters
            where e.AdmitDate >= cutoff
            join d in db.Departments
                on e.DepartmentId equals d.DepartmentId
            group e by new { d.DepartmentId, d.Name } into g
            select new
            {
                DepartmentName = g.Key.Name,
                inpatient = g.Count(x => x.EncounterType == "Inpatient"),
                outpatient = g.Count(x => x.EncounterType == "Outpatient"),
                ed = g.Count(x => x.EncounterType == "ED"),
                Total = g.Count()
            }
        )
        .OrderByDescending(x => x.Total)
        .ToList();

        return result;
    });




app.Run();
