using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FRCScout.Models
{
    public class Match
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }

        public string Name { get; set; }

        public DateTime ScheduledTime { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public IList<MatchParticipant> Participants { get; set; }

        public int UnaccountedBluePenalties { get; set; }

        public int UnaccountedRedPenalties { get; set; }
    }
}