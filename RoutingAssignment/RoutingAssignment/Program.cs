var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Dictionary<int, string> countryList = new Dictionary<int, string>();
countryList.Add(1, "United States");
countryList.Add(2, "Canada");
countryList.Add(3, "United Kingdom");
countryList.Add(4, "India");
countryList.Add(5, "Japan");

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapGet("countries", async context =>
    {
        foreach (KeyValuePair<int, string> kvp in countryList)
        {
            await context.Response.WriteAsync($"{kvp.Key}, {kvp.Value}\n");
        }
    });

    endpoints.MapGet("/countries/{countryID:int:range(1,100)}", async (context) =>
    {
        var routeValues = context.Request.RouteValues;
        int countryID = Convert.ToInt32(routeValues["countryID"]);
        if (countryID > 0 && countryID < 6)
        {
            await context.Response.WriteAsync($"{countryList[countryID]}");
        }
        else if (countryID > 5 &&  countryID < 101)
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 404;
            await context.Response.WriteAsync($"[No Country]");
        }
    });

    endpoints.MapGet("/countries/{countryID:min(101)}", async context =>
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"The CountryID should be between 1 and 100");
    });
});

app.Run(async (context) => {
    await context.Response.WriteAsync("Won't get the answer follow /countries/[id - optional]");
});

app.Run();
