using System;
using System.IO;
using System.Xml.Linq;
using PwdGen.Infrastructure;

namespace PwdGen.Model
{
    class FileManager : IFileManager
    {
        public void Save(Container container, string path)
        {
            XElement containerElement = new XElement("Container",
               new XElement("id", container.Id),
               new XElement("pass", container.Pass),
               new XElement("date", container.Date));

            var doc = XDocument.Load(path);
            doc.Root.Add(containerElement);
            doc.Save(path);
        }
        public Container Load(string path)
        {
            if (IsExist(path))
            {
                var tmp = (XContainer)XDocument.Load(path).Document.Root.LastNode;
                if (tmp == null)
                    return null;
                return new Container()
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
            using (var tt = File.AppendText(path))
            {
                tt.WriteLine("{0} {1} {2}", container.Date, container.Id, container.Pass);
                tt.WriteLine(Environment.NewLine);
            }
        }
    }
}
