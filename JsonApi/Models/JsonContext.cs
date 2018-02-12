using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonApi.Models
{
    public class JsonContext : DbContext
    {
        public JsonContext(DbContextOptions<JsonContext> options):base(options)
        {

        }

        public DbSet<JsonItem> JsonItems { get; set; }
    }
}
