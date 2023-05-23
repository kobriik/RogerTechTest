using Newtonsoft.Json;
using RogerTechTest.Models;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RogerTechTest.Services
{
    public class DocumentService
    {
        /// <summary>
        /// Načtení dat 
        /// </summary>
        public IEnumerable<Document> GetData(string path)
        {
            XElement purchaseOrder = XElement.Load(path);
            XmlSerializer serializer = new(typeof(Document));

            return purchaseOrder.Descendants("document").Select(x => (Document)serializer.Deserialize(x.CreateReader()));
        }

        /// <summary>
        /// Konverze dat do jsonu
        /// </summary>
        public string ConvertData2Output(List<Document> documents, bool correct)
        {
            return JsonConvert.SerializeObject(documents.Where(x => x.IsCorrect == correct).Select(x => new
            {
                source = x.Source,
                published = x.Published,
                url = x.Url,
                title = x.Title.Value,
                content = x.BodyText,
                topics = x.Topic.Value,
            }));
        }
    }
}
