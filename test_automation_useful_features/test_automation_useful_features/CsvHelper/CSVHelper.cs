using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace test_automation_useful_features.Helpers.CSVHelper
{
    public static class CsvFileHelper
    {
        // more details here https://joshclose.github.io/CsvHelper/getting-started
        public static IEnumerable<T> ReadObjFromCsv<T>(string fullFilePath)
        {
            using (var reader = new StreamReader(fullFilePath))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
        }

        public static void WriteObjToCsv<T>(IEnumerable<T> objects, string fullFilePath)
        {
            using (var writer = new StreamWriter(fullFilePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(objects);
            }
        }
    }
}
