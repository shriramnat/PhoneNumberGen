using System;
using System.Text;
namespace NameToNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "abc";
            int MaxDigits = 10;

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
            using (System.IO.StreamWriter testBenchFile = new System.IO.StreamWriter(@"TestBench.txt"))
            {
                DateTime begin = DateTime.UtcNow;
                testBenchFile.Write("Started at: " + begin.ToString());

                PrefixValue(MaxDigits, keypadNumberValue, name);
                
                DateTime end = DateTime.UtcNow;
                testBenchFile.WriteLine("\n Ended at: " + end.ToString() + "\n");
                testBenchFile.WriteLine("\n\nMeasured time for the last iteration: \t" + (end - begin).TotalSeconds + " s. \n\n");
            }
        }

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

        private static void PrefixValue(int MaxDigits, double keypadNumberValue, string name)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@Convert.ToString(name+"-PhoneNumbers.txt")))
            {
                Console.WriteLine("Name: {0}  Converted Value: {1}", name, keypadNumberValue);
                double j = 0;
                for (int i = 0; i < MaxDigits - name.Length + 1; i++)
                {
                    double testingval = 0;
                    for (; j < Math.Pow(10, i); j++)
                    {
                        testingval = j * Math.Pow(10, name.Length) + keypadNumberValue;
                        //Console.WriteLine("Sending value:" + testingval);
                        SuffixValue(MaxDigits, testingval, Convert.ToString(testingval), file);
                    }
                }

            }
        }


    }
}
