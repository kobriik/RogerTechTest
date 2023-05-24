using Newtonsoft.Json;
using RogerTechTest.Models;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RogerTechTest.Services
{
    public class ArticleService
    {
        /// <summary>
        /// Načtení dat 
        /// </summary>
        public IEnumerable<Article> GetData(string path)
        {
            XElement purchaseOrder = XElement.Load(path);
            XmlSerializer serializer = new(typeof(Article));

            return purchaseOrder.Descendants("document").Select(x => (Article)serializer.Deserialize(x.CreateReader()));
        }

        /// <summary>
        /// Konverze dat do jsonu
        /// </summary>
        public string ConvertData2Output(List<Article> articles, bool correct)
        {
            return JsonConvert.SerializeObject(articles.Where(x => x.IsCorrect == correct).Select(x => new
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
