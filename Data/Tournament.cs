using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime TournamentDate { get; set; }
        public string Image { get; set; }

        [Required]
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
