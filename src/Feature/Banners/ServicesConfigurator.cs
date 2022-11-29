using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeanObrien.Feature.Banners.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;


namespace DeanObrien.Feature.Banners
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IImageTaxonomy, ImageTaxonomyWithMediaProtection>();
            serviceCollection.AddScoped<ILinkTaxonomy, LinkTaxonomy>();
        }
    }
}