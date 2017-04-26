namespace ArrangerLibrary.Abstractions
{
    public interface IBox
    {
        int Height { get; set; }
        int Width { get; set; }
        int Area { get; }
        int PosX { get; set; }
        int PosY { get; set; }
        bool CanHold(IItem _item);
    }
}
