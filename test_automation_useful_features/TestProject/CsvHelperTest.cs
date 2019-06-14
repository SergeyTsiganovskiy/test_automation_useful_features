using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using test_automation_useful_features.Helpers.CSVHelper;

namespace TestProject
{

    public class CsvHelperTest
    {
        [Test]
        public void Test()
        {
            List<Server> server = new List<Server>
            {
                new Server { Name = "one" , Family = "oneF", Environment = "oneE", Application = "oneA"},
                new Server { Name = "two", Family = "twoE", Environment = "twoE",  Application = "twoA"},
            };

            var v = CsvFileHelper.ReadObjFromCsv<Server>(
                @"F:\ASP.NET CORE STUDY PROJECTs\test_automation_useful_features\test_automation_useful_features\TestProject\testfile.csv");
        }
    }
}
