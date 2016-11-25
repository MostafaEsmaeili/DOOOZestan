using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Framework.DataAccess.Entlib;
using Framework.IoC;
using Framework.Logging;
using Framework.Utility;

namespace Doozestan.Common.Logging
{
    public class LogDao
    {
        //private CustomLogger Logger
        //{
        //    get
        //    {
        //        return new CustomLogger(this.GetType().FullName);
        //    }
        //}

        private CustomDatabase CustomDatabase
        {
            get
            {
                return CoreContainer.Container.Resolve<CustomDatabase>();
            }
        }

        public class LogEntity
        {
            public virtual Int32 Id { get; set; }
            public virtual DateTime EventDateTime { get; set; }
            public virtual string EventLevel { get; set; }
            public virtual string UserName { get; set; }
            public virtual string MachineName { get; set; }
            public virtual string ErrorSource { get; set; }
            public virtual string ErrorClass { get; set; }
            public virtual string ErrorMethod { get; set; }
            public virtual string InnerErrorMessage { get; set; }
        }

        public List<Log> Get(DateTime fromDate, DateTime toDate)
        {
            var command = CustomDatabase.GetStoredProcCommand("dbo.GetLog");
            CustomDatabase.AddInParameter(command, "fromDate", DbType.DateTime, fromDate.Date);
            CustomDatabase.AddInParameter(command, "toDate", DbType.DateTime, toDate.Date);
            var mapper = CustomDatabase.GetMapper(LogMapperToEntity);
            var list = CustomDatabase.ExecuteCommandAccessor(command, mapper).ToList();
            return list;
        }

        public List<Log> GetById(int Id)
        {
            var command = CustomDatabase.GetStoredProcCommand("dbo.GetLogById");
            CustomDatabase.AddInParameter(command, "Id", DbType.Int32, Id);
            var mapper = CustomDatabase.GetMapper(LogMapperToEntity);
            var list = CustomDatabase.ExecuteCommandAccessor(command, mapper).ToList();
            return list;
        }

        private static Log LogMapperToEntity(IDataReader dr)
        {
            return new Log()
            {
                Id = Int32.Parse(dr["Id"].SafeString()),
                EventLevel = dr["EventLevel"].SafeString(),
                ErrorMethod = dr["ErrorMethod"].SafeString(),
                ErrorSource = dr["ErrorSource"].SafeString(),
                InnerErrorMessage = dr["InnerErrorMessage"].SafeString(),
                UserName = dr["UserName"].SafeString(),
                EventDateTime = dr["EventDateTime"].SafeDateTime(),
                ErrorClass = dr["ErrorClass"].SafeString(),
            };

        }
    }
}

