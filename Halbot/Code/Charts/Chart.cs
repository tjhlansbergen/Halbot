namespace Halbot.Code.Charts
{
    public abstract class Chart
    {
        public string Name { get; protected set; }
        public abstract string CreateHTML();
    }
}