using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// სერვისების დამატება
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- 301 მუდმივი გადამისამართება SEO-სთვის ---
//// ვიყენებთ app.Run-ს Middleware-ის ნაცვლად, რადგან გვინდა მოთხოვნა აქ დასრულდეს
//app.Run(async (context) =>
//{
//    //context.Response.StatusCode = 301;
//    //context.Response.Redirect("https://sdrive.ge", permanent: true);
//    //await Task.CompletedTask;
//});

// ქვემოთ მოცემული კოდი აღარ გაეშვება, მაგრამ სინტაქსურად საჭიროა
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