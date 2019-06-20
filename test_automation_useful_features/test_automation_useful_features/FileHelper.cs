using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace test_automation_useful_features
{
    public static class FileHelper
    {
        /// <summary>
        /// Reads JSON file content
        /// </summary>
        /// <param name="fileLocation">Location of the file in project</param>
        /// <returns>JSON string that was read from a file</returns>
        public static string ReadFileContent(string fileLocation)
        {
            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), fileLocation));
        }

        /// <summary>
        /// Reads JSON file content asynchronously
        /// </summary>
        /// <param name="fileLocation">Location of the file in project</param>
        /// <returns>Task with JSON string that was read from a file</returns>
        public static async Task<string> ReadFileContentAsync(string fileLocation)
        {
            using (var reader = File.OpenText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), fileLocation)))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Write a line into file
        /// </summary>
        /// <param name="fileLocation">Location of the file in project</param>
        /// <param name="info">string</param>
        public static void WriteLine(string fileLocation, string info, bool flag = true)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation, append: flag))
            {
                writer.WriteLine(info);
            }
        }

        /// <summary>
        /// Write a line into file with all content before
        /// </summary>
        /// <param name="fileLocation">Location of the file in project</param>
        /// <param name="info">string</param>
        public static void WriteLineWithClearBefore(string fileLocation, string info, bool flag = true)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation, append: flag))
            {
                writer.Write(string.Empty);
                writer.WriteLine(info);
            }
        }

        /// <summary>
        /// Clear file content
        /// </summary>
        /// <param name="fileLocation">Location of the file in project</param>
        public static void ClearFile(string fileLocation)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation))
            {
                writer.Write(string.Empty);
            }
        }

        public static List<Dictionary<string, string>> ParseFileToListOfDicts(string filePath, char separator)
        {
            var fileLines = System.IO.File.ReadAllLines(filePath);
            var headers = fileLines.First().Split(separator);
            var data = fileLines.Skip(1).Select(line => line.Split(separator));
            List<Dictionary<string, string>> fileModel = new List<Dictionary<string, string>>();
            data.ToList().ForEach(dataRow =>
            {
                {
                    var tempDict = new Dictionary<string, string>();
                    for (var i = 0; i < headers.Length; i++)
                    {
                        tempDict.Add(headers[i], dataRow[i]);
                    }
                    fileModel.Add(tempDict);
                }
            });
            return fileModel;
        }

        public static string SerializeListOfDictsToString(List<Dictionary<string, string>> fileModel, char separator)
        {
            var fileString = new StringBuilder();
            fileModel.First().Keys.ToList().ForEach(key => fileString.Append(key).Append(separator));
            fileString.Remove(fileString.Length - 1, 1).Append("\n");
            fileModel.ForEach(dataRow =>
            {
                dataRow.Values.ToList().ForEach(value => fileString.Append(value).Append(separator));
                fileString.Remove(fileString.Length - 1, 1).Append("\n");
            });
            return fileString.ToString();
        }

    }
}