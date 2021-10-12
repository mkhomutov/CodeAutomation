namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using Orc.Csv;
    using System.Globalization;
    using CsvHelper.Configuration;
    using System;

    public class ParseCSV
    {
        private readonly IEnumerable<string> _headers;

        public ParseCSV(string path)
        {
            _headers = GetHeaders(path).Select(x => x[0].ToString().ToUpper() + String.Join("", x.Skip(1)));
        }

        public IEnumerable<string> Headers
        {
            get => _headers;
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
    }
}
