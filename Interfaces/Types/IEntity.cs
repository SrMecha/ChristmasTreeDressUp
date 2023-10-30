namespace ChristmasTreeDressUp.Interfaces.Types
{
    public interface IEntity
    {
        public string Tag { get; init; }
        public Bitmap Image { get; set; }
        public Rectangle Bounds { get; set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public void Dispose();
        public void Update();
        public void OnClick();
    }
}
