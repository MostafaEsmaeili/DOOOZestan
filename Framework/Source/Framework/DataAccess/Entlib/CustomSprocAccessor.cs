using System.Collections.Generic;
using System.Data.Common;
using Framework.Extensions;
using Framework.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Repository.Pattern;

namespace Framework.DataAccess.Entlib
{
    public class CustomSprocAccessor<TResult> : SprocAccessor<TResult>
    {
        protected DbCommand Command { get; set; }
        protected IRowMapper<TResult> RowMapper { get; set; }

        public CustomSprocAccessor(Database database, DbCommand command, IRowMapper<TResult> rowMapper)
            : base(database, command.CommandText, rowMapper)
        {
            Command = command;
            RowMapper = rowMapper;
        }

        public override IEnumerable<TResult> Execute(params object[] parameterValues)
        {
            return ExecutePagedCollection().Result;
        }

        public PagedCollection<TResult> ExecutePagedCollection()
        {
            var pagedCollection = new PagedCollection<TResult>();

            using (var reader = Database.ExecuteReader(Command))
            {
                pagedCollection.Result = new List<TResult>();

                while (reader.Read())
                    pagedCollection.Result.Add(RowMapper.MapRow(reader));

                if (reader.NextResult() && reader.Read())
                {
                    pagedCollection.TotalRecords = reader.SafeReader(reader.GetName(0)).SafeInt(0);
                }
                   
            }

            return pagedCollection;
        }
    }
}
