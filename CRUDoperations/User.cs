using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CRUDoperations
{
    internal class User
    {
        [Column("user_id")]
        public int Id {  get; set; }
        public string? Name { get; set; }

        public Company? Company { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
    }

    [NotMapped]
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
