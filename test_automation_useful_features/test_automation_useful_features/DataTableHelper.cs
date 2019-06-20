using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace test_automation_useful_features
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Read value from dataTable by providing a row number and a column name
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowNumber"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<string> ReadData(this DataTable table, string columnName)
        {
            return table.AsEnumerable().Select(row => row[columnName].ToString()).ToList();
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            if (table == null) return null;
            List<T> list = new List<T>();

            foreach (var row in table.AsEnumerable())
            {
                T obj = new T();
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[propertyInfo.Name], propertyInfo.PropertyType));
                    }
                    catch
                    {
                        continue;
                    }
                }

                list.Add(obj);
            }

            return list.Any() ? list : null;
        }
    }
}