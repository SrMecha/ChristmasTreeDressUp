using ChristmasTreeDressUp.Interfaces.Types;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class Entity : IEntity
    {
        protected bool disposed = false;
        private Rectangle _bounds = new();

        public Rectangle Bounds
        {
            get => _bounds;
            set => _bounds = value;
        }
        public Size Size
        {
            get => _bounds.Size;
            set => _bounds.Size = value;
        }
        public Point Location
        {
            get => _bounds.Location;
            set => _bounds.Location = value;
        }
        public int Left
        {
            get => _bounds.Left;
            set => _bounds.X = value;
        }
        public int Top
        {
            get => _bounds.Top;
            set => _bounds.Y = value;
        }

        public Bitmap Image { get; set; }
        public string Tag { get; init; }

        public Entity(Bitmap image, Size size, Point location, string tag = "None")
        {
            Tag = tag;
            Size = size;
            Image = image;
            Location = location;
        }

        public Entity()
        {
            Tag = "None";
            Size = new();
            Location = new();
            Image = null!;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Image.Dispose();
                }
                disposed = true;
            }
        }

        public virtual void Update()
        {

        }

        public virtual void OnClick()
        {

        }

        ~Entity()
        {
            Dispose(disposing: false);
        }
    }
}
