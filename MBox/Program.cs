using System.Text;
using System.Text.Json.Serialization;
using MBox.Models.Db;
using MBox.Repositories.MYSQL;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.Db;
using MBox.Services.RabbitMQ;
using MBox.Services.Responce;
using MBox.Services.Responce.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using MBox.Services;
using Minio;
using Minio.DataModel.Args;
using MBox;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.z\

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicBox API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//MinIO
var endpoint = "play.min.io";
var accessKey = "Q3AM3UQ867trueSPQQA43P2F";
var secretKey = "zuf+tfteSlswRu7BJ86wtrueekitnifILbZam1KYY3TG";
builder.Services.AddMinio(accessKey, secretKey);

builder.Services.AddMinio(configureClient => configureClient
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey));

builder.Services.AddAuthentication()
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidIssuer = "config.issuer",
        ValidateAudience = true,
        ValidAudience = "config.audience",
        ValidateLifetime = true,
    };
});

builder.Services.AddSingleton<RabbitMqService>(sp =>
{
    var connectionFactory = new ConnectionFactory
    {
        HostName = builder.Configuration["RabbitMQ:HOST_NAME"],

        UserName = builder.Configuration["RabbitMQ:USER_NAME"],
        Password = builder.Configuration["RabbitMQ:PASSWORD"],
        Ssl = { Enabled = false }
    };
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connection = connectionFactory.CreateConnection();
    return new RabbitMqService(connection, configuration);
});

builder.Services.AddScoped<IBandApplicationService, BandApplicationService>();
builder.Services.AddScoped<IBandService, BandService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IBaseService<News>, NewsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();



builder.Services.AddScoped<IRepository<Application>, BandApplicationRepository>();
builder.Services.AddScoped<IRepository<Album>, AlbumRepository>();
builder.Services.AddScoped<IRepository<Band>, BandRepository>();
builder.Services.AddScoped<IRepository<News>, NewsRepository>();
builder.Services.AddScoped<IRepository<Playlist>, PlaylistRepository>();
builder.Services.AddScoped<IRepository<Song>, SongRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<LikedPlaylist>, LikedPlaylistsRepository>();

builder.Services.AddScoped<IHttpResponseHandler, HttpResponseHandler>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/public/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/public/swagger/v1/swagger.json", "MusicBox API");
    c.RoutePrefix = "api/public/swagger";
});

app.UseForwardedHeaders();
app.UseCors(builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());


Seed(app);
//app.UseMiddleware<AuthorisationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


async void Seed(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var _context = services.GetRequiredService<AppDbContext>();
    if (_context != null)
    {
        SeedDB seed = new SeedDB();
        await seed.SeedAsync(_context);
    }
}