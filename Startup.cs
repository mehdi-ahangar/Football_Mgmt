using Football_Mgmt.Data;
using Football_Mgmt.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Football_Mgmt
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

           
            services.AddDbContext<FootballContext>(opt => opt.UseInMemoryDatabase("Football_Mgmt"));
           
            services.AddScoped<IFootballRepository, FootballRepository>();
            
            
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddControllersWithViews()
                     .AddNewtonsoftJson(options =>
                          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                );
                      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
             }
            
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<FootballContext>();
                  

            AddTestData(context);
         
           app.UseMvc();
           app.UseHttpsRedirection();
         
           app.UseRouting();

            app.UseAuthorization();
                       
        }
        private static void AddTestData(FootballContext context)
        {
            var player1 = new Player { Id = 90, Name = "Ronaldo" };
            var player2 = new Player { Id = 80, Name = "Messi" };


            var team1 = new Team
            {
                        Id = 1,
                    StadiumId = 1,
                     PlayerId = 1
               
        };
            var team2 = new Team
            {
                Id = 2,
                Name = "RealMadrid",
                StadiumId = 2
            };          
            

            context.Players.Add(player1);
            context.Players.Add(player2);

             
            context.Teams.Add(team1);
            context.Teams.Add(team2);
            context.Teams.Find(1).Players.Insert(0, player1);

            context.SaveChanges();       
          
        }
               
    }
}
