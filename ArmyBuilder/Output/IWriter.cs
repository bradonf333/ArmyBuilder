namespace ArmyBuilder.Output
{
    public interface IWriter
    {
        void WriteMessage(string message);
        void ClearMessage();
        void Alert();
        void Information();
        void Default();
        void CustomBG(Color color);
        void CustomFG(Color color);
    }
}
