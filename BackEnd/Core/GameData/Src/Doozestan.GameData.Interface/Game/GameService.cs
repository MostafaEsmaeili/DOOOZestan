using System;
using System.Collections.Generic;
using Framework.Logging;
using Framework.Service;

namespace Doozestan.GameData.InternalService.Game
{
    public class GameService : Service<Domain.Game,IGameDao>,IGameService
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);

        public Domain.Game GetGames(int id)
        {
            try
            {
               return Dao.GetGames(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message,ex);
                throw;
            }
        }

        public List<Domain.Game> GetGameByUserId(string userId)
        {
            try
            {
                return Dao.GetGameByUserId(userId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public void UpdateGame(Domain.Game game)
        {
            try
            {
                 Dao.UpdateGame(game);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public List<Domain.Game> GetAllGame()
        {
            try
            {
               return Dao.GetAllGame();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
    }
}