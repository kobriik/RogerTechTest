using CsvHelper;
using CsvHelper.Configuration;
using RogerTechTest.Models;
using System.Collections;
using System.Globalization;

namespace RogerTechTest.Services
{
    public class SourceService
    {

        /// <summary>
        /// Načtení dat
        /// </summary>
        public List<Source> GetData(string path)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, configuration))
                {
                    return csv.GetRecords<Source>().ToList();
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Konfigurační soubor nenalezen");
                throw;
            }
        }
    }
}
