using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;

namespace RevenueCollection.Models
{
    public class SingleBusinessPermitModel
    {

        private string _effectivedate;
        [Required]
        [DisplayName("Effective Date")]
        public string EffectiveDate
        {
            set
            {
                if (DateTime.TryParse(value, out var date))
                {
                    _effectivedate = date.ToString(CultureInfo.CurrentCulture);
                }
            }
            get
            {

                if (DateTime.TryParse(_effectivedate, out var date))
                {
                    date = date.AddMonths(Duration);
                }

                return date.ToString("dd MMMM yyyy");
            }
        }
        [DisplayName("Expiry Date")]
        [Required]

        public string ExpiryDate
        {
            get
            {
                if (DateTime.TryParse(EffectiveDate, out var date))
                {
                    date = date.AddMonths(Duration);
                }

                return date.ToString("dd MMMM yyyy");
            }
        }
        [DisplayName("KRA PIN")]
        public string KRAPIN { get; set; }
        public int Duration { get; set; }
        [DisplayName("Business ID")]
        [Required] public string BusinessID { get; set; }
        [DisplayName("Business Name")]
        [Required] public string BusinessName { get; set; }
        [DisplayName("Business Activity")]
        [Required] public string BusinessActivity { get; set; }
        [DisplayName("Activity Code")]
        [Required] public string ActivityCode { get; set; }

        public string Amount { get; set; }
        [DisplayName("P.O. Box")]
        public string PoBox { get; set; }
        [DisplayName("Plot No")]
        [Required]
        public string PlotNO { get; set; }
        [DisplayName("Road Street")]
        public string RoadStreet { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }

        public string AmountinWords => NumberToWords(Convert.ToInt32(Amount));

        public string Door { get; set; }
        private string _dateofissue;

        [Required]
        public string Occupation { get; set; }
        public string DateOFIssue => DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");

        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[]
                {
                    "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                    "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
                };
                var tensMap = new[]
                    {"zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(words);
        }
    }
}