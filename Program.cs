
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

            // Register the Repository and Service after DbContext
            // Repository
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPostImageRepository, PostImageRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IRoleCheckRepository, RoleCheckRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ILikeRepository, LikeRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<ITypeFriendsRepository, TypeFriendsRepository>();
            builder.Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            builder.Services.AddScoped<IFriendRepository, FriendRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<ISearchRepository, SearchRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            //Service
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
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IReportService, ReportService>();

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
                app.UseSwagger((c =>
                {
                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
                }));
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
