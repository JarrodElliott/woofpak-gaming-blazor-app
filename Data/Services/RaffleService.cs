using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data.Services
{
    public class RaffleService
    {
        ApplicationDbContext _db;

        public RaffleService()
        {
            _db = new ApplicationDbContext();
        }
        //public Task<List<Tournament>> GetRaffleItems()
        //{
        //    using (_db)
        //    {
        //        //var t = _db.Tournament.Include(t => t.Game).ToList();
        //        //return Task.FromResult(t);
        //    }
        //}

    }
}
