using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FRCScout.Models
{
    public class MatchParticipant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchParticipantId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Match Match { get; set; }

        public int TeamNumber { get; set; }
        public virtual Team Team { get; set; }

        public string Alliance { get; set; }

        public int AutonomousPoints { get; set; }

        public int TeleoperatedPoints { get; set; }

        public int BonusPoints { get; set; }

        public int PenaltyPoints { get; set; }
    }
}