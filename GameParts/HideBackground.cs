using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.GameParts
{
    public class HideBackground : Control
    {
        private int _distanceBetweenCovers = 967;
        private List<BackgroundCover> covers = new();

        public bool IsCovered = false;

        public HideBackground() 
        {

        }

        public void Start()
        {
            for (int i = 0; i < 4; i++)
                covers.Add(new BackgroundCover(new(-Screen.PrimaryScreen!.Bounds.Width - (i * _distanceBetweenCovers), 0),
                    new(-Screen.PrimaryScreen!.Bounds.Width + (Screen.PrimaryScreen!.Bounds.Width - _distanceBetweenCovers) - 100, 0), this));
            foreach (var cover in covers)
            {
                EntityManager.AddEntity(cover);
            }
        }

        public void Stop()
        {
            foreach (var cover in covers)
            {
                cover.Repeat = false;
            }
            covers.Clear();
            IsCovered = false;
        }

        public void WaitForCover()
        {
            var t = Task.Run(() => {
                while (!IsCovered)
                    Thread.Sleep(15);
            });
            t.Wait();
        }
    }
}
