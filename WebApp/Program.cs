using Microsoft.EntityFrameworkCore;
using Persistence;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Application.Service;
using FluentValidation.AspNetCore;
using Application.Validation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(
    opt=>opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes=true
);
//AutoMapper Configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//fluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers().AddFluentValidation(
    cfg => cfg.RegisterValidatorsFromAssemblyContaining<CategoryValidation>()
);
//Injections  dependencies
builder.Services.AddTransient<ICategoryService,CategoryService>();
//Database Configuration
builder.Services.AddDbContext<Context>(
    cfg => {
        cfg.UseSqlite("Data Source=test.db", b=>
        b.MigrationsAssembly("Persistence"));
    }
);

var app = builder.Build();

// Create migration when starting the application
using(var environment = app.Services.CreateScope()){
    var service = environment.ServiceProvider;

    try{
        var context = service.GetRequiredService<Context>();
        context.Database.Migrate();
    }catch(Exception e){
        var log = service.GetRequiredService<ILogger<Program>>();
        log.LogError(e, "Error in the migration");
    }


}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
