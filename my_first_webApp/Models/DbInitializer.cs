using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_first_webApp.Models
{
    public class DbInitializer
    {
        public static void Initialize(TodoContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
