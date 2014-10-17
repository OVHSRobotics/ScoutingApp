using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FRCScout.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        public string SchoolName { get; set; }

        public string TeamName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public IList<Robot> Robots { get; set; }
    }
}