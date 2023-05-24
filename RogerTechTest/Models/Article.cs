using System.Xml;
using System.Xml.Serialization;

namespace RogerTechTest.Models
{
    [XmlRoot("document")]
    public class Article
    {
        [XmlAttribute("id_site")]
        public string Source { get; set; }

        [XmlElement("local_time")]
        public string Published { get; set; }

        [XmlElement("direct_url")]
        public string Url { get; set; }

        [XmlElement("header")]
        public Title Title { get; set; }

        [XmlElement("body")]
        public Body Content { get; set; }

        public string BodyText => string.Join(" ", Content.P.Select(x => x.Value).ToList());

        [XmlElement("topics")]
        public Topic Topic { get; set; }

        public bool IsCorrect { get; set; }
    }

    public class Text
    {
        [XmlElement("text")]
        public string Value { get; set; }
    }

    public class Title : Text
    {

    }

    public class Body
    {
        [XmlElement("p")]
        public List<Paragraph> P { get; set; }
    }

    public class Paragraph : Text
    {

    }

    public class Topic
    {
        [XmlElement("topic")]
        public List<string> Value { get; set; }
    }
}
