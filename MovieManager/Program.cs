using MovieManager.Infrastructure;

//Builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddApplicationDbContexts();

// Identity Service
builder.Services.AddIdentityContext();

// Custom Services (Services, Redis, FluentValidation, Cookies)
builder.Services.AddApplicationServices(); 
builder.Services.AddRedisCache(builder);
builder.Services.AddFluentValidationWithReflection();
builder.Services.AddCookieConsentPolicy();

// Build
WebApplication app = builder.Build();


//Http request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage().UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error").UseHsts();
}

app.UseHttpsRedirection().UseStaticFiles().UseRouting();
app.UseAuthentication().UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(name: "Area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");

app.MapRazorPages(); //for Login/Register views

//Run the App
app.Run();
