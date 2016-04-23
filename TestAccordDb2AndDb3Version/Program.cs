using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIMP_v3._0.Enums;
using TestAccordDb2AndDb3Version.version2;
using TestAccordDb2AndDb3Version.version3;

namespace TestAccordDb2AndDb3Version
{
    class Program
    {
        private static string pathLogFile = @"D:\Documents\Work\AIMP3\testReport.txt";

        private static void Main(string[] args)
        {
            //string str1 =
            //    "[ООО \"КарРентПойнт\"^pСанкт-Петербург, пр. Стачек, 37-лит.А^pИНН/КПП 7805506226/780501001^pОГРН 1099847001671^pр/с 40702810793760000011^pк/c 30101810100000000778в Северо-Западный филиал ОАО АКБ \"Росбанк\"^pБИК 044030778]";

            //string str2 =
            //    "[ООО \"КарРентПойнт\"^pСанкт-Петербург, пр. Стачек, 37-лит.А^pИНН/КПП 7805506226/780501001^pОГРН 1099847001671^pр/с 40702810793760000011^pк/с 30101810100000000778 в Северо-Западный филиал ОАО АКБ \"Росбанк\"^pБИК 044030778]";

            //if (str1 != str2)
            //{
            //    for (int iChar1 = 0; iChar1 < str1.Length; iChar1++)
            //    {
            //        for (int iChar2 = 0; iChar2 < str2.Length; iChar2++)
            //        {
            //            if (iChar1 == iChar2)
            //            {
            //                if (str1[iChar1] != str2[iChar2])
            //                {
                                
            //                }
            //            }
            //        }
            //    }
            //}
            if (File.Exists(pathLogFile))
                File.Delete(pathLogFile);

            var three = new VersionThree().Get(TypeReport.DKP, 5);

            var two = new VersionTwo().Get(TypeReport.DKP, 1100);

            WriteLog($"three count value {three.Count}");

            WriteLog($"two count value {two.Count}");

            if (three.Count != two.Count)
            {
                var threeStr = three.Select(x => new {x.Key});

                var twoStr = two.Select(x => new {x.Key});

                List<string> res = threeStr.Except(twoStr).Union(twoStr.Except(threeStr)).Select(x=>x.Key).ToList();

                foreach (var notFound in res.ToArray())
                {
                    WriteLog(" ");
                    WriteLog($"not found: {notFound}");
                }
            }

            WriteLog("-------------------------");

            foreach (KeyValuePair<string, string> iTwo in two)
            {
                var iThree = three.FirstOrDefault(x => x.Key == iTwo.Key);

                //WriteLog($"         {iTwo.Key}           ");

                if (string.IsNullOrWhiteSpace(iThree.Key))
                {
                    WriteLog($"!  NOT EXIST KEY");
                }

                if (string.IsNullOrWhiteSpace(iThree.Value) && string.IsNullOrWhiteSpace(iTwo.Value))
                    continue;
                
                if (iThree.Value != iTwo.Value)
                {
                    WriteLog($"!  twoVALUE [{iTwo.Value}] NOT threeVALUE [{iThree.Value}]");
                }

                //WriteLog($" ");
            }
        }

        private static void WriteLog(string line)
        {
            File.AppendAllLines(pathLogFile, new List<string>() {line});
        }
        
    }
}
