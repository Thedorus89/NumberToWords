using System;

/*
 Author : Theodorus Danang Wicaksono
 */
namespace NumberToWords
{
    class EnglishNumberToWords
    {
        private string[] numberArr = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
            "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        private string[] tensArr = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private string[] suffixesArr = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

        public Boolean isValidDigit(int lenWholeNo)
        {
            int maxDigit = (suffixesArr.Length) * 3;
            if (lenWholeNo > maxDigit)
                throw (new MaxDigitException(maxDigit.ToString()));
            else
                return true;
        }
        public void WordsCurrency(Currency cur)
        {
            string groupWords = "";

            string cents = WordCent(cur.PointNumber);
            string dollars = WordDollar(cur.WholeNumber, cur.PointNumber);

            if (dollars == "")
                groupWords = cents;
            else
            {
                if (cents == "")
                    groupWords = dollars;
                else
                    groupWords = dollars + " and " + cents;
            }

            if (cur.NumberCurrency < 0)
                groupWords = "negative " + groupWords;

            cur.WordCurrency = groupWords.ToUpper();
        }

        private string WordCent(int pointNo)
        {
            string cents = "";
            if (pointNo == 1)
                cents = numberArr[1] + " cent"; 
            else if (pointNo > 0)
                cents = TwoDigitGroupToWords(pointNo) + " cents";

            return cents;
        }

        private string WordDollar(decimal wholeNo, int pointNo)
        {
            string dollars = "";
            if (wholeNo == 0)
            {
                if (pointNo == 0)
                    dollars = numberArr[0] + " dollars";
                else
                    dollars = "";
            }
            else if (wholeNo == 1)
                dollars = numberArr[1] + " dollar";
            else
                dollars = ScaleGroup(wholeNo) + " dollars";

            return dollars;
        }

        private string ScaleGroup(decimal wholeNo)
        {
            string combined = "";
            string sNumber = wholeNo.ToString();
            int totalGroup = (int)Math.Ceiling((double)sNumber.Length / 3);

            int[] digitGroups = ScaleGroupNumber(sNumber, totalGroup);
            combined = ScaleGroupText(digitGroups, totalGroup);

            return combined;
        }
        private int[] ScaleGroupNumber(string sNumber, int totalGroup)
        {
            int[] digitGroups = new int[totalGroup];

            for (int i = 0; i < totalGroup; i++)
            {
                if (i < totalGroup - 1)
                {
                    digitGroups[i] = int.Parse(sNumber.Substring(sNumber.Length - 3, 3));
                    sNumber = sNumber.Remove(sNumber.Length - 3);
                }
                else
                {
                    digitGroups[i] = int.Parse(sNumber);
                }
            }

            return digitGroups;
        }

        private string ScaleGroupText(int[] digitGroups, int totalGroup)
        {
            string[] groupText = new string[totalGroup];
            string combined = "";

            for (int i = 0; i < totalGroup; i++)
                groupText[i] = ThreeDigitGroupToWords(digitGroups[i]);

            combined = groupText[0];

            for (int i = 1; i < totalGroup; i++)
            {
                if (digitGroups[i] != 0)
                {
                    string prefix = groupText[i] + " " + suffixesArr[i];

                    if (combined.Length != 0)
                        prefix += " ";

                    combined = prefix + combined;
                }
            }

            return combined;
        }

        private string ThreeDigitGroupToWords(int threeDigits)
        {
            string groupText = "";

            int hundreds = threeDigits / 100;
            int tensUnits = threeDigits % 100;

            if (hundreds != 0)
            {
                groupText += numberArr[hundreds] + " hundred";

                if (tensUnits != 0) 
                    groupText += " and ";
            }

            groupText += TwoDigitGroupToWords(tensUnits);

            return groupText;
        }

        private string TwoDigitGroupToWords(int twoDigits)
        {
            string groupText = "";

            int tens = twoDigits / 10;
            int units = twoDigits % 10;

            if (tens >= 2)
            {
                groupText += tensArr[tens];
                if (units != 0)
                    groupText += "-" + numberArr[units]; 
            }
            else if (twoDigits != 0)
                groupText += numberArr[twoDigits];

            return groupText;
        }
    }
}
