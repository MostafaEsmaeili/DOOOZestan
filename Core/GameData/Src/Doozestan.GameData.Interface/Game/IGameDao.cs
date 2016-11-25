using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DataAccess.Repositories;

namespace Doozestan.GameData.InternalService.Game
{
    public interface IGameDao : IDao<Domain.Game>
    {
        Domain.Game GetGames(int id);
        List<Doozestan.Domain.Game> GetAllGame();
        List<Domain.Game> GetGameByUserId(string userId);
        void UpdateGame(Domain.Game game);
    }
}
