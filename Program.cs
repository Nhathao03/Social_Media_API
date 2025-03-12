
using Microsoft.EntityFrameworkCore;
using Social_Media.BAL;
using Social_Media.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Social_Media
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


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

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
