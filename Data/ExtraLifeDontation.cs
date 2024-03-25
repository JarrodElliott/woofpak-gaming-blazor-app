using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoofpakGamingSiteServerApp.Data
{
    public class ExtraLifeDontation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double DonationAmount { get; set; }

        [Required]
        public DateTime DontationTime { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public ApplicationUser WoofpakUser { get; set; }

        [ForeignKey("ExtraLifeTeamId")]
        public ExtraLifeTeam Team { get; set; }

        [ForeignKey("ExtraLifeEventId")]
        public ExtraLifeEvent Event { get; set; }

        [ForeignKey("ExtraLifeParticipantId")]
        public ExtraLifeParticipant Participant { get; set; }

        public string WoofpakKey { get; set; }


    }
}
