using CsvHelper.Configuration.Attributes;
namespace RogerTechTest.Models
{
    public class Source
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(2)]
        public string Code { get; set; }
    }
}
