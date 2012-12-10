using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace NameToNumbers
{
    class NametoNumbers
    {
        static string name = "shri";
        static int MaxDigits = 10;
        private static double totalExecutions = 0;

        //Constructor
        public NametoNumbers()
        {
        }

        //Regex to check if the string does not contain numbers or special characters
        private static bool isValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        static void Main(string[] args)
        {
            double keypadNumberValue = 0;
            int[] keypadDictionary = new int[26];

            /*Dictionary that maps the characters to numbers assuming a to be at 0th position and z to be at 25th- the values in the array at that position
              are the corresponding numbers on a phone's keypad when the characters are entered - an alternate approach would be to use a dictionary object */
            keypadDictionary[0] = keypadDictionary[1] = keypadDictionary[2] = 2;
            keypadDictionary[3] = keypadDictionary[4] = keypadDictionary[5] = 3;
            keypadDictionary[6] = keypadDictionary[7] = keypadDictionary[8] = 4;
            keypadDictionary[9] = keypadDictionary[10] = keypadDictionary[11] = 5;
            keypadDictionary[12] = keypadDictionary[13] = keypadDictionary[14] = 6;
            keypadDictionary[15] = keypadDictionary[16] = keypadDictionary[17] = keypadDictionary[18] = 7;
            keypadDictionary[19] = keypadDictionary[20] = keypadDictionary[21] = 8;
            keypadDictionary[22] = keypadDictionary[23] = keypadDictionary[24] = keypadDictionary[25] = 9;

            //Convert Name into its corresponding numerical value. if there are characters thta are no in a-z or A-z it sets the name to InvalidInputString
            if (isValid(name.ToLower()))
            {
                foreach (char a in name.ToLower())
                {
                    keypadNumberValue = keypadNumberValue * 10 + keypadDictionary[a - 'a'];
                }
            }
            else
            {
                keypadNumberValue = 0;
                name = "InvalidInputString";
            }


            //Writes the Execution Summary to a File 
            using (System.IO.StreamWriter testBenchFile = new System.IO.StreamWriter(@Convert.ToString(name + "-ExecutionSummary.txt")))
            {
                if (name.Length > MaxDigits || name.Length == 0 || keypadNumberValue == 0)
                    testBenchFile.WriteLine("Unable to calculate phone numbers as the input text is empty or has more than 10 characters or special characters. The name should only have a-z in it");
                else
                {
                    //execution time tracking harness
                    DateTime begin = DateTime.UtcNow;

                    Double TotalNumbersfound = PrefixValue(MaxDigits, keypadNumberValue, name);

                    DateTime end = DateTime.UtcNow;

                    testBenchFile.WriteLine("Name: {0}  Converted Value: {1}", name, keypadNumberValue);
                    testBenchFile.WriteLine("Total NUmber of Phone numbers found: {0}", totalExecutions);
                    testBenchFile.WriteLine("Started at: {0}", begin.ToString());
                    testBenchFile.WriteLine("Ended at: {0}", end.ToString());
                    testBenchFile.WriteLine("Measured time for Execution: {0}s.", (end - begin).TotalSeconds);
                }
            }
        }


        /// <summary>
        ///     Identifies the various numbers that can be prefixed to the converted value
        ///     Prepends the values to converted value and sends it to the SuffixValue method
        ///     eg: if the converted value is 998 , it sends over 998, 1998, 2998.....9998, 10998, 11998 etc
        /// </summary>
        /// <param name="MaxDigits"></param>
        /// <param name="keypadNumberValue"></param>
        /// <param name="name"></param>


        private static double PrefixValue(int MaxDigits, double keypadNumberValue, string name)
        {
            int fileNumber = 1;
            //Console.WriteLine("Name: {0}  Converted Value: {1}", name, keypadNumberValue);
            double PhoneNumberCount = 0;
            System.IO.StreamWriter file;
            for (int i = 0; i < MaxDigits - name.Length + 1; i++)
            {
                using (file = File.AppendText(@Convert.ToString(name + "-PhoneNumbers-" + fileNumber + ".txt")))
                {
                    double prefixedVal = 0;
                    for (; PhoneNumberCount < Math.Pow(10, i); PhoneNumberCount++)
                    {
                        prefixedVal = PhoneNumberCount * Math.Pow(10, name.Length) + keypadNumberValue;
                        SuffixValue(MaxDigits, prefixedVal, Convert.ToString(prefixedVal), file);
                    }
                }

                /******** For PAGING - increases the counter after the inner loop of entries so that contents get saved to the next file
                * Comment this out if you want all numbers in the same file
                * This increases execution due to opening and closing a number of files and should be used only when needed. ***********/

                //if (PhoneNumberCount % 1000 == 0 && PhoneNumberCount != 0)
                //{
                //    fileNumber++;
                //    file.Close();
                //}
            }
            return PhoneNumberCount;

        }


        /// <summary>
        ///     Identifies the various numbers that can be suffixed to the value from the PrefixValue Method
        ///     Appends the values to input value and writes it to file
        ///     eg: if the received value is 1998 and MaxLength is 5 , it writes 19980. 19981.....19989 to file
        /// </summary>
        /// <param name="MaxDigits"></param>
        /// <param name="keypadNumberValue"></param>
        /// <param name="name"></param>
        /// <param name="file"></param>


        private static void SuffixValue(int MaxDigits, double keypadNumberValue, string name, System.IO.StreamWriter file)
        {
            int curLen = MaxDigits;
            if (curLen < name.Length) return;
            else
            {
                double startvalue = keypadNumberValue * (Math.Pow(10, (curLen - name.Length)));
                for (double i = 0; i < Math.Pow(10, (curLen - name.Length)); i++)
                {

                    if (startvalue < Math.Pow(10, MaxDigits - 1))
                        //This should never be hit
                        throw new System.InvalidOperationException("value is less than 10 digits");
                    else
                    {
                        totalExecutions++;
                        file.Write(startvalue + ",");
                    }
                    startvalue++;
                }
            }
        }


    }
}
