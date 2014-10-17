using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FRCScout.Models
{
    public class Robot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RobotId { get; set; }

        public int TeamNumber { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Team Team { get; set; }

        public string AutonomousControlType { get; set; }

        public string ControllerType { get; set; }

        public IList<Picture> Pictures { get; set; }
    }
}