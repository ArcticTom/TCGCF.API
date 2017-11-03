using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using TCGCF.API.Entities;
using TCGCF.API.Models;
using Microsoft.EntityFrameworkCore;
using TCGCF.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TCGCF.API.Ignore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using AspNetCoreRateLimit;

namespace TCGCF.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        private IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //loads configuration for later use by the throttling middleware
            services.AddOptions();

            //set used database and connectionstring to dbcontext
            //gets connectionstring from appSettings.json for dev and from the environment variables for prod
            services.AddDbContext<CardInfoContext>(o => o.UseSqlServer(Configuration["connectionStrings:TCGCardFetcher"]));

            //add data fetch repository service
            services.AddScoped<ICardInfoRepository, CardInfoRepository>();

            //initialize user table
            services.AddTransient<UserDataInitializer>();

            //initialize cards
            services.AddTransient<CardDataInitializer>();

            //add identity provider, sql db
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CardInfoContext>();

            //add caching
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //preventing redirects to default login url
            services.ConfigureApplicationCookie(options => options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
            {
                OnRedirectToLogin = (ctx) =>
                {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 401;
                    }
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = (ctx) =>
                {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 403;
                    }
                    return Task.CompletedTask;
                }
            });

            //add versioning
            services.AddApiVersioning(cfg =>
            {
                //set default version to 0.1
                cfg.DefaultApiVersion = new ApiVersion(0, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                //read version from header
                cfg.ApiVersionReader = new HeaderApiVersionReader("Version");
            });

            //add jwt token validation for user authentication
            services.AddAuthentication()
              .AddJwtBearer(cfg =>
              {
                  cfg.RequireHttpsMetadata = false;
                  cfg.SaveToken = true;

                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = Configuration["tokens:issuer"],
                      ValidAudience = Configuration["tokens:audience"],
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["tokens:key"])),
                      ValidateLifetime = true
                  };

              });

            //add new security policy
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
            });

            //add xml to supported input and output data formats
            //add ssl support
            services.AddMvc(opt =>
            {
                if (!_env.IsProduction())
                {
                    opt.SslPort = 44312;
                }
                opt.Filters.Add(new RequireHttpsAttribute());
            }
                ).AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter())).AddMvcOptions(o => o.InputFormatters.Add(new XmlDataContractSerializerInputFormatter()));

            /* #if DEBUG
             * Run at DEV
             * #else
             * Run at PROD
             * #endif
             */

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CardInfoContext context)
        {
            if (env.IsDevelopment())
            {
                //set developer friendly error page for dev environment
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //enable IP throttling
            app.UseIpRateLimiting();

            //tell automapper all mappings used between Entities and Models to remove all manual mapping work
            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<Set, SetWithNoCardsDTO>();
                c.CreateMap<Set, SetDTO>();
                c.CreateMap<Deck, DeckWithNoCardsDTO>();
                c.CreateMap<Deck, DeckDTO>();
                c.CreateMap<Card, CardDTO>();
                c.CreateMap<Card, CardNoIdDTO>();
                c.CreateMap<CardsInDeck, CardsInDeckDTO>();
                c.CreateMap<Legality, LegalityDTO>();
                c.CreateMap<CardToDeckAddingDTO, CardsInDeck>();
                c.CreateMap<DeckAddingDTO, Deck>();
                c.CreateMap<DeckUpdatingDTO, Deck>();
                c.CreateMap<Deck, DeckUpdatingDTO>();

                //to map to a versioned model
                //c.CreateMap<Deck, DeckWithNoCardsDTOTest>().IncludeBase<Deck, DeckWithNoCardsDTO>();
                //to map to object with a different name
                //c.CreateMap<Example, ExampleDTO>().ForMember(c => c.StartDate, opt => opt.MapFrom(example => example.EventDate))
                //to calculate new values on the fly
                //.ForMember(c => c.EndDate, opt => opt.ResolveUsing(example => example.EventDate.AddDays(example.Length - 1)));
            });

            //only initialize data if project is built running instead of entity framework tooling
            if (Configuration["DesignTime"] != "true")
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var userInitializer = scope.ServiceProvider.GetRequiredService<UserDataInitializer>();
                    userInitializer.Seed().Wait();
                    var cardInitializer = scope.ServiceProvider.GetRequiredService<CardDataInitializer>();
                    cardInitializer.Seed().Wait();
                }
            }

            //enable authentication
            app.UseAuthentication();

            //enable statuscode modification
            app.UseStatusCodePages();
            //enable mvc
            app.UseMvc();

        }
    }
}
