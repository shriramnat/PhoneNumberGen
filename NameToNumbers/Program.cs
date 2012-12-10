using System;
using System.Text;
namespace NameToNumbers
{
    class NametoNumbers
    {
        static void Main(string[] args)
        {
            string name = "Vishal";
            int MaxDigits = 10;

            /*Dictionary that mapps the characters to numbers assuming a to be at oth position and z to be at 25th- the values in the array at that position
              are the corresponding numbers on a phone's keypad when the characters are entered - an alternate approach would be to use a dictionary object */

            int[] keypadDictionary = new int[26];
            keypadDictionary[0] = keypadDictionary[1] = keypadDictionary[2] = 2;
            keypadDictionary[3] = keypadDictionary[4] = keypadDictionary[5] = 3;
            keypadDictionary[6] = keypadDictionary[7] = keypadDictionary[8] = 4;
            keypadDictionary[9] = keypadDictionary[10] = keypadDictionary[11] = 5;
            keypadDictionary[12] = keypadDictionary[13] = keypadDictionary[14] = 6;
            keypadDictionary[15] = keypadDictionary[16] = keypadDictionary[17] = keypadDictionary[18] = 7;
            keypadDictionary[19] = keypadDictionary[20] = keypadDictionary[21] = 8;
            keypadDictionary[22] = keypadDictionary[23] = keypadDictionary[24] = keypadDictionary[25] = 9;

            double keypadNumberValue = 0;
            foreach (char a in name.ToLower())
            {
                keypadNumberValue = keypadNumberValue * 10 + keypadDictionary[a - 'a'];
            }

            //tracking execution time
            using (System.IO.StreamWriter testBenchFile = new System.IO.StreamWriter(@"TestBench.txt"))
            {
                DateTime begin = DateTime.UtcNow;
                testBenchFile.WriteLine("Started at: " + begin.ToString());

                PrefixValue(MaxDigits, keypadNumberValue, name);
                
                DateTime end = DateTime.UtcNow;
                testBenchFile.WriteLine("Ended at: " + end.ToString());
                testBenchFile.WriteLine("Measured time for Execution: " + (end - begin).TotalSeconds + " s.");
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

        private static void PrefixValue(int MaxDigits, double keypadNumberValue, string name)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@Convert.ToString(name + "-PhoneNumbers.txt")))
            {
                Console.WriteLine("Name: {0}  Converted Value: {1}", name, keypadNumberValue);
                double j = 0;
                for (int i = 0; i < MaxDigits - name.Length + 1; i++)
                {
                    double prefixedVal = 0;
                    for (; j < Math.Pow(10, i); j++)
                    {
                        prefixedVal = j * Math.Pow(10, name.Length) + keypadNumberValue;
                        //Console.WriteLine("Sending value:" + prefixedVal);
                        SuffixValue(MaxDigits, prefixedVal, Convert.ToString(prefixedVal), file);
                    }
                }

            }
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
                    file.Write(startvalue + ",");
                    startvalue++;
                }
            }   
        }

    }
}
