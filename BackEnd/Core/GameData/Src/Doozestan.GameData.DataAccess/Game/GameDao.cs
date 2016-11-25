using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Doozestan.Domain;
using Doozestan.GameData.InternalService.Game;
using Framework.DataAccess;
using Framework.DataAccess.Entlib;
using Framework.Logging;

namespace Doozestan.GameData.Dao
{
    public class GameDao : AbstractDao<Domain.Game>,IGameDao
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);
        public GameMapper GameMapper { get; set; }

        public Domain.Game GetGames(int id)
      {
            try
            {
                var command = Entlib.GetStoredProcCommand("dbo.GetGamesById");
                Entlib.AddInParameter(command, "Id", DbType.Int32, id);
                var items = Entlib.ExecuteCommandAccessor(command, GameMapper).ToList();
                return items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
      }

        public List<Game> GetAllGame()
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("dbo.GetAllGame");
                var items = Entlib.ExecuteCommandAccessor(command, GameMapper).ToList();
                return items;
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public List<Domain.Game> GetGameByUserId(string userId)
      {
            try
            {
                var command = Entlib.GetStoredProcCommand("dbo.GetGameByUserId");
                Entlib.AddInParameter(command, "UserID", DbType.String, userId);
                var items = Entlib.ExecuteCommandAccessor(command, GameMapper).ToList();
                return items;
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
                var command = Entlib.GetStoredProcCommand("dbo.UpdateGame");
                Entlib.AddInParameter(command, "Id", DbType.Int32, game.Id);
                Entlib.AddInParameter(command, "WinnerId", DbType.String, game.WinnerId);
                Entlib.AddInParameter(command, "Status", DbType.String, game.Status);
                Entlib.AddInParameter(command, "EndDate", DbType.DateTime, game.EndDate);
                Entlib.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
    }
}
