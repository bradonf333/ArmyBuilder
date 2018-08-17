namespace ArmyBuilder.Writers
{
    public interface IWriter
    {
        void WriteMessage(string message);
        void ClearMessage();
    }
}
