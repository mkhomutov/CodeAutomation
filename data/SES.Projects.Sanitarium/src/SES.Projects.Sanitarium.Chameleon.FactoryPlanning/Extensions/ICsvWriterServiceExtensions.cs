

namespace SES.Projects.Sanitarium.Chameleon
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.Logging;
    using CsvHelper.Configuration;
    using FactoryPlanning;
    using FactoryPlanning.Models;
    using FactoryPlanning.ProjectManagement;
    using Orc.Csv;

    public static class ICsvWriterServiceExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private static readonly string UserName = Environment.UserName;

        public static async Task SaveRecordsAsync<TRecord, TRecordMap>(this ICsvWriterService csvWriterService, IEnumerable<TRecord> records, string fileName)
            where TRecordMap : ClassMap, new()
        {
            try
            {
                var directory = Path.GetDirectoryName(fileName);
                Directory.CreateDirectory(directory);

                var csvContext = new CsvContext<TRecord, TRecordMap>
                {
                    Culture = SanitariumEnvironment.Culture,
                };

                csvContext.Configuration = csvWriterService.CreateDefaultConfiguration(csvContext);

                // Enable this configuration if values contain quotes (e.g. 3" bottle)
                //csvContext.Configuration.IgnoreQuotes = true;

                await csvWriterService.WriteRecordsAsync(records, fileName, csvContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to save records data to '{fileName}'");
                throw;
            }
        }
    }
}
