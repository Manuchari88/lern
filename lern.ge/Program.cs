var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ეს ბლოკი აკეთებს ყველაფერს, რაც გუგლს სჭირდება:
// 1. იჭერს ნებისმიერ მოთხოვნას
// 2. აბრუნებს 301 სტატუსს
// 3. უთითებს ახალ მისამართს
app.Run(async context =>
{
    var newUrl = "https://sdrive.ge" + context.Request.Path + context.Request.QueryString;
    context.Response.StatusCode = 301;
    context.Response.Headers.Location = newUrl;
    await Task.CompletedTask;
});

app.Run();