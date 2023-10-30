using ChristmasTreeDressUp.Forms;
using ChristmasTreeDressUp.GameParts;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class Catcher : Entity
    {
        public readonly FirstGame FirstGame;
        private const int Speed = 30;
        private readonly Bitmap _leftImage = Properties.Resources.Catcher;
        private readonly Bitmap _rightImage;
        private readonly int _maxLeft = Screen.PrimaryScreen!.Bounds.Width - Properties.Resources.Catcher.Width;

        public bool IsMoovingLeft { get; set; } = false;
        public bool IsMoovingRight { get; set; } = false;
        public int Points { get; set; } = 0;

        public Catcher(FirstGame firstGame) 
        {
            FirstGame = firstGame;
            Location = new(_maxLeft / 2, Screen.PrimaryScreen!.WorkingArea.Height - Properties.Resources.Catcher.Height);
            Image = _leftImage;
            Size = _leftImage.Size;
            var bmp = new Bitmap(_leftImage.Width, _leftImage.Height);
            var graphics = Graphics.FromImage(bmp);
            Point[] destinationPoints = {
                new Point(_leftImage.Width, 0),
                new Point(0, 0),
                new Point(_leftImage.Width, _leftImage.Height)};
            graphics.DrawImage(Properties.Resources.Catcher, destinationPoints);
            graphics.Flush();
            _rightImage = bmp;
        }

        public void MoveLeft()
        {
            if (IsMoovingLeft)
            {
                Image = _leftImage;
                Left -= Speed;
                if (Left < 0)
                    Left = 0;
            }
        }

        public void MoveRight()
        {
            if (IsMoovingRight) 
            {
                Image = _rightImage;
                Left += Speed;
                if (Left > _maxLeft)
                    Left = _maxLeft;
            } 
        }

        public bool IsCatched(Point point)
        {
            return point.X > Location.X && point.X < Location.X + Size.Width && point.Y > Location.Y && point.Y < Location.Y + 20;
        }

        public void CheckOver()
        {
            if (Points > 10)
                FirstGame.IsGameEnded = true;
        }

        public override void Update()
        {
            MoveLeft();
            MoveRight();
        }
    }
}
