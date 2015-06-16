using PwdGen.Model;

namespace PwdGen.Infrastructure
{
    internal interface IFileManager
    {
        void Save(Container container, string path);
        Container Load(string path);
        bool IsExist(string path);
        void SaveHistory(Container container, string path);
    }
}
