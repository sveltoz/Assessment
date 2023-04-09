using RecipeCalculator.Class;
using RecipeCalculator.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeCalculator
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataManager dbManager = new DataManager();
                try
                {
                    dbManager.Open();
                    DataTable dtIngredient = dbManager.GetDataTable("Select * from Ingredient");
                    foreach (DataRow row in dtIngredient.Rows)
                    {
                        DropDownListIngredient.Items.Add(new ListItem(row["Name"].ToString(), row["ID"].ToString()));
                    }
                    DataTable dtCountry = dbManager.GetDataTable("Select * from Country");

                    foreach (DataRow row in dtCountry.Rows)
                    {
                        DropDownListCountry.Items.Add(new ListItem(row["Name"].ToString(), row["ID"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    dbManager.Close();
                }
                createDataTable();
            }
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            if (ViewState["Recipe"] != null)
            {
                DataManager dbManager = new DataManager();
                Ingredient ing;

                dbManager.Open();
                ing = dbManager.GetIngredientById(int.Parse(DropDownListIngredient.SelectedValue), float.Parse(editBoxQuantity.Text),int.Parse(DropDownListCountry.SelectedValue));
                dbManager.Close();
                DataTable dt = (DataTable)ViewState["Recipe"];
                DataRow drNew = dt.NewRow();
                drNew["ingredient"] = ing.Name;
                drNew["quantity"] = ing.Quantity;
                drNew["rate"] = ing.CountrySpecificPrice + " " + ing.Currency.CurrencyCode + "/" + ing.Unit;
                drNew["grossAmount"] = ing.CountrySpecificGrossAmount;
                drNew["discount"] = ing.CountrySpecificDiscount;
                drNew["tax"] = ing.CountrySpecificTax;
                drNew["total"] = ing.CountrySpecificAmount;
                dt.Rows.Add(drNew);
                ViewState["Recipe"] = dt;
                IngredientListView.DataSource = dt;
                IngredientListView.DataBind();

                Decimal TotalTax = Convert.ToDecimal(dt.Compute("SUM(tax)", string.Empty));
                Decimal TotalDiscount = Convert.ToDecimal(dt.Compute("SUM(discount)", string.Empty));
                Decimal TotalPrice = Convert.ToDecimal(dt.Compute("SUM(total)", string.Empty));

                totalDiv.InnerText = string.Format("Total Cost : {0} {1}", TotalPrice, ing.Currency.CurrencyCode);
                totalDiscountDiv.InnerText = string.Format("Total Discount : {0} {1}", TotalDiscount, ing.Currency.CurrencyCode);
                totalTaxDiv.InnerText = string.Format("Total Tax : {0} {1}", TotalTax, ing.Currency.CurrencyCode);
            }
        }
        private void createDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn ingredient = new DataColumn("ingredient", typeof(string));
            DataColumn quantity = new DataColumn("quantity", typeof(float));
            DataColumn rate = new DataColumn("rate", typeof(string));
            DataColumn grossAmount = new DataColumn("grossAmount", typeof(float));
            DataColumn discount = new DataColumn("discount", typeof(float));
            DataColumn tax = new DataColumn("tax", typeof(float));
            DataColumn total = new DataColumn("total", typeof(float));

            dt.Columns.Add(ingredient);
            dt.Columns.Add(quantity);
            dt.Columns.Add(rate);
            dt.Columns.Add(grossAmount);
            dt.Columns.Add(discount);
            dt.Columns.Add(tax);
            dt.Columns.Add(total);

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            //saving databale into viewstate   
            ViewState["Recipe"] = dt;
            //bind Gridview  
            IngredientListView.DataSource = dt;
            IngredientListView.DataBind();
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            createDataTable();
            totalDiv.InnerText = string.Empty;
            totalDiscountDiv.InnerText = string.Empty;
            totalTaxDiv.InnerText = string.Empty;
        }
    }
}