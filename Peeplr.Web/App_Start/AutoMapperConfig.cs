using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using am = AutoMapper;
using entMappings = Peeplr.Main.Model.Mappers;

namespace Peeplr.Web
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper() 
        {
            entMappings::AutoMapperConfiguration.ConfigureDataToDomainModelMappings();
        }
    }
}