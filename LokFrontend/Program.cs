using LokFrontend.Infrastructure.Data;
using LokFrontend.Application.Services;
using LokFrontend.Infrastructure.Repositories;
using LokFrontend.Application.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IQuizService,QuizService>();
builder.Services.AddScoped<IQuizRepository,QuizRepository>();
builder.Services.AddScoped<INoticeService,NoticeService>();
builder.Services.AddScoped<INoticeRepository,NoticeRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
