using Azure.Identity;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.Client;

namespace AzureDevopsIntegration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<VssConnection>(sp =>
            {
                var credential = new ClientSecretCredential(builder.Configuration["AzureDevops:TenantId"],
                    builder.Configuration["AzureDevops:ClientId"],
                    builder.Configuration["AzureDevops:ClientSecret"]);

                var vssAadCredentials = new VssAzureIdentityCredential(credential);
                var orgUrl = new Uri(new Uri("https://dev.azure.com"), builder.Configuration["AzureDevops:OrganizationName"]);
                return new VssConnection(orgUrl, vssAadCredentials);
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
