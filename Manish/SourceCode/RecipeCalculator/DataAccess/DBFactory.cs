using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;

namespace RecipeCalculator.DataAccess
{
    internal class DBFactory
    {
        private static DbProviderFactory objFactory = null;

        public static DbProviderFactory GetDataProvider(DataProviderType provider)
        {
            switch (provider)
            {
                case DataProviderType.SqlServer:
                    objFactory = SqlClientFactory.Instance;
                    break;
                case DataProviderType.OleDb:
                    objFactory = OleDbFactory.Instance;
                    break;
                case DataProviderType.Odbc:
                    objFactory = OdbcFactory.Instance;
                    break;
            }
            return objFactory;
        }

        public static DbConnection GetConnection(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlConnection();
                case DataProviderType.OleDb:
                    return new OleDbConnection();
                case DataProviderType.Odbc:
                    return new OdbcConnection();
                default:
                    return null;
            }
        }

        public static DbCommand GetCommand(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlCommand();
                case DataProviderType.OleDb:
                    return new OleDbCommand();
                case DataProviderType.Odbc:
                    return new OdbcCommand();
                default:
                    return null;
            }
        }

        public static DbDataAdapter GetDataAdapter(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlDataAdapter();
                case DataProviderType.OleDb:
                    return new OleDbDataAdapter();
                case DataProviderType.Odbc:
                    return new OdbcDataAdapter();
                default:
                    return null;
            }
        }

        public static DbParameter GetProcParameter(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlParameter();
                case DataProviderType.OleDb:
                    return new OleDbParameter();
                case DataProviderType.Odbc:
                    return new OdbcParameter();
                default:
                    return null;
            }
        }
    }
    public enum DataProviderType
    {
       None,SqlServer, OleDb, Odbc
    }
}