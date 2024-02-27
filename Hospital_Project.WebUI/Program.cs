using HospitalProject.Business.Abstract;
using HospitalProject.Business.Concrete;
using HospitalProject.DataAccess.Abstract;
using HospitalProject.Entities.Data;
using HospitalProject.DataAccess.Abstract;
using HospitalProject.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital.WebUI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var context = builder.Configuration.GetConnectionString("mydb");

builder.Services.AddDbContext<CustomIdentityDbContext>(opt =>
{
    opt.UseSqlServer(context);
});

builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDoctorDal, EFDoctorDal>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IPatientDal, EFPatientDal>();
builder.Services.AddScoped<IAppointmentDal, EFAppointmentDal>();
builder.Services.AddScoped<IDepartmentDal, EFDepartmentDal>();

builder.Services.AddIdentity<CustomIdentityUser,CustomIdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

//builder.Services.AddIdentity<Doctor, CustomIdentityRole>()
//    .AddEntityFrameworkStores<CustomIdentityDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<Admin, CustomIdentityRole>()
//    .AddEntityFrameworkStores<CustomIdentityDbContext>()
//    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Default", "{controller=Authentication}/{action=Start}/{id?}");
    endpoints.MapHub<UserHub>("/userhub");
});

app.Run();
