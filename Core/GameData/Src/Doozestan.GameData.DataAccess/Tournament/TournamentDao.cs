using System;
using System.Data;
using Doozestan.GameData.InternalService.Tournament;
using Framework.DataAccess;
using Framework.Logging;

namespace Doozestan.GameData.Dao
{
    public class  TournamentDao :AbstractDao<Domain.Tournament>, ITournamentDao
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);

        public void AddTournament(Domain.Tournament tournament)
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("dbo.AddTournament");
                Entlib.AddInParameter(command, "GameId", DbType.Int32, tournament.GameId);
                Entlib.AddInParameter(command, "UserId", DbType.String, tournament.UserId);
                Entlib.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message,ex);
                throw;
            }
        }
    }
}
