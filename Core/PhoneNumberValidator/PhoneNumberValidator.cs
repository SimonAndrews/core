using PhoneNumbers;

namespace Core.PhoneNumberValidator
{
    public class PhoneNumberValidator
    {
        private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

        public static string FormatNumberByCountryCode(string number, string countryCode)
        {
            number = number.Replace("+", string.Empty);
            var protoNumber = PhoneUtil.Parse(number, countryCode);
            var formattedNumber = PhoneUtil.Format(protoNumber, PhoneNumberFormat.E164);
            return formattedNumber;
        }
    }
}
