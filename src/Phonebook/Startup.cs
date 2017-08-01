using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Phonebook.Models;
using AutoMapper;
using Phonebook.ViewModels;
using FluentValidation.AspNetCore;

namespace Phonebook
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhonebookContext>();
            services.AddScoped<IPhonebookRepository, PhonebookRepository>();
            services.AddTransient<PhonebookDatabaseSeeder>();
            services.AddMvc()
                    .AddFluentValidation(fvc =>
                        fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                ILoggerFactory loggerFactory,
                                PhonebookDatabaseSeeder seeder)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                config.CreateMap<ContactTag, TagViewModel>()
                      .ForMember(viewModel => viewModel.TagId, src => src.MapFrom(contactTag => contactTag.TagId))
                      .ForMember(viewModel => viewModel.TagName, src => src.MapFrom(contactTag => contactTag.Tag.TagName));

                config.CreateMap<TagViewModel, ContactTag>()
                      .ForMember(contactTag => contactTag.Tag, src => src.MapFrom(viewModel => new Tag
                       {
                           TagId = viewModel.TagId,
                           TagName = viewModel.TagName
                       }));

                config.CreateMap<ContactViewModel, Contact>()
                    .ForMember(contact => contact.ContactTags, src => src.MapFrom(viewModel => viewModel.Tags))          
                    .AfterMap((viewModel, model) =>
                    {
                        foreach (var contactTag in model.ContactTags)
                            contactTag.ContactId = viewModel.ContactId;
                    });

                config.CreateMap<Contact, ContactViewModel>()
                   .ForMember(viewModel => viewModel.Tags, x => x.MapFrom(contact => contact.ContactTags.Select(tag => tag.Tag).ToArray()));

                config.CreateMap<EmailViewModel, Email>().ReverseMap();
                config.CreateMap<PhoneViewModel, Phone>().ReverseMap();
                config.CreateMap<TagViewModel, Tag>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsurePhonebookSeedData();
        }
    }
}
