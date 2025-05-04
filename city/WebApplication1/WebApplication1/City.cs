using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Reflection;

namespace WebApplication1
{
    [Table("city")]
    public class City : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("population_size")]
        public int Population_size { get; set; }
    }
}
