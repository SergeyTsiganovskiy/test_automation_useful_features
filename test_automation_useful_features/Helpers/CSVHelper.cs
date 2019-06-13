using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExaLinkRebatesAndFees.Utility.CommonHelpers
{
    internal class CSVHelper
    {
        private List<Dictionary<string, string>> listObjResult;

        public List<Dictionary<string, string>> ConvertCsvFileToDictionary(string path, char delimiter = ';')
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
                csv.Add(line.Split(delimiter));

            var properties = lines[0].Split(delimiter);

            listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }

            return listObjResult;
        }

        public List<Dictionary<string, string>> ConvertCsvFileToDictionary1(string path, char delimiter = ';')
        {
            var splitedLines = from line in File.ReadAllLines(path)
                select line.Split(delimiter);

            var res = from value in splitedLines.Skip(1)
                      from column in splitedLines.Take(1)
                      select new {Colum = column, Value = value};

            var result = res.ToDictionary(x => x.Colum, x => x.Value);


            return result;
        }
    }
}
