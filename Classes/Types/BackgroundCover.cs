
using ChristmasTreeDressUp.GameParts;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class BackgroundCover : Entity
    {
        private Point _repeatLocation;
        private HideBackground _owner;

        public bool Repeat { get; set; } = true;

        public BackgroundCover(Point location, Point repeatLocation, HideBackground owner)
        {
            Tag = "BackgroundCover";
            Size = Screen.PrimaryScreen!.Bounds.Size;
            Location = location;
            Image = ImageHelper.ResizeImage(Properties.Resources.BackgroundCover, Screen.PrimaryScreen!.Bounds.Size);
            _repeatLocation = repeatLocation;
            _owner = owner;
        }

        public override void Update()
        {
            Left += 100;
            if (Left > Screen.PrimaryScreen!.Bounds.Width)
            {
                if (Repeat)
                {
                    Location = _repeatLocation;
                    _owner.IsCovered = true;
                }
                else
                    EntityManager.Remove(this);
            }
        }
    }
}
