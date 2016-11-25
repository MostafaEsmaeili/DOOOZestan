using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doozestan.Domain;
using Doozestan.Domain.Enum;
using Doozestan.GameData.InternalService.Game;
using Doozestan.GameData.InternalService.Tournament;
using Framework.IoC;
using Framework.Logging;

namespace Doozestan.Api
{
    public class GameProvider
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);
        public IGameService GameService => CoreContainer.Container.Resolve<IGameService>();
        public ITournamentService TournamentService => CoreContainer.Container.Resolve<ITournamentService>();

        public Game GetGames(int id)
        {
            try
            {
                return GameService.GetGames(id);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public Game addNewGame(Game game)
        {
            try
            {
                game.StartDate=DateTime.Now;
                game.Status = (int?) GameStatus.Start;
              
                
                GameService.Insert(game);
                return game;
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
                return GameService.GetGameByUserId(userId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public void UpdateGame(Game game)
        {
            try
            {
                game.EndDate = DateTime.Now;
                GameService.UpdateGame(game);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
        public List<Domain.Game> GetAllGames( )
        {
            try
            {
                return GameService.GetAllGame();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

  
        public void AddTournament(Domain.Tournament tournament)
        {
            try
            {
                TournamentService.AddTournament(tournament);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

    }
}
