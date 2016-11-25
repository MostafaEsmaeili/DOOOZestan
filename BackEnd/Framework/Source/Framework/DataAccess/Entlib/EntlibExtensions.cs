using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Repository.Pattern;

namespace Framework.DataAccess.Entlib
{
    public static class EntlibExtensions
    {
     
        public static CustomSprocAccessor<TResult> CreateCommandAccessor<TResult>(this Database database, DbCommand command, IRowMapper<TResult> rowMapper)
        {
            if (command == null)
                throw new ArgumentException("procedureCommand should not be null");
            else
                return new CustomSprocAccessor<TResult>(database, command, rowMapper);
        }

        /// <summary>
        /// Executes a stored procedure and returns the result as an enumerable of <typeparamref name="TResult"/>.
        /// 
        /// </summary>
        /// <typeparam name="TResult">The element type that will be returned when executing.</typeparam>
        /// <param name="database">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.Database"/> 
        /// that contains the command.</param><param name="command">The command that will be executed.</param>
        /// <param name="rowMapper">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.IRowMapper`1"/> 
        /// that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// An enumerable of <typeparamref name="TResult"/>.
        /// </returns>
        public static IEnumerable<TResult> ExecuteCommandAccessor<TResult>(this Database database, DbCommand command, IRowMapper<TResult> rowMapper) where TResult : new()
        {
            return CreateCommandAccessor(database, command, rowMapper).Execute();
        }

        /// <summary>
        /// this implementation should be encapsulated in some CustomSprocAccessor<TR1, TR2> but at this time i have no enough time to do 
        /// this method executes multiple result sets 
        /// </summary>
        /// <typeparam name="TResult1"></typeparam>
        /// <typeparam name="TResult2"></typeparam>
        /// <param name="database"></param>
        /// <param name="command"></param>
        /// <param name="rowMapper1"></param>
        /// <param name="rowMapper2"></param>
        /// <returns></returns>
        public static ArrayList ExecuteCommandAccessorMultipleResultSets<TResult1, TResult2>(this Database database, DbCommand command, IRowMapper<TResult1> rowMapper1, IRowMapper<TResult2> rowMapper2)
        {
            var finalResult = new ArrayList(2);
            
            var result1List = new List<TResult1>();
            var result2List = new List<TResult2>();
            finalResult.Add(result1List);
            finalResult.Add(result2List);            

            using (var reader = database.ExecuteReader(command))
            {                
                while (reader.Read())
                    result1List.Add(rowMapper1.MapRow(reader));

                if (reader.NextResult())
                    while (reader.Read())
                        result2List.Add(rowMapper2.MapRow(reader));
            }

            return finalResult;
        }

        /// <summary>
        /// Executes a stored procedure and returns the result as an object of <typeparamref name="TResult"/>.
        /// 
        /// </summary>
        /// <typeparam name="TResult">The element type that will be returned when executing.</typeparam>
        /// <param name="database">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.Database"/> 
        /// that contains the stored procedure.</param><param name="procedureName">The name of the stored procedure that will be executed.</param>
        /// <param name="parameterValues">The parameter values thst should be passed to the stored procedure.</param>
        /// <param name="rowMapper">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.IRowMapper`1"/> 
        /// that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// A paged collection of <typeparamref name="TResult"/>.
        /// </returns>
        public static TResult ExecuteSprocAccessorObject<TResult>(this Database database, string procedureName, IRowMapper<TResult> rowMapper, params object[] parameterValues) where TResult : new()
        {
            return database.ExecuteSprocAccessor(procedureName, rowMapper, parameterValues).FirstOrDefault();
        }

        /// <summary>
        /// Executes a stored procedure and returns the result as an object of <typeparamref name="TResult"/>.
        /// 
        /// </summary>
        /// <typeparam name="TResult">The element type that will be returned when executing.</typeparam>
        /// <param name="database">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.Database"/> 
        /// that contains the stored procedure.</param><param name="procedureName">The name of the stored procedure that will be executed.</param>
        /// <param name="parameterValues">The parameter values thst should be passed to the stored procedure.</param>
        /// <param name="resultSetMappers">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.IResultSetMapper`1"/> 
        /// that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// A paged collection of <typeparamref name="TResult"/>.
        /// </returns>
        public static TResult ExecuteSprocAccessorObject<TResult>(this Database database, string procedureName, IResultSetMapper<TResult> resultSetMappers, params object[] parameterValues) where TResult : new()
        {
            return database.ExecuteSprocAccessor(procedureName, resultSetMappers, parameterValues).FirstOrDefault();
        }

        /// <summary>
        /// Executes a stored procedure and returns the result as a paged collection of <typeparamref name="TResult"/>.
        /// 
        /// </summary>
        /// <typeparam name="TResult">The element type that will be returned when executing.</typeparam>
        /// <param name="database">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.Database"/> 
        /// that contains the stored procedure.</param><param name="command">The name of the stored procedure that will be executed.</param>
        /// <param name="rowMapper">The <see cref="T:Microsoft.Practices.EnterpriseLibrary.Data.IRowMapper`1"/> 
        /// that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        /// <returns>
        /// Paged collection of <typeparamref name="TResult"/>.
        /// </returns>
        public static PagedCollection<TResult> ExecutePagedCommandAccessor<TResult>(this Database database, DbCommand command, IRowMapper<TResult> rowMapper) where TResult : new()
        {
            return CreateCommandAccessor(database, command, rowMapper).ExecutePagedCollection();
        }

        public static IDbDataParameter AddUdtDictionaryParameter(this Database Entlib, DbCommand command, string udtName, string parameterName, IEnumerable<KeyValuePair<string, string>> values)
        {
            var param = new SqlParameter(parameterName, SqlDbType.Structured)
            {
                TypeName = udtName
            };
            var dt = new DataTable();
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            if (values == null)
            {
                param.Value = dt;
                command.Parameters.Add(param);
                return param;
            }

            foreach (var value in values)
            {
                var row = dt.NewRow();
                row["Key"] = value.Key;
                row["Value"] = value.Value;
                dt.Rows.Add(row);
            }

            param.Value = dt;

            command.Parameters.Add(param);

            return param;
        }

        public static IDbDataParameter AddEnumerableParameter<T>(this Database Entlib, DbCommand command, string udtName,
            string parameterName, IEnumerable<T> values, string fieldName)
        {
            var param = new SqlParameter(parameterName, SqlDbType.Structured)
            {
                TypeName = udtName
            };
            var dt = new DataTable();
            dt.Columns.Add(fieldName, typeof (T));
            if (values == null)
            {
                param.Value = dt;
                command.Parameters.Add(param);
                return param;
            }

            foreach (var value in values)
            {
                var row = dt.NewRow();
                row[fieldName] = value;
                dt.Rows.Add(row);
            }

            param.Value = dt;

            command.Parameters.Add(param);

            return param;
        }

        public static IDbDataParameter AddDataTableParameter(this Database Entlib, DbCommand command, string sqlTypeName, string parameterName, DataTable dataTable)
        {
            var param = new SqlParameter(parameterName, SqlDbType.Structured)
            {
                TypeName = sqlTypeName
            };
            param.Value = dataTable;
            command.Parameters.Add(param);
            return param;
        }
    }
}
