using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class Cls_SqlConnStr
    {

        private string connStr;

        public string ConnStr
        {

            get
            {
                SqlConnectionStringBuilder SqlConnBud = new SqlConnectionStringBuilder();//連線字串
                SqlConnBud.DataSource = "DESKTOP-3S4A0OR\\SQLEXPRESS"; //' "PROJECTSERVER" '別名名稱
                SqlConnBud.IntegratedSecurity = true;
                SqlConnBud.InitialCatalog = "Northwind"; // '資料庫名稱
                SqlConnBud.UserID = "DESKTOP-3S4A0OR\\USER";           //'DB帳號
                //SqlConnBud.Password = "";        //  'DB密碼
                SqlConnBud.LoadBalanceTimeout = 300;
                SqlConnBud.ConnectTimeout = 300;
             


                connStr = SqlConnBud.ConnectionString;
                return connStr;

            }

        }
    }
}