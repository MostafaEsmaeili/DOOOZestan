using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Service;

namespace Doozestan.GameData.InternalService.Game
{
    public interface   IGameService:IService<Domain.Game>
    {
        Domain.Game GetGames(int id);
        List<Domain.Game> GetGameByUserId(string userId);
        void UpdateGame(Domain.Game game);
        List<Doozestan.Domain.Game> GetAllGame();


    }
}
