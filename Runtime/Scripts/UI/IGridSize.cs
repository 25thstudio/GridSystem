namespace The25thStudio.GridSystem.UI
{
    public interface IGridSize
    {
        int Width { get; }
        int Height { get; }
    }

    internal class DefaultGridSize : IGridSize
    {
        public int Width { get; } = 1;
        public int Height { get; } = 1;
    }
}