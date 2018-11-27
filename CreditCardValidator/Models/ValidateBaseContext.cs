using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace CreditCardValidator.Models
{
    /// <summary>
    /// Class for Database
    /// </summary>
    public class ValidateBaseContext : DbContext
    {
    }

    /// <summary>
    /// Class for implementation DBContext
    /// </summary>
    public static class DBOperations
    {
        /// <summary>
        /// Check if the card number exists in DataBase
        /// </summary>
        /// <param name="CardNom">card number</param>
        /// <returns>true - if exist; otherwise - false</returns>
        public static bool CheckCardNom(long CardNom, out string error)
        {
            bool result = false;
            error = "";
            using (ValidateBaseContext db = new ValidateBaseContext())
            {
                try
                {
                    db.Database.Connection.Open();
                    DbCommand cmd = db.Database.Connection.CreateCommand();
                    cmd.CommandText = "DoCheck";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("pCardNom", CardNom));
                    var pAmount = new SqlParameter("pAmount", 0) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pAmount);
                    cmd.ExecuteNonQuery();
                    int amount = (pAmount.Value == null) ? 0 : Convert.ToInt32(pAmount.Value);
                    db.Database.Connection.Close();
                    return amount > 0;
                }
                catch (Exception ee)
                {
                    error = ee.Message;
                }
            }
            return result;
        }
    }
}