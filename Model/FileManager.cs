using System.IO;
using System.Xml.Linq;
using PwdGen.Infrastructure;

namespace PwdGen.Model
{
    internal class FileManager : IFileManager
    {
        public void Save(Container container, string path)
        {
            var containerElement = new XElement("Container",
                new XElement("id", container.Id),
                new XElement("pass", container.Pass),
                new XElement("date", container.Date));

            if (IsExist(path))
            {
                var doc = XDocument.Load(path);
                doc.Root.Add(containerElement);
                doc.Save(path);
            }
            else
            {
                new XDocument(new XDeclaration(null, "utf-8", null), containerElement).Save(path);
            }
        }

        public Container Load(string path)
        {
            if (IsExist(path))
            {
                var tmp = (XContainer) XDocument.Load(path).Document.Root.LastNode;
                if (tmp == null)
                    return null;
                return new Container
                {
                    Id = tmp.Element("id").Value,
                    Pass = tmp.Element("pass").Value,
                    Date = tmp.Element("date").Value
                };
            }
            return null;
        }

        public bool IsExist(string path)
        {
            return File.Exists(path);
        }

        public void SaveHistory(Container container, string path)
        {
            using (var appendText = File.AppendText(path))
            {
                appendText.WriteLine("{0} {1} {2}", container.Date, container.Id, container.Pass);
            }
        }
    }
}