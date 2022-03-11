using Humanizer;

namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using Orc.Csv;
    using System.Globalization;
    using CsvHelper.Configuration;
    using System;
    using System.Data;
    using CsvHelper;
    using System.IO;

    public class CsvReader
    {
        public static List<FieldDetails> ReadFields(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            
            var className = fileName.Singularize(false);


            var fields = new List<FieldDetails>();

            var csvReaderService = new CsvReaderService();

            var configuration = new CsvConfiguration(new CultureInfo("en-AU"))
            {
                Delimiter = ",",
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim
            };

            var csvContext = new CsvContext<object>
            {
                Configuration = configuration
            };

            using var csvReader = csvReaderService.CreateReader(path, csvContext);
            csvReader.Read();
            csvReader.ReadHeader();

            var headers = csvReader.HeaderRecord;

            using var csvDataReader = new CsvDataReader(csvReader);
            var dataTable = new DataTable();

            dataTable.Load(csvDataReader);

            for(var i = 0; i < headers.Length;i++)
            {
                var columnIndex = i;
                var columnValues = dataTable.AsEnumerable().Select(x => x.Field<string>(columnIndex)).ToArray();

                var validPropertyName = headers[i].ToValidPropertyName(className);
                if (validPropertyName.Equals(className))
                {
                    validPropertyName += "Property";
                }

                var fieldType = columnValues.ResolveType(CultureInfo.GetCultureInfo("en-AU"));
                var fieldAlias = headers[i].Equals(validPropertyName) ? null : validPropertyName;

                fields.Add(new FieldDetails(name: headers[i], type: fieldType.ToStringType(), alias: fieldAlias));
            }

            return fields;
        }
    }
}
