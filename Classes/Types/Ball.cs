using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class Ball : Entity
    {
        private const int FallingSpeed = 10;
        private readonly int _maxTop = Screen.PrimaryScreen!.Bounds.Height;
        private Point _catchPoint = new();
        private readonly Catcher _catcher;

        public Point CatchPoint { get { return _catchPoint; } }

        public Ball(Point location, Catcher catcher)
        {
            _catcher = catcher;
            List<Bitmap> ballImages = new()
            {
                Properties.Resources.BlueBall,
                Properties.Resources.YellowBall,
                Properties.Resources.RedBall,
                Properties.Resources.PinkBall
            };
            Image = ballImages[Random.Shared.Next(0, ballImages.Count - 1)];
            Size = Image.Size;
            Location = location;
            _catchPoint = new(Location.X + (Size.Width / 2), Location.Y + Size.Height / 3 * 2);
        }

        public override void Update()
        {
            Top += FallingSpeed;
            _catchPoint.Y += FallingSpeed;
            if (_catcher.IsCatched(CatchPoint))
            {
                _catcher.Points += 1;
                _catcher.CheckOver();
                EntityManager.Remove(this);
            }
            else if (Top > _maxTop)
                EntityManager.Remove(this);
        }
    }
}
