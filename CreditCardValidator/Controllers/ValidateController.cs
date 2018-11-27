using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using CreditCardValidator.Models;
using CreditCardValidator.Utils;

namespace CreditCardValidator.Controllers
{
    /// <summary>
    /// Controller for validation
    /// </summary>
    public class ValidateController : ApiController
    {
        /// <summary>
        /// Do validation of credit card
        /// </summary>
        /// <param name="card">The Card is object containing information about card</param>
        /// <returns>validation result</returns>
        public ValidateResult Check(Card card)
        {
            ValidateResult result = new ValidateResult();

            // validate for input parameters
            if (card.CardNom <= 0 || card.ExpDate == null || card.ExpDate.Length < 6)
            {
                result.Result = "Not all information has been passed for validation";
                return result;
            }

            try
            {                
                // preparing card nom
                string sCardNom = card.CardNom.ToString();

                // convert to datetime
                string ExpDateShort = card.ExpDate.Length > 6 ? card.ExpDate.Substring(0, 6) : card.ExpDate;
                ExpDateShort = "01" + ExpDateShort;
                DateTime expdt = DateTime.MinValue;
                DateTime.TryParseExact(ExpDateShort, "ddMMyyyy", null, DateTimeStyles.None, out expdt);

                //do validate
                ValidationResult vr = Util.Validation(sCardNom, expdt);
                CardType ct = Util.GetCardType(sCardNom);

                // do check on DB
                if (vr == ValidationResult.Valid)
                {
                    string error = "";
                    bool dbexists = DBOperations.CheckCardNom(card.CardNom, out error);
                    if (error != "")
                    {
                        result.Result = error;
                        return result;
                    }
                    vr = dbexists ? ValidationResult.Valid : ValidationResult.DoesNotExists;
                }

                // fill output
                result.CardType = Enum.GetName(typeof(CardType), ct);
                result.Result = Util.GetValidationResultName(vr);
            }
            catch (Exception ee)
            {
                // for some errors
                result.Result = ee.Message;
            }

            return result;
        }

    }
}
