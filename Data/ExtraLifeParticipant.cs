using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data
{
    public class ExtraLifeParticipant
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string DisplayName{ get; set; }

        [Required]
        public int NumDonations { get; set; }

        [Required]
        public double SumDonations { get; set; }

        public bool IsStreamLive { get; set; }

        [Required]
        public double FundraisingGoal { get; set; }

        [Required]
        public string AvatarImageURL { get; set; }

        [Required]
        public DateTime CreatedDateUTC { get; set; }

        
        [ForeignKey("ExtraLifeEventId")]
        public ExtraLifeEvent Event { get; set; }

        [ForeignKey("ExtraLifeTeamId")]
        public ExtraLifeTeam Team { get; set; }
    }

}
