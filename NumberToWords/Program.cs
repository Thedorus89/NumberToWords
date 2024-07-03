using System;
using System.Globalization;

/*
 Author : Theodorus Danang Wicaksono
 */
namespace NumberToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            StartingMain();
            Console.Read();
        }

        public static void StartingMain()
        {
            try
            {
                Currency cur = new Currency();
                decimal inputNumber;
                Console.WriteLine("Please enter a number of currency : ");

                if (!decimal.TryParse(Console.ReadLine(), out inputNumber))
                {
                    throw (new DecimalException(""));
                }
                else
                {
                    cur.NumberCurrency = inputNumber;
                    SplitterIntegerDecimal(cur);

                    EnglishNumberToWords englishWords = new EnglishNumberToWords();
                    if (englishWords.isValidDigit(cur.WholeNumber.ToString().Length))
                    {
                        englishWords.WordsCurrency(cur);
                    }

                    Console.WriteLine("\n{0} NUMBERS IN WORDS : \n{1}", cur.NumberCurrency, cur.WordCurrency);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("\nError info : " + ex.Message);
            }
            finally
            {
                WouldYouLikeToRestart();
            }
        }

        public static void WouldYouLikeToRestart()
        {
            Console.WriteLine("\nPress r to restart");
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();

            if (input.KeyChar == 'r')
            {
                Console.Clear();
                StartingMain();
            }
        }
        public static void SplitterIntegerDecimal(Currency cur)
        {
            string sNumber = "";
            if (cur.NumberCurrency < 0)
                sNumber = (cur.NumberCurrency * -1).ToString("0.00", CultureInfo.InvariantCulture);
            else
                sNumber = cur.NumberCurrency.ToString("0.00", CultureInfo.InvariantCulture);

            string[] splitter = sNumber.Split('.');
            cur.WholeNumber = decimal.Parse(splitter[0]);
            cur.PointNumber = int.Parse(splitter[1]);
        }
    }
}
