namespace ArrangerLibrary.Abstractions
{
    public interface IItem
    {
        int Height { get; }
        int Width { get; }
        int Area { get; }
        bool Rotatable { get; set; }
        IItem CreateCopy();
    }
}
