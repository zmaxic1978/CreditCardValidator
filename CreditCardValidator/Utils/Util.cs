using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardValidator.Utils
{
    public enum CardType { Visa, MasterCard, Amex, JCB, Unknown };
    public enum ValidationResult { Valid, Invalid, DoesNotExists };

    public class Util
    {
        /// <summary>
        /// Returns name of result
        /// </summary>
        /// <param name="val">Validation result</param>
        /// <returns>name of validation result</returns>
        public static string GetValidationResultName(ValidationResult val)
        {
            string result = "";
            switch (val)
            {
                case ValidationResult.DoesNotExists: { result = "Does Not Exists"; break; }
                default:
                    { result = Enum.GetName(typeof(ValidationResult), val); break; }
            }
            return result;
        }

        /// <summary>
        /// the function detects prime number 
        /// </summary>
        /// <param name="N">number to detect</param>
        /// <returns>true - is prime, false - is not prime</returns>
        private static bool isPrime(int N)
        {
            for (int i = 2; i < (int)(N / 2); i++)
            {
                if (N % i == 0) return false;
            }
            return true;
        }

        /// <summary>
        /// The function returns the type of a credit card
        /// </summary>
        /// <param name="CardNom">card number</param>
        /// <returns>type of credit card</returns>
        public static CardType GetCardType(string CardNom)
        {
            //1.Visa is a card number starting with 4.
            //2.MasterCard is a card number starting with 5.
            //3.Amex is a card number starting with 34, 37.
            //4.JCB is a card number starting with 3528–3589.
            //5.The card starting with any other numbers is “Unknown”.  
            //6.Only Amex card number has 15 digits, the rest of card types have 16 digits.

            CardType result = CardType.Unknown;
            int len = CardNom.Length;

            if (len == 16 && CardNom.StartsWith("4")) return CardType.Visa;
            if (len == 16 && CardNom.StartsWith("5")) return CardType.MasterCard;
            if (len == 15 && CardNom.StartsWith("34")) return CardType.Amex;
            if (len == 15 && CardNom.StartsWith("37")) return CardType.Amex;
            if (len == 16)
            {
                int tetra_CardNom = 0;
                int.TryParse(CardNom.Substring(0, 4), out tetra_CardNom);
                if (tetra_CardNom >= 3528 && tetra_CardNom <= 3589)
                {
                    return CardType.JCB;
                }
            }
            return result;
        }

        /// <summary>
        /// The function check validation of a credit card
        /// </summary>
        /// <param name="CardNom">card number</param>
        /// <param name="ExpDate">date of expiry</param>
        /// <returns>"Valid" for success result, "Invalid" for unsuccessful result</returns>
        public static ValidationResult Validation(string CardNom, DateTime ExpDate)
        {
            //7. A valid Visa card is the card number where expiry year is a leap year.
            //8. A valid MasterCard card is the card number where expiry year is a prime number.
            //9. Every JCB card is valid.
            //10. If a card number does not exist in the database, it should return “Does not exist". 
            //11. The rest case is “Invalid” card.

            CardType cardtype = GetCardType(CardNom);
            if (cardtype == CardType.Visa && DateTime.IsLeapYear(ExpDate.Year))
                return ValidationResult.Valid;
            if (cardtype == CardType.MasterCard && isPrime(ExpDate.Year))
                return ValidationResult.Valid;
            if (cardtype == CardType.JCB)
                return ValidationResult.Valid;

            return ValidationResult.Invalid;
        }
    }
}