using DuoLearn.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using DuoLearn.Infrastructure.Context;
using DuoLearn.Applications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DuoLearn.Api;
using DuoLearn.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddTransient<AuthenticatedUser>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUserService, UserServices>();
builder.Services.AddTransient<IAuthServices, AuthService>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<ILanguageService, LanguageServices>();
builder.Services.AddTransient<ISectionService, SectionServices>();
builder.Services.AddTransient<ILevelService, LevelServices>();
builder.Services.AddTransient<ILessonService, LessonServices>();
builder.Services.AddTransient<IQuestionService, QuestionServices>();
builder.Services.AddTransient<IQuestionValidateService, QuestionValidateService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<AuthenticatedUser>();

app.MapControllers();

app.Run();
