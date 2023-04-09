using System.Data.Common;
using System;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.Sockets;
using RecipeCalculator.Class;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace RecipeCalculator.DataAccess
{
    public sealed class DataManager
    {
        private DbConnection dbConnection;
        private string strConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataProviderType dataProviderType = GetProviderType(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);

        public DataManager()
        {

            dbConnection = DBFactory.GetConnection(dataProviderType);
            dbConnection.ConnectionString = strConnectionString;
        }

        public static DataProviderType GetProviderType(string providerType)
        {
            if (providerType.Equals("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase))
                return DataProviderType.SqlServer;
            else if (providerType.Equals("System.Data.OleDb", StringComparison.InvariantCultureIgnoreCase))
                return DataProviderType.OleDb;
            else if (providerType.Equals("System.Data.Odbc", StringComparison.InvariantCultureIgnoreCase))
                return DataProviderType.Odbc;
            else
                return DataProviderType.None;
        }
        public void Open()
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
        }

        public void Close()
        {
            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
        }


        public DbConnection Connection
        {
            get
            {
                return dbConnection;
            }
        }

        public String ConnectionString
        {
            get
            {
                return strConnectionString;
            }
        }

        public DataProviderType DBProvider
        {
            get
            {
                return dataProviderType;
            }
        }

        public DataTable GetDataTable(string sqlString)
        {
            using (DbDataAdapter dbDataAdapter = DBFactory.GetDataAdapter(this.DBProvider))
            {
                try
                {
                    dbDataAdapter.SelectCommand = DBFactory.GetCommand(this.DBProvider);
                    dbDataAdapter.SelectCommand.CommandText = sqlString;
                    dbDataAdapter.SelectCommand.Connection = this.Connection;
                    DataTable dataTable = new DataTable();
                    dataTable.BeginLoadData();
                    dbDataAdapter.Fill(dataTable);
                    dataTable.EndLoadData();
                    return dataTable;
                }

                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public DataTable GetDataTableByID(string procName, int ID)
        {
            using (DbDataAdapter dbDataAdapter = DBFactory.GetDataAdapter(this.DBProvider))
            {
                try
                {

                    dbDataAdapter.SelectCommand = DBFactory.GetCommand(this.DBProvider);
                    dbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dbDataAdapter.SelectCommand.CommandText = procName;
                    dbDataAdapter.SelectCommand.Connection = this.Connection;

                    var parameter = dbDataAdapter.SelectCommand.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.Value = ID;
                    dbDataAdapter.SelectCommand.Parameters.Add(parameter);

                    DataTable dataTable = new DataTable();
                    dataTable.BeginLoadData();
                    dbDataAdapter.Fill(dataTable);
                    dataTable.EndLoadData();

                    return dataTable;
                }

                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public Ingredient GetIngredientById(int id, float quantity,int CountryID)
        {
            Ingredient ingredient;
            DataTable dataTable = GetDataTableByID("GetIngredientInfo", id);

            float price = float.Parse(dataTable.Rows[0]["Price"].ToString());
            string name = dataTable.Rows[0]["Name"].ToString();
            string unit = dataTable.Rows[0]["Unit"].ToString();
            ingredient = new Ingredient(id, name,unit, quantity, price, GetDiscountByIngredientId(id), GetCategoryByIngredientId(id), GetTaxAndSurchargeByIngredientId(id), GetCurrencyByCountryId(CountryID));
            return ingredient;
        }


        public List<Discount> GetDiscountByIngredientId(int id)
        {
            List<Discount> discounts = new List<Discount>();
            DataTable dataTable = GetDataTableByID("GetDiscountById", id);
            foreach (DataRow row in dataTable.Rows)
            {
                discounts.Add(new Discount(int.Parse(row["ID"].ToString()), row["Name"].ToString(), float.Parse(row["Percentage"].ToString())));
            }
            return discounts;
        }
        public List<TaxAndSurcharge> GetTaxAndSurchargeByIngredientId(int id)
        {
            List<TaxAndSurcharge> taxAndSurcharges = new List<TaxAndSurcharge>();
            DataTable dataTable = GetDataTableByID("GetTaxAndSurchargeById", id);
            foreach (DataRow row in dataTable.Rows)
            {
                taxAndSurcharges.Add(new TaxAndSurcharge(int.Parse(row["ID"].ToString()), row["Name"].ToString(), float.Parse(row["Percentage"].ToString())));
            }
            return taxAndSurcharges;
        }
        public List<Category> GetCategoryByIngredientId(int id)
        {
            List<Category> categories = new List<Category>();
            DataTable dataTable = GetDataTableByID("GetCategoryById", id);
            foreach (DataRow row in dataTable.Rows)
            {
                categories.Add(new Category(int.Parse(row["ID"].ToString()), row["Name"].ToString()));
            }
            return categories;
        }

        public Currency GetCurrencyByCountryId(int Countryid)
        {
            
            DataTable dataTable = GetDataTableByID("GetCurrencyInfo", Countryid);
            DataRow row = dataTable.Rows[0];
            Currency currency = new Currency(int.Parse(row["ID"].ToString()), row["CountryName"].ToString(), float.Parse(row["ConversionRate"].ToString()), row["CurrencyCode"].ToString());
            return currency;
        }
    }
}
