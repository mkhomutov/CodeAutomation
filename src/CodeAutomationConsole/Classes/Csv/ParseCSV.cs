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

    public class ParseCSV
    {
        private readonly IEnumerable<string> _headers;
        private readonly string _path;

        public ParseCSV(string path)
        {
            _path = path;
            _headers = GetHeaders(path);
        }

        public IEnumerable<string> Headers
        {
            get => _headers;
        }

        public List<FieldDetails> Details
        {
            get => GetDeatils(_path);
        }

        public FieldDetails GetField(string name)
        {
            return Details.Find(x => string.Equals(name, x.Name));
        }

        private string[] GetHeaders (string path)
        {
            string[] headers;

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

            using (var csvReader = csvReaderService.CreateReader(path, csvContext))
            {
                csvReader.Read();
                csvReader.ReadHeader();
                headers = csvReader.HeaderRecord;
            }

            return headers;
        }

        private List<FieldDetails> GetDeatils(string path)
        {
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

            using (var csvReader = csvReaderService.CreateReader(path, csvContext))
            {
                csvReader.Read();
                csvReader.ReadHeader();

                var headers = csvReader.HeaderRecord;

                using (var csvDataReader = new CsvDataReader(csvReader))
                {
                    var dt = new DataTable();

                    dt.Load(csvDataReader);

                    for(var i =0; i < headers.Length;i++)
                    {
                        var columnValues = dt.AsEnumerable().Select(x => x.Field<string>(i)).ToArray();
                        var validPropertyName = headers[i].ToValidPopertyName();

                        var fieldType = new ParseType(columnValues).Type;
                        var fieldAlias = headers[i].Equals(validPropertyName) ? null : validPropertyName;

                        fields.Add(new FieldDetails(name: headers[i], type: fieldType, alias: fieldAlias));
                    }

                }
            }

            return fields;
        }
    }
}
