using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data
{
    public class ExtraLifeTeam
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ExtraLifeEventId")]
        public ExtraLifeEvent Event { get; set; }
    }
}
