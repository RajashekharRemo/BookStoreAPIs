using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // below syntax to generate token
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // it validates the server, that generates the token
            ValidateAudience = true,// it validates the recipient of the token, is autherised to recieve
            ValidateLifetime = true,// it checks, if token is not expired at end the signing issuer key is valid or not
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // 
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // it valids the signature of the token

        };
    }); // raja



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });  // For Lock
    //option.OperationFilter<SwaggerFileOperationFilter>();

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });


    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBusiness,  UserBusiness>();
builder.Services.AddScoped<IBookBusiness,  BookBusiness>();
builder.Services.AddScoped<IBookRepository,  BookRepository>();
builder.Services.AddScoped<IOrderAddressBusiness,  OrderAddressBusiness>();
builder.Services.AddScoped<IOrderAdressRepository,  OrderAdressRepository>();
builder.Services.AddScoped<IWishListBusiness, WishListBusiness>();
builder.Services.AddScoped<IWishList,  WishList>();
builder.Services.AddScoped<IReviewBusiness,  ReviewBusiness>();
builder.Services.AddScoped<IReviewRepository,  ReviewRepository>();






builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});

builder.Services.AddMassTransitHostedService();






//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(1);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;// or SameSiteMode.Lax or SameSiteMode.Strict
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//});



builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (op) =>
    {
        op.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthentication(); // raja
app.UseAuthorization();
//app.UseSession();// raja2

app.MapControllers();

app.Run();
