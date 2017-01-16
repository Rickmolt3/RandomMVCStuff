using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Web.Configuration;
using projectForTestingStuff.Models;
using projectForTestingStuff.Controllers;




namespace projectForTestingStuff.Controllers.DataControllers
{
    public class UserDataController
    {
        public static SqlDatabase db;

        public UserDataController(string connectionstring)
        {

            db = new SqlDatabase(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

            if (connectionstring.Length > 0)
            {
                if (db == null)
                {
                    db = new SqlDatabase(connectionstring);
                }
            }
            else
            {
                if (db == null)
                {
                    db = new SqlDatabase(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                }
            }

        }

        public List<UserModel> checkUser(string emailAddress, string password)
        {
            // Readies stored proc from server.
            DbCommand user = db.GetStoredProcCommand("spLogin");

            db.AddInParameter(user, "@appUserEmail", DbType.String, emailAddress);
            db.AddInParameter(user, "@appUserPassword", DbType.String, password);

            // Executes stored proc to return values into a DataSet.
            DataSet ds = db.ExecuteDataSet(user);

            var userstuff = (from drRow in ds.Tables[0].AsEnumerable()
                          select new UserModel()
                          {

                              emailAddress = drRow.Field<string>("appUserEmail"),
                              //password = drRow.Field<string>("appUserPassword")
                              
                          }).ToList();

            return userstuff;
        }
    }
}