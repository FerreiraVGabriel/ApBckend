using Microsoft.VisualBasic.CompilerServices;
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
using ProjetoGabrielAPI.Shared;

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
            //services.AddCors();
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowDev",
            //     //  builder => builder.WithOrigins("http://localhost:4200/").WithMethods("PUT", "DELETE", "GET")
            //     builder => builder
            //         .SetIsOriginAllowedToAllowWildcardSubdomains()
            //         .WithOrigins("http://localhost:4200/")
            //         .AllowAnyMethod()
            //         .AllowCredentials()
            //         .AllowAnyHeader()
            //         .Build()
            //      );
            // });
            services.AddCors();
            

            //REPOSITORY
            services.AddTransient<ITimesRepository, TimesRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<ICompeticaoRepository, CompeticaoRepository>();
            services.AddTransient<IMercadosRepository, MercadosRepository>();
            services.AddTransient<IApostasRepository, ApostasRepository>();
            services.AddTransient<IApostasLiveRepository, ApostasLiveRepository>();
            services.AddTransient<ITipoApostaRepository, TipoApostaRepository>();
            services.AddTransient<IFiltrosRepository, FiltrosRepository>();
            services.AddTransient<IInformacoesRepository, InformacoesRepository>();
            services.AddTransient<ITelaPrincipalRepository, TelaPrincipalRepository>();

            //Utils
            services.AddTransient<UtilsProject>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            // app.UseCors(options =>
            // options.WithOrigins("http://localhost:4200")
            // .AllowAnyMethod()
            // .AllowAnyHeader()
            // .AllowCredentials());

                // global cors policy
                app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) 
                .AllowCredentials()); 



            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }
    }
}
