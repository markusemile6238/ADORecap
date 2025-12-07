using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ADOFromConsole.Configuration
{
    public class DbAdoCommand 
    {
        private readonly DbCommand _dbCommand;

        public DbAdoCommand(DbCommand dbCommand)
        {
            _dbCommand = dbCommand;
        }
        #region Properties
        public int CommandTimeOut
        {
            get => _dbCommand.CommandTimeout;
            set => _dbCommand.CommandTimeout = value;
        }

        public string CommandText
        {
            get => _dbCommand.CommandText;
            set => _dbCommand.CommandText = value;
        }
        #endregion

        #region Methodes
        public void AddParameter(string name, DbType type, Object? value) 
        {
            var parameter = _dbCommand.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value ?? DBNull.Value;
            _dbCommand.Parameters.Add(parameter);
        }

        public void ClearParameter()
        {
            _dbCommand.Parameters.Clear();
        }

        internal DbCommand GetDbCommand() => _dbCommand;
        #endregion

      
       

    }
}
