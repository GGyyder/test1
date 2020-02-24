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
        /// <summary>
        /// 新增product (sql)
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public string Add_Product(List<Products> products)
        {
            SqlTransaction sqltrans = default(SqlTransaction);
            string storeporcedure = "Add_Product";


            using (SqlConnection conn = new SqlConnection((new Cls_SqlConnStr()).ConnStr))
            {
                conn.Open();
                sqltrans = conn.BeginTransaction();
                try
                {
                    foreach (Products product in products)
                    {
                        using (SqlCommand sqlcommand = new SqlCommand(storeporcedure, conn))
                        {
                            sqlcommand.Transaction = sqltrans;
                            sqlcommand.CommandType = CommandType.StoredProcedure;
                            sqlcommand.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = product.ProductName;
                            sqlcommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = product.SupplierID;
                            sqlcommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = product.CategoryID;
                            sqlcommand.Parameters.Add("@QuantityPerUnit", SqlDbType.NVarChar).Value = product.QuantityPerUnit;
                            sqlcommand.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = product.UnitPrice;
                            sqlcommand.Parameters.Add("@UnitsInStock", SqlDbType.SmallInt).Value = product.UnitsInStock;
                            sqlcommand.Parameters.Add("@UnitsOnOrder", SqlDbType.SmallInt).Value = product.UnitsOnOrder;
                            sqlcommand.Parameters.Add("@ReorderLevel", SqlDbType.SmallInt).Value = product.ReorderLevel;
                            sqlcommand.Parameters.Add("@Discontinued", SqlDbType.Bit).Value = product.Discontinued;
                            sqlcommand.ExecuteNonQuery();
                        }
                    }
                    sqltrans.Commit();
                    return "S";
                }
                catch (Exception ex)
                {
                    sqltrans.Rollback();
                    return ex.Message;
                }

            }



        }



    }
}