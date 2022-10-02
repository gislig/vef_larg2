using Battleground.Api.Schema;
using Battleground.Services.Implementations;
using Battleground.Services.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefer();
builder.Services.AddHttpScope();
builder.Services.AddDbContext<BattlegroundDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BattlegroundConnectionString"), b => b.MigrationsAssembly("Battleground.Api"));
});

builder.Services.AddTransient<IPokemonService, PokemonService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IBattleService, BattleService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddHttpClient<IPokemonService, PokemonService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("PokemonApiBaseUrl"));
});

builder.Services.AddGraphQL(qlBuilder => qlBuilder
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
    .AddSchema<BattlegroundSchema>()
    .AddGraphTypes()
    .AddDataLoader());

var app = builder.Build();

app.UseGraphQLPlayground();
app.UseGraphQL<ISchema>();
app.MapGet("/", context =>
{
    context.Response.Redirect("/ui/playground");
    return Task.CompletedTask;
});

app.Run();
