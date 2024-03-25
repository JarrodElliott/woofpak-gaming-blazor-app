using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data.Services
{
    public class TournamentService
    {
        public Task<List<Tournament>> GetTournaments()
        {
            using (var context = new ApplicationDbContext())
            {
                //var res = context.Tournament
                //    .Join(context.Game,
                //    T => T.IdGame,
                //    G => G.Id,
                //    (T, G) => new
                //    {
                //        T.Title,
                //        Game = G.Name,
                //        T.TournamentDate,
                //        T.Description,
                //        T.
                //    }
                //    );
                //foreach(var a in res)
                //{
                //    a.
                //}
                var t = context.Tournament.Include(t => t.Game).ToList();
                return Task.FromResult(t);
            }
        }
    }
}
