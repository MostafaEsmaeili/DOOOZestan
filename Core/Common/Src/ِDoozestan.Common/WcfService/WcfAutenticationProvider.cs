using System;
using System.Data;
using System.Linq;
using Framework.DataAccess.Entlib;
using Framework.IoC;
using Framework.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Doozestan.Common.WcfService
{
    public class WcfAutenticationProvider
    {

        public static void Update(ServiceLoginEntity loginEntity)
        {
            try
            {
                var q = $"	UPDATE dbo.ServiceLoginProvide SET EncryptedKey = '{loginEntity.TockenKey}', LastSet = '{loginEntity.LastSet}'";
                Database.ExecuteNonQuery(CommandType.Text, q);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ServiceLoginEntity Get()
        {
            try
            {
                var command = Database.GetStoredProcCommand("GeServiceLoginEntity");
                var result = Database.ExecuteCommandAccessor(command, Database.GetMapper(dr => new ServiceLoginEntity
                {
                    LastSet = dr[3].SafeDateTime(),
                    TockenKey = dr[2].SafeString(),
                    Password = dr[1].SafeString(),
                    UserName = dr[0].SafeString(),
                    IP = dr[4].SafeString()

                })).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Database Database => CoreContainer.Container.Resolve<Database>();
    }


}
