using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoastMaster.Api.Models
{
    public class CoffeeModel
    {
        public int ID;

        [StringLength(100, MinimumLength=3)]
        [Display(Name = "Coffee")]
        public int Name;

        [StringLength(100, MinimumLength=3)]
        [Display(Name = "Species")]
        public string SpeciesName;
        
        [Display(Name = "Classification")]
        public string DisplayName
        {
            get
            {
                return SpeciesName + " : " + Name;
            }
        }

    }
}