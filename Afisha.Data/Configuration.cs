using Afisha.Models;
using BLToolkit.Mapping;
using BLToolkit.Mapping.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afisha.Data
{
   public static class DataConfiguration
    {
       public static void Configure()
       {
           FluentConfig.Configure(Map.DefaultSchema).MapingFromType<MovieMap>();
       }
    }

   internal class MovieMap : FluentMap<Movie>
   {
       public MovieMap()
       {
           MapField(x=>x.Id,"Id").PrimaryKey(x => x.Id).Identity();
       }
   }
}
