using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public partial class Products
    {
        public string returnStr { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class Cls_Product
    {
         



        public List<Products> GetAllProduct()
        {
            DataTable Dt = new DataTable();
            List<Products> products = new List<Products>();
            using (SqlConnection conn = new SqlConnection(new Cls_SqlConnStr().ConnStr))
            {
                conn.Open();
                string sqlstr = "select * from [Products] ";

                using (SqlDataAdapter sqladapt = new SqlDataAdapter(sqlstr, conn))
                {
                    sqladapt.SelectCommand.CommandType = CommandType.Text;
                    sqladapt.Fill(Dt);

              

                }
            }

            DataRow Darow = default(DataRow);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Darow = Dt.Rows[i];
                    Products product = new Products();
                    product.ProductID = Convert.ToInt32(Darow["ProductID"].ToString());
                    product.ProductName = Darow["ProductName"].ToString();
                    product.SupplierID = Convert.ToInt32(Darow["SupplierID"].ToString());
                    product.CategoryID = Convert.ToInt32(Darow["CategoryID"].ToString());
                    product.QuantityPerUnit = Darow["QuantityPerUnit"].ToString();
                    product.UnitPrice = Convert.ToDecimal(Darow["UnitPrice"].ToString());
                    product.UnitsInStock = Convert.ToInt16(Darow["UnitsInStock"].ToString());
                    product.UnitsOnOrder = Convert.ToInt16(Darow["UnitsOnOrder"].ToString());
                    product.ReorderLevel = Convert.ToInt16(Darow["ReorderLevel"].ToString());
                    product.Discontinued= Convert.ToBoolean(Darow["Discontinued"].ToString());
                    products.Add(product);

                }


            }
            return products;


        }


        public Products GetProduct(string productID)
        {
            DataTable Dt = new DataTable();
            Products product = new Products();
            try
            {
                using (SqlConnection conn = new SqlConnection(new Cls_SqlConnStr().ConnStr))
                {
                    conn.Open();
                    string sqlstr = "Sel_Product";

                    using (SqlDataAdapter sqladapt = new SqlDataAdapter(sqlstr, conn))
                    {
                        sqladapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqladapt.SelectCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = Convert.ToInt32(productID);
                        sqladapt.Fill(Dt);



                    }
                }

                DataRow Darow = default(DataRow);
                if (Dt.Rows.Count > 0)
                {

                    Darow = Dt.Rows[0];

                    product.ProductID = Convert.ToInt32(Darow["ProductID"].ToString());
                    product.ProductName = Darow["ProductName"].ToString();
                    product.SupplierID = Convert.ToInt32(Darow["SupplierID"].ToString());
                    product.CategoryID = Convert.ToInt32(Darow["CategoryID"].ToString());
                    product.QuantityPerUnit = Darow["QuantityPerUnit"].ToString();
                    product.UnitPrice = Convert.ToDecimal(Darow["UnitPrice"].ToString());
                    product.UnitsInStock = Convert.ToInt16(Darow["UnitsInStock"].ToString());
                    product.UnitsOnOrder = Convert.ToInt16(Darow["UnitsOnOrder"].ToString());
                    product.ReorderLevel = Convert.ToInt16(Darow["ReorderLevel"].ToString());
                    product.Discontinued = Convert.ToBoolean(Darow["Discontinued"].ToString());
                    product.returnStr = "S";
                    return product;

                }
                else
                {
                    product.returnStr = "F";
                    product.ErrorMsg = "查無此產品";

                }
                return product;
            }
            catch (Exception ex)
            {
                product.returnStr = "F";
                product.ErrorMsg = ex.Message.ToString();
                return product;
            }

        }




    }
}