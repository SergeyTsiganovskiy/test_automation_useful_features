using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace test_automation_useful_features.Helpers.CSVHelper
{
    public static class CsvFileHelper
    {
        // more details here https://joshclose.github.io/CsvHelper/getting-started
        public static IEnumerable<T> ReadObjFromCsv<T>(string fullFilePath, string delimiter = ",")
        {
            using (var reader = new StreamReader(fullFilePath))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = delimiter;
                var records = csvReader.GetRecords<T>().ToList();
                return records;
            }
        }

        public static void WriteObjToCsv<T>(IEnumerable<T> objects, string fullFilePath, string delimiter = ",")
        {
            using (var writer = new StreamWriter(fullFilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = delimiter;
                csvWriter.WriteRecords(objects);
            }
        }
    }
}
