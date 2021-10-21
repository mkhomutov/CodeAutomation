namespace CodeAutomationConsole.Tests
{
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsvHelper;
    using CsvHelper.Configuration;
    using NUnit.Framework;
    using Orc.Csv;


    public class PropertyNameValidation
    {
        private readonly DataTable _propertyNames;

        public PropertyNameValidation()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var propertyNamesResource = $"{assembly.GetName().Name}.Resources.PropertyNames.csv";

            _propertyNames = LoadResource(propertyNamesResource);

        }

        [Test]
        public void IsValidPropertyName()
        {
            foreach (var row in _propertyNames.AsEnumerable())
            {
                var wrongName = row.Field<string>("In");
                var rightName = row.Field<string>("Out");

                Assert.AreEqual(rightName, wrongName.ToValidPopertyName());
            }
        }

        private static DataTable LoadResource(string resourse)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var dataTable = new DataTable();

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

            using (Stream stream = assembly.GetManifestResourceStream(resourse))
            using (StreamReader reader = new StreamReader(stream))
            using(var csvReader = csvReaderService.CreateReader(reader, csvContext))
            {
                using (var dataReader = new CsvDataReader(csvReader))
                {
                    dataTable.Load(dataReader);
                }
            }

            return dataTable;
        }
    }
}
