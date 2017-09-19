using System;
using System.Collections.Generic;
using System.Text;
using CourseWork.BusinessLogicLayer.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class ConfiguresExtension
    {
        private static readonly IConfigurationRoot _configuration;

        //public static void AddMappers(this IServiceCollection services)
        //{
        //    services.Configure<MailOptions>(options => Configuration.GetSection("MailOptions").Bind(options));
        //    services.Configure<CloudinaryOptions>(options =>
        //        Configuration.GetSection("CloudinaryOptions").Bind(options));
        //}
    }
}
