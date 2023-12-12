namespace SaYLance.interfaces
{
    public interface IDefaultTextIO
    {
        public void AllowInput();
        public void Clear();
        public void Log(string message);
        public void Error(string error);
        public void Warning(string warning);
    }
}
