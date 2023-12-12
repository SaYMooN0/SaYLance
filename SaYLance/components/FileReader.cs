namespace SaYLance.components
{
    internal class FileReader : IDisposable
    {
        private readonly string _path;

        public FileReader(string filePath)
        {
            _path = filePath;
        }
        public string GetFirstLine()
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                return reader.ReadLine();
            }
        }
        public void Dispose() { }
    }
}
