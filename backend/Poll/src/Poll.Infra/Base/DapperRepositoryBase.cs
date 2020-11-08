using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Tnf.Configuration;
using Tnf.Dapper;
using Tnf.Repositories;

namespace Poll.Infra.Base
{
    public class DapperRepositoryBase<TDbContext> : IDapperRepository
        where TDbContext : DbContext
    {
        private readonly IDapperProvider _dapperProvider;

        public DapperRepositoryBase(IDapperProvider dapperProvider)
        {
            _dapperProvider = dapperProvider;
        }

        public virtual DbConnection Connection
        {
            get { return (DbConnection)_dapperProvider.GetActiveConnection(typeof(TDbContext)); }
        }

        public static bool HasNext<T>(T request, int totalResult)
            where T : Tnf.Dto.RequestAllDto
        {
            var totalPage = (totalResult + request.PageSize - 1) / request.PageSize;
            var hasNext = request.Page < totalPage;
            return hasNext;
        }
        public static void CreatePagination(int page, int pageSize, StringBuilder sqlQuery, DynamicParameters dynamicParameters)
        {
            sqlQuery.Append(" OFFSET(@page - 1) * @pageSize ROWS ");
            sqlQuery.Append(" FETCH NEXT @pageSize ROWS ONLY ");

            dynamicParameters.Add("page", page);
            dynamicParameters.Add("pageSize", pageSize);
        }
    }


    public interface IDapperRepository
    {
    }

    public interface IDapperProvider
    {
        IDbConnection GetActiveConnection(Type type);
    }

    public class DapperSqlServerProvider : IDapperProvider, IDisposable
    {
        private readonly ITnfConfiguration _tnfConfiguration;
        private readonly DapperOptions _dapperOptions;
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        private IDbConnection _dbConnection;

        public DapperSqlServerProvider(ITnfConfiguration tnfConfiguration, DapperOptions dapperOptions, IActiveTransactionProvider activeTransactionProvider)
        {
            _tnfConfiguration = tnfConfiguration;
            _dapperOptions = dapperOptions;
            _activeTransactionProvider = activeTransactionProvider;
        }


        public IDbConnection GetActiveConnection(Type type)
        {
            var activeConnection = (DbConnection)_activeTransactionProvider.GetActiveConnection(new ActiveTransactionProviderArgs(type));

            if (activeConnection != null)
            {
                if (activeConnection.State == ConnectionState.Closed)
                    activeConnection.Open();

                return activeConnection;
            }

            var defaultNameOrConnectionString = _tnfConfiguration.DefaultNameOrConnectionString;

            if (string.IsNullOrWhiteSpace(defaultNameOrConnectionString))
                defaultNameOrConnectionString = _dapperOptions.DefaultNameOrConnectionString;

            _dbConnection = new SqlConnection(defaultNameOrConnectionString);
            return _dbConnection;
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }
    }

    public class DapperPostgreProvider : IDapperProvider, IDisposable
    {
        private readonly ITnfConfiguration _tnfConfiguration;
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        private IDbConnection _dbConnection;

        public DapperPostgreProvider(ITnfConfiguration tnfConfiguration, IActiveTransactionProvider activeTransactionProvider)
        {
            _tnfConfiguration = tnfConfiguration;
            _activeTransactionProvider = activeTransactionProvider;
        }

        public IDbConnection GetActiveConnection(Type type)
        {
            try
            {
                _dbConnection = (DbConnection)_activeTransactionProvider.GetActiveConnection(new ActiveTransactionProviderArgs(type));

                _dbConnection = new NpgsqlConnection(_tnfConfiguration.DefaultNameOrConnectionString);
            }
            catch (Exception ex)
            {

            }

            return _dbConnection;
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }
    }
}
