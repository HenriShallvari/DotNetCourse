var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// we added the Swashbuckle.AspNetCore package to use Swagger UI.

builder.Services.AddEndpointsApiExplorer(); // looks at what endpoints we have and throws them into Swagger
builder.Services.AddSwaggerGen(); // generates all the necessary info for Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();

// This setup allows us to have a bunch of endpoints by creating 
// a bunch of controllers inside the Controllers folder without having to 
// declare everytime the routes.