using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DataAccess.Repositories;

namespace Doozestan.GameData.InternalService.Tournament
{
    public  interface ITournamentDao : IDao<Domain.Tournament>
    {
        void AddTournament(Domain.Tournament tournament);
    }
}
