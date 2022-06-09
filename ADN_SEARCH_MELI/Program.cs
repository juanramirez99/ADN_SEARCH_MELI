using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ADN_SEARCH_MELI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] dna = {
                            { "1", "q", "T", "T", "T", "T"},
                            { "X", "2", "w", "G", "x", "w"},
                            { "C", "Y", "3", "e", "G", "e"},
                            { "3", "C", "d", "C", "r", "e"},
                            { "4", "G", "C", "X", "5", "d"},
                            { "6", "Y", "G", "F", "d", "d"}
                           };
            /*
             * (2,0) -> H     (1,0) -> X    (0,0) -> 1  (0,1) -> W   (0,2) -> A
             * (3,1) -> H     (2,1) -> Y    (1,1) -> 2  (1,2) -> X   (1,3) -> B
             * (4,2) -> H     (3,2) -> W    (2,2) -> 3  (2,3) -> Y   (2,4) -> C
             * (5,3) -> H     (4,3) -> X    (3,3) -> 4  (3,4) -> X   (3,5) -> D
             *                (5,4) -> X    (4,4) -> 5  (4,5) -> W 
             *                              (5,5) -> 6
             *                              
             * inicio  = longitud - 4  ->  6 - 4 = 2
             * 
             */

            int sequenceCount = IsMutant(dna);
            Console.WriteLine(sequenceCount >= 2 ? "IsMutant" : "IsHuman");
            Console.ReadLine();
        }

        public static int HorizontalRevision(string[,] dna, int count)
        {
            int len = dna.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                string tmp = string.Empty;
                for (int j = 0; j < len; j++)
                {
                    tmp += dna[i, j];
                }

                if (tmp.Length > 3)
                {
                    count += Regex.Matches(tmp, "AAAA").Count
                            + Regex.Matches(tmp, "TTTT").Count
                            + Regex.Matches(tmp, "CCCC").Count
                            + Regex.Matches(tmp, "GGGG").Count;
                }

                if (count == 2)
                    return count;
            }
            return count;
        }

        public static int VerticalRevision(string[,] dna, int count)
        {
            int len = dna.GetLength(0);
            for (int j = 0; j < len; j++)
            {
                string tmp = string.Empty;
                for (int i = 0; i < len; i++)
                {
                    tmp += dna[i, j];
                }

                if (tmp.Length > 3)
                {
                    count += Regex.Matches(tmp, "AAAA").Count
                            + Regex.Matches(tmp, "TTTT").Count
                            + Regex.Matches(tmp, "CCCC").Count
                            + Regex.Matches(tmp, "GGGG").Count;
                }
                if (count == 2)
                    return count;
            }
            return count;
        }

        public static int ObliqueRevision(string[,] dna, int count)
        {

            int len = dna.GetLength(0);
            for (int n = -len; n <= len - 4; n++)
            {
                string tmp = string.Empty;
                for (int i = 0; i < len; i++)
                {
                    if ((i - n >= 0) && (i - n < len))
                    {
                        tmp = tmp + dna[i, (i - n)];
                    }
                }
                if (tmp.Length > 3)
                {
                    count += Regex.Matches(tmp, "AAAA").Count
                            + Regex.Matches(tmp, "TTTT").Count
                            + Regex.Matches(tmp, "CCCC").Count
                            + Regex.Matches(tmp, "GGGG").Count;
                }
                if (count == 2)
                    return count;
            }
            return count;
        }

        public static int IsMutant(string[,] dna)
        {
            int sequenceCount = 0;
            sequenceCount = VerticalRevision(dna, sequenceCount);
            if (sequenceCount < 2)
                sequenceCount = HorizontalRevision(dna, sequenceCount);
            if (sequenceCount < 2)
                sequenceCount = ObliqueRevision(dna, sequenceCount);
            return sequenceCount;
        }
    }
}
