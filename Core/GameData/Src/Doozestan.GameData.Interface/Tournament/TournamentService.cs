using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Logging;
using Framework.Service;

namespace Doozestan.GameData.InternalService.Tournament
{
   public class TournamentService : Service<Domain.Tournament,ITournamentDao>,ITournamentDao
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);

        public void AddTournament(Domain.Tournament tournament)
       {
            try
            {
                Dao.AddTournament(tournament);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
    }
}
