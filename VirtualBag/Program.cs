using BLL.Services;
using DAL.EF;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); //added by me

//Depandency Injection
builder.Services.AddDbContext<VirtualBagDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

builder.Services.AddScoped<ClassRepo>();
builder.Services.AddScoped<SubjectRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<TeacherAssignmentRepo>();
builder.Services.AddScoped<BookRepo>();
builder.Services.AddScoped<NoteRepo>();
builder.Services.AddScoped<HomeworkRepo>();
builder.Services.AddScoped<HomeworkSubmissionRepo>();
builder.Services.AddScoped<AttendanceSessionRepo>();
builder.Services.AddScoped<AttendanceRepo>();
builder.Services.AddScoped<StudyActivityRepo>();
builder.Services.AddScoped<NotificationRepo>();

builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TeacherAssignmentService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<HomeworkService>();
builder.Services.AddScoped<HomeworkSubmissionService>();
builder.Services.AddScoped<AttendanceSessionService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<StudyActivityService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession(); //added by me

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

Console.WriteLine("Application Started");
app.Run();
