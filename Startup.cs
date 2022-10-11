using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoGabrielAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoGabrielAPI.Interfaces;

namespace ProjetoGabrielAPI
{
    public class Startup
    {
        public IConfiguration Configuration {get; set;}

        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("sql")));
            services.AddControllers();
            services.AddCors();

            //REPOSITORY
            services.AddTransient<ITimesRepository, TimesRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<ICompeticaoRepository, CompeticaoRepository>();
            services.AddTransient<IMercadosRepository, MercadosRepository>();
            services.AddTransient<IApostasRepository, ApostasRepository>();
            services.AddTransient<IApostasLiveRepository, ApostasLiveRepository>();
            services.AddTransient<ITipoApostaRepository, TipoApostaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(option => option.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }
    }
}
