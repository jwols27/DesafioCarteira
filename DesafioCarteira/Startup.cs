using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DesafioCarteira.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCarteira
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddNHibernate(Configuration.GetConnectionString("DefaultConnection"));
            services.AddControllersWithViews();

            services.AddScoped<SeedingService>();
            services.AddScoped<CarteiraService>();
            services.AddScoped<PessoasService>();

            services.Configure<MvcOptions>(options =>
            {
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                     (x) => "O valor é inválido");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => $"O valor {x} deve ser um número");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => $"O valor da propriedade '{x}' não foi informado");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (x, y) => $"O valor '{x}' não é valido para {y}");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => "O campo é obrigatório");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => $"O valor informado é inválido para {x}");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => "O valor é inválido");
            });

            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new[] { new CultureInfo("pt-BR"), new CultureInfo("en") };
                options.DefaultRequestCulture = new RequestCulture("pt-BR", "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {

            var supportedCultures = new[] { new CultureInfo("pt-BR"), new CultureInfo("en") };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("pt-BR")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(localizationOptions);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Carteira}/{action=Index}/{id?}");
            });
        }
    }
}
