
using Microsoft.EntityFrameworkCore;
using Social_Media.BAL;
using Social_Media.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Social_Media
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"], // API issue token
                        ValidAudience = jwtSettings["Audience"], // Frontend use token
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the PostRepository and PostService after DbContext
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<PostCategoryRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PostImageRepository>();
            builder.Services.AddScoped<CommentRepository>();
            builder.Services.AddScoped<RoleCheckRepository>();
            builder.Services.AddScoped<RoleRepository>();
            builder.Services.AddScoped<LikeRepository>();
            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<TypeFriendsRepository>();
            builder.Services.AddScoped<FriendRequestRepository>();
            builder.Services.AddScoped<FriendRepository>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostCategoryService, PostCategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPostImageService, PostImageService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleCheckService, RoleCheckService>();
            builder.Services.AddScoped<ILikeService, LikeService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<ITypeFriendsService, TypeFriendsService>();
            builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();
            builder.Services.AddScoped<IFriendsService, FriendService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            // Add Identity services
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddResponseCaching(); //Response Caching
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddSignalR();
            //CORS (Cross-Origin Resource Sharing) allow access API from other domain 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet( "/user", () => "Hello World!");
            app.MapControllers();

            app.Run();
        }
    }
}
