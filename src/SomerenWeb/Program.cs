using SomerenWeb.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddScoped<IStudentRepository>(_ => new StudentRepository(connectionString));
builder.Services.AddScoped<ILecturerRepository>(_ => new LecturerRepository(connectionString));
builder.Services.AddScoped<IRoomRepository>(_ => new RoomRepository(connectionString));
builder.Services.AddScoped<IActivityRepository>(_ => new ActivityRepository(connectionString));
builder.Services.AddScoped<IDrinkOrderRepository>(_ => new DrinkOrderRepository(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
