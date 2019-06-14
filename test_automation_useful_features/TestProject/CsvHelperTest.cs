using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using test_automation_useful_features;
using test_automation_useful_features.Helpers.CSVHelper;

namespace TestProject
{

    public class CsvHelperTest
    {
        [Test]
        public void Test1()
        {
            List<Server> server = new List<Server>
            {
                new Server { Name = "one" , Family = "oneF", Environment = "oneE", Application = "oneA"},
                new Server { Name = "two", Family = "twoE", Environment = "twoE",  Application = "twoA"},
            };

            var v = CsvFileHelper.ReadObjFromCsv<Server>(
                @"F:\ASP.NET CORE STUDY PROJECTs\test_automation_useful_features\test_automation_useful_features\TestProject\testfile.csv", "|");
        }

        [Test]
        public void Test2()
        {
            var str = FileHelper.ReadFileContent(@"NewFolder1/testfile.csv");
        }
    }
}
