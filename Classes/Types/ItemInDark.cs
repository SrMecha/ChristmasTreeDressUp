using ChristmasTreeDressUp.GameParts;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class ItemInDark : Entity
    {
        private readonly SecondGame _secondGame;
        public ItemInDark(Bitmap image, bool onLeftSide, SecondGame secondGame)
        {
            _secondGame = secondGame;
            Image = image;
            Size = Image.Size;
            Location = GenerateLocation(onLeftSide);
        }

        private Point GenerateLocation(bool onLeftSide)
        {
            if (onLeftSide)
                return new(Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Width / 2),
                Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Height - Properties.Resources.Lights.Size.Height));
            else
                return new(Random.Shared.Next(Screen.PrimaryScreen!.Bounds.Width / 2, Screen.PrimaryScreen!.Bounds.Width - Image.Width),
                Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Height - Properties.Resources.Lights.Size.Height));
        }

        public override void OnClick()
        {

            if (++_secondGame.ItemFounded >= 5)
                _secondGame.IsGameEnded = true;
            EntityManager.Remove(this);
        }
    }
}
