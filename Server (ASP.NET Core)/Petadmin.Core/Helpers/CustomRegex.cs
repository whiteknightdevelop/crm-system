using System.Text.RegularExpressions;

namespace Petadmin.Core.Helpers
{
    public static class CustomRegex
    {
        public static readonly Regex PassportRegex = new Regex("^[a-zA-Z0-9<]+$");
        public static readonly Regex NumericRegex = new Regex("(^-?[0-9]+$)|(^$)");
        public static readonly Regex NumericEmptyRegex = new Regex("^[0-9]*$");
        public static readonly Regex NumericEmptyOrLength7Regex = new Regex("(^0$)|(^([0-9]{7})$)");
        public static readonly Regex AlphabeticRegex = new Regex("^[a-zA-Z\\u0590-\\u05fe\\s\\']+$");
        public static readonly Regex AlphanumericHebrewRegex = new Regex("^[a-zA-Z\\u0590-\\u05fe0-9 ]+$");
        public static readonly Regex AlphanumericHebrewPrescriptionRegex = new Regex("^[a-zA-Z\\u0590-\\u05fe0-9-\\(\\)%\\/+']+[a-zA-Z\\u0590-\\u05fe0-9-\\(\\)%\\/+ .']+$");
        public static readonly Regex AlphanumericRegex = new Regex("^[a-zA-Z0-9]+$");
        public static readonly Regex PhoneNumberRegex = new Regex("^0(([2-4]{1}-\\d{7})|([8-9]{1}-\\d{7})|([5]{1}\\d-\\d{7})|([7]{1}\\d-\\d{7})|([2-4]{1}\\d{7})|([8-9]{1}\\d{7})|([5]{1}\\d\\d{7})|([7]{1}\\d\\d{7}))$");
        public static readonly Regex EmailRegex = new Regex("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
        public static readonly Regex NumericDecimal = new Regex("^(\\d{2}[.]?\\d?)|(0.0)$");
        public static readonly Regex NumericPulse = new Regex("(^[1-9]{1}[0-9]{1,2}$)|(^0$)|(^$)");
        public static readonly Regex NumericDecimalWeight = new Regex("(^[1-9]{1}[0-9]{0,2}$)|(^[1-9]{1}[0-9]{0,2}[.]{1}[0-9]{1,3}$)|(^[0]{1}[.]{1}[0-9]{1,3}$)|(^0$)|(^$)");
        public static readonly Regex NumericDecimalTemperature = new Regex("(^[1-9]{1}[0-9]{1}[.]{1}[0-9]{1}$)|(^0$)|(^0.0$)|(^$)");
        public static readonly Regex MobileNumberRegex = new Regex("(^05[0-9]{1}[2-9]{1}[0-9]{6}$)");
    }
}
