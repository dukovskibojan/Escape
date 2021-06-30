using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Escape.Models
{
    public class Art
    {
        [Key]
        public int id { get; set; }
        [Display (Name = "Name of the art")]
        public string artName { get; set; }
        public string artUrl { get; set; }
        [Display(Name = "Description about the art")]
        public string desc { get; set; }
        public virtual Creator creator { get; set; }
    }
}