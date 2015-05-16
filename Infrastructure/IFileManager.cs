using System;
using System.IO;
using System.Xml.Linq;
using PwdGen.Model;

namespace PwdGen.Infrastructure
{
    interface IFileManager
    {
        void Save(Container container, string path);
        Container Load(string path);
        bool IsExist(string path);
        void SaveHistory(Container container, string path);
    }
}
