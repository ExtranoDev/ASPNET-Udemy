using MiddlewareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("From Middleware 1\n");
    await next(context);
});

app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello from the Other side");
            await next();
        });
    });

// middleware 2
// app.UseMiddleware<MyCustomMiddleware>();
// app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

// middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("From Middleware 3\n");
});

app.Run(); // app.Run doesn't forward requests to the next middleware
