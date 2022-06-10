using System.Text.RegularExpressions;

namespace LEVEL2_ADN_PROCESSING_API.Service
{
    public class DNAProcessingService
    {
        public DNAProcessingService()
        {

        }

        private int HorizontalRevision(string[,] dna, int count)
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
                    return Convert.ToInt16(count);
            }
            return count;
        }

        public string[,] CastToDNAMatrix(List<string> val)
        {
            int len = val[0].Length;
            string[,] dna = new string[len, len];
            for (int i = 0; i < len; i++)
            {
                var res = val[i].Select(x => new string(x, 1)).ToArray();
                for (int j = 0; j < len; j++)
                {
                    dna[i, j] = res[j];

                }
            }
            return dna;
        }

        private int VerticalRevision(string[,] dna, int count)
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

        private int ObliqueRevision(string[,] dna, int count)
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

        //TODO MAKE THIS METHOD ASYNC FOR PARALLELL PROCESS
        public int IsMutant(string[,] dna)
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
