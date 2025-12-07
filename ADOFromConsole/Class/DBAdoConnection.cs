using Microsoft.Extensions.Configuration;
using DotNetEnv;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace ADOFromConsole.Configuration
{
    public class DBAdoConnection : IDisposable
    {
        private readonly string _connectionString;
        private readonly DbConnection _dbconnection;
        private bool _disposed = false;

        public DBAdoConnection()
        {
            // charge le .env
            var projectDir = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.FullName;

            Env.Load(Path.Combine(projectDir, ".env"));

            // charger appensetting
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile(Path.Combine(projectDir, "appsettings.json"), optional: false)
                .Build();

            string raw = config.GetConnectionString("Default") ?? "";

            // remplacement des {} dans la connectionstring
            _connectionString = Regex.Replace(raw, @"\$\{(.+?)\}", match =>
            {
                var key = match.Groups[1].Value;
                return Env.GetString(key) ?? throw new Exception($"Variable '{key}' introuvable dans .env");
            });

            _dbconnection = new SqlConnection(_connectionString);

        }

        #region Methode Connection
        public ConnectionState State => _dbconnection.State;


        public void Open() 
        { 
            if(_dbconnection.State != ConnectionState.Open) _dbconnection.Open();
        }
        public void Close() 
        { 
            if(_dbconnection.State != ConnectionState.Closed) _dbconnection.Close();
        }
        #endregion


        #region ExecuteScalarCommand
        // return one record or null
        public Object? ExecuteScalar(DbAdoCommand command)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));

            var dbCommand = command.GetDbCommand();
            dbCommand.Connection = _dbconnection;
            try
            {
                Open();
                return dbCommand.ExecuteScalar();
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region ExecuteNonQuery
        public int ExcuteNonQuery(DbAdoCommand command)
        {
            if( command == null) throw new ArgumentNullException( nameof(command));
            var dbCommand = command.GetDbCommand();
            dbCommand.Connection = _dbconnection;
            try
            {
                Open();
                return dbCommand.ExecuteNonQuery();

            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region ExecuteReader
        // return a IEnumerable
        public  IEnumerable<T> ExecuteReader<T>(DbAdoCommand command, Func<IDataRecord, T> mapper)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            var dbCommand = command.GetDbCommand();
            dbCommand.Connection = _dbconnection;

            try
            {
                Open();
                using var reader =  dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    yield return mapper(reader);
                }
            }
            finally { Close(); }
        }
        #endregion

        #region ExecuteReaderAsync
        public async IAsyncEnumerable<T> ExecuteReaderAsync<T>(DbAdoCommand command,Func<IDataRecord, T> mapper)
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            if(mapper == null) throw new ArgumentNullException(nameof(mapper));

            var dbCommand = command.GetDbCommand();
            dbCommand.Connection = _dbconnection;

            try
            {
                Open();
                using var reader = await dbCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    yield return mapper(reader);
                }
            }finally { Close(); }
        }
        #endregion


        #region GetDataTable
        public DataTable GetDataTable(DbAdoCommand command)
        {
            if(command == null) throw new ArgumentNullException( nameof(command));
            
            var dbCommand = command.GetDbCommand();
            dbCommand.Connection = _dbconnection;

            var dataTable = new DataTable();

            try
            {
                Open();
                using var adapter = DbProviderFactories.GetFactory(_dbconnection)?.CreateDataAdapter();
                adapter.SelectCommand = dbCommand;
                adapter.Fill(dataTable);
            }finally { Close(); }
            return dataTable;

        }
        #endregion

        public DbAdoCommand CreateCommand()=>new DbAdoCommand(_dbconnection.CreateCommand());

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbconnection.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
