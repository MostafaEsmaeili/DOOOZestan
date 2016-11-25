using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Service;

namespace Doozestan.GameData.InternalService.Tournament
{
   public interface ITournamentService : IService<Domain.Tournament>
   {
       void AddTournament(Domain.Tournament tournament);
   }
}
