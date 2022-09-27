using Battleground.Api.Schema;
using Battleground.Repositories;
using Battleground.Services.Implementations;
using Battleground.Services.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IPokemonService, PokemonService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>
    ("PokemonApiBaseUrl"));
});

var config = builder.Configuration;
var connString = config.GetConnectionString("BattlegroundConnectionString");
builder.Services.AddDbContext<BattlegroundDbContext>(options => options.UseNpgsql(connString, x => x.MigrationsAssembly("Battleground.Api")));

builder.Services.AddDefer();
builder.Services.AddHttpScope();

builder.Services.AddTransient<IPokemonService, PokemonService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IBattleService, BattleService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();

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
