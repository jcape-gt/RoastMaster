using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    public class RoastType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // Seconds needed to achieve the roast type (for Arabica, elevation 250', 75 degrees).
        // Species instances store necessary time modifiers.

        // Percentage to achieve the given roast type. 0% is green, 100% is earliest charcoal 
        // classification, others lie in between
        public double RoastPercentage { get; set; }
    }
}
