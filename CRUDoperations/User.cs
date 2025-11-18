using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
namespace CRUDoperations
{

    [Index("Id", "PhoneNumber")]
    internal class User
    {
        //[Column("user_id")]

        //[Key]
        public int Id {  get; set; }
        [MaxLength(50)]
        //[Required]
        public string? Name { get; set; }

        public string? PhoneNumber {  get; set; }

        //public Company? Company { get; set; }
    }

    //public class Company
    //{
    //    public int Id { get; set; }
    //    public string? Name { get; set; } 
    //}

    //[NotMapped]
    //public class Country
    //{
    //    public int Id { get; set; }
    //    public string? Name { get; set; }
    //}
}
