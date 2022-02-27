

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using PokeApi.API.Mapping;
using PokeApi.API.Requests;
using PokeApi.API.Validators;
using PokeApi.Core;
using PokeApi.Persistence;
using PokeApi.Persistence.Settings;
using PokeAPI.Domain;
using System.Reflection;

namespace TrueLayer.Pokemon
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            Configure(services);
            ConfigureAutoMapper(services);
            ConfigureAppSettings(services);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<ErrorHandlingMiddleWare>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokemon", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrueLayer.Pokemon v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void Configure(IServiceCollection services)
        {
            services.AddScoped<IValidator<GetPokemonRequest>, GetPokemonRequestValidator>();
            services.AddScoped<IValidator<GetTranslatedPokemonRequest>, GetTranslatedPokemonRequestValidator>();
            services.AddScoped<IGetPokemonData, GetPokemonDataService>();
            services.AddScoped<ITranslateDescription, TranslateDescriptionService>();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PokemonResponseMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            services.AddSingleton(y =>
            {
                var _configuration = y.GetService<IConfiguration>();
                var _section = _configuration.GetSection("Values");
                var appsettingsConfig = new PokemonAppSettings(
                    _section["Language"],
                    _section["CaveHabitat"],
                    _section["YodaType"],
                    _section["ShakespeakereType"],
                    _section["YodaTranslationUrl"],
                    _section["ShakeSpeareTranslationUrl"]);
                return appsettingsConfig;
            });
        }
    }
}
