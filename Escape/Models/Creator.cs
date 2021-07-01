using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Escape.Models
{
    public class Creator
    {
        [Key]
        public int id { get; set; }
        [Display (Name = "Creator")]
        public string name { get; set; }
        public Creator() { }
        public string username { get; set; }
        public string email { get; set; }
    }
}