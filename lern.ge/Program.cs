using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// სერვისების დამატება
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- 301 მუდმივი გადამისამართება SEO-სთვის ---
// Map მეთოდი პირდაპირ იჭერს ყველა მოთხოვნას და აღარ ტოვებს შეცდომის შანსს
app.Map("{*path}", async (HttpContext context) =>
{
    var newUrl = "https://sdrive.ge" + context.Request.Path + context.Request.QueryString;

    context.Response.Headers.Location = newUrl;
    context.Response.StatusCode = 301;

    await context.Response.CompleteAsync();
});

// ქვემოთ მოცემული კოდი სტრუქტურისთვის
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();