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

        public async Task<int> IsMutant(string[,] dna)
        {
            Task<int> sequenceCount = 0;
            sequenceCount = await Task.Run(() => VerticalRevision(dna, sequenceCount)).Result;
            if (sequenceCount < 2)
                sequenceCount = await Task.Run(() => HorizontalRevision(dna, sequenceCount)).Result;
            if (sequenceCount < 2)
                sequenceCount = await Task.Run(() => ObliqueRevision(dna, sequenceCount)).Result;
            return sequenceCount;
        }
    }
}
