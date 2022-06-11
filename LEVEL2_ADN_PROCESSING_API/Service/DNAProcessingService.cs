using Belgrade.SqlClient;
using Belgrade.SqlClient.SqlDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LEVEL2_ADN_PROCESSING_API.Service
{
    public class DNAProcessingService
    {
        private const string ConnString = "Server =.; Database = ADN_DB; user id = sa; password = 123456; MultipleActiveResultSets = true";
        private IQueryMapper _queryMapper = new QueryMapper(ConnString);
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
            AddStatsRecord(sequenceCount >= 2?1:0);
            return sequenceCount;
        }

        public async void AddStatsRecord(int result)
        {
            ICommand cmd = new Command(ConnString);
            var date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            await cmd.Sql($"INSERT INTO ADNHistory (IsMutant, Date) VALUES ({result},'{date}')").Exec();
        }
        public async Task<Stats> GetRatio()
        {
            List<Stats> result =
            await _queryMapper.Sql
            (
                $@"  SELECT TOP 1
	                (SELECT count(*) FROM  ADNHistory WHERE IsMutant = 1 ) AS Count_Mutant_DNA,
	                (SELECT count(*) FROM  ADNHistory WHERE IsMutant = 0 ) AS count_Human_DNA, 
	                (0.4) AS Ratio
                    FROM ADNHistory"
            )
            .Map<Stats>(
                row => new Stats
                {
                   CountMutantDNA = Convert.ToInt32(row[0]),
                   CountHumanDNA= Convert.ToInt32(row[1]),
                   Ratio = Convert.ToInt32(row[2])
                });

            if (!result.Any())
            {
                return new Stats() {CountHumanDNA =0, CountMutantDNA=0,Ratio =0 };
            }
            return result[0];
        }
    }
}
