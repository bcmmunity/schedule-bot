﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegrammAspMvcDotNetCoreBot.Models;
using Microsoft.EntityFrameworkCore;
using TelegrammAppMvcDotNetCore___Buisness_Logic;
using TelegrammAppMvcDotNetCore___Buisness_Logic.Models;

namespace TelegrammAspMvcDotNetCoreBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

			//Thread thread = new Thread(new ThreadStart(Function));
			//thread.IsBackground = true;
			//thread.Name = "Function";
			//thread.Start();
			ScheduleUpdate.Update();
		}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
		{
			// получаем строку подключения из файла конфигурации
			string connection = Configuration.GetConnectionString("DefaultConnection");
			// добавляем контекст MyContext в качестве сервиса в приложение
			services.AddDbContext<MyContext>(options =>
				options.UseSqlServer(connection));

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // обработка ошибок HTTP
            app.UseStatusCodePages();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Bot Configurations
            Bot.GetBotClientAsync().Wait();
        }
    }
}
