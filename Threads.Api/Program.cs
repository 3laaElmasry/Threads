using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.BusinessLogicLayer.Services;
using Threads.DataAccessLayer.Data;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.Repository;
using Threads.DataAccessLayer.RepositoryContracts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Clerk.BackendAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // 👈 Enables SwaggerB

//ConncetionString 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//{
//    options.Password.RequiredLength = 5;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireDigit = false;
//    options.Password.RequiredUniqueChars = 1;
//    options.Password.RequireNonAlphanumeric = false;
//}).AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders()
//.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
//.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();




builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IClerkUserService, ClerkUserService>();

builder.Services.AddSingleton(_ =>
    new ClerkBackendApi(bearerAuth: builder.Configuration["Clerk:SecretKey"]));

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://well-panda-46.clerk.accounts.dev";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://well-panda-46.clerk.accounts.dev",
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        };
    });

/*
//Map JWT Keys to JWTClass Popreties
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddScoped<IAuthService, AuthService>();

//JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //configure roles of jwt
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // 👈 Enables the Swagger UI
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
