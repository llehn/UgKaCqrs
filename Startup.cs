using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Configuration;
using Akka.Persistence.Query;
using Akka.Persistence.Query.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UgKaCqrs
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var config = File.ReadAllText("akka.hokon");
            var actorSystem = ActorSystem.Create("demo", ConfigurationFactory.ParseString(config));
            var sharding = ClusterSharding.Get(actorSystem);
            var actorRef = sharding.Start(
                "book",
                Props.Create<BookAggregate>(),
                ClusterShardingSettings.Create(actorSystem),
                new MessageExtractor());

            var readJournal = PersistenceQuery.Get(actorSystem)
                .ReadJournalFor<SqlReadJournal>("akka.persistence.query.sql");

            services.AddSingleton(readJournal);
            services.AddSingleton(actorSystem);
            services.AddSingleton<BookQueryService>();

            var actorService = new ActorService(actorRef);
            services.AddSingleton(actorService);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
