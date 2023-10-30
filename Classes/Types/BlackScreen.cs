namespace ChristmasTreeDressUp.Classes.Types
{
    public class BlackScreen : Entity
    {
        private readonly Point _zeroCursorImageLocation;
        public BlackScreen()
        {
            Tag = "BlackScreen";
            Image = Properties.Resources.BlackScreen;
            Size = Image.Size;
            _zeroCursorImageLocation = new(-((Size.Width / 2)),
                -((Size.Height / 2)));
            SetLocation(Cursor.Position);
        }

        public void SetLocation(Point mouseLocation)
        {
            Location = new(_zeroCursorImageLocation.X + mouseLocation.X, _zeroCursorImageLocation.Y + mouseLocation.Y);
        }
    }
}
