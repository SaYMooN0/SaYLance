namespace SaYLance.interfaces
{
    public interface IDefaultTextIO
    {
        public Task<string> StringInputAsync();
        public void Clear();
        public void Log(string message);
        public void Error(string error);
        public void Warning(string warning);
    }
}
