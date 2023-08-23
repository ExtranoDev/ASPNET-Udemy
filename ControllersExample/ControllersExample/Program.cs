var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // adds all the controller classes as services

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers(); // maps and routes all controllers to respective endpoints

app.Run();
