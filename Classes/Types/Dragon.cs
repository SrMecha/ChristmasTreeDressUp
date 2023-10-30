using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class Dragon : Entity
    {
        private const int Speed = 20;
        private bool _isDroped = false;
        private bool _isMoovingLeft = false;
        private int _dropPoint = 0;
        private readonly Catcher _catcher;

        public bool IsActive { get; set; } = false;

        public Dragon(Catcher catcher)
        {
            _catcher = catcher;
            Image = Properties.Resources.FlyingRightDragon;
            ImageAnimator.Animate(Image, new EventHandler(OnFrameChanged!));
            Size = Image.Size;
            Location = new(-Image.Width, 0);
            GenerateNewDropPoint();
        }

        private void GenerateNewDropPoint()
        {
            _dropPoint = Random.Shared.Next(0, Screen.PrimaryScreen!.Bounds.Width - Image.Width);
        }

        private void OnFrameChanged(object sender, EventArgs args)
        {

        }

        public void WaitForMooving(int time = 300)
        {
            Task.Run(() =>
            {
                IsActive = false;
                Task.Delay(time);
                IsActive = true;
                GenerateNewDropPoint();
                _isDroped = false;
                _isMoovingLeft = !_isMoovingLeft;
                ImageAnimator.StopAnimate(Image, new EventHandler(OnFrameChanged!));
                Image = _isMoovingLeft ? Properties.Resources.FlyingLeftDragon : Properties.Resources.FlyingRightDragon;
                ImageAnimator.Animate(Image, new EventHandler(OnFrameChanged!));
            });
        }

        public void WaitBeforeStart(int time = 7000)
        {
            Task.Run(() =>
            {
                Task.Delay(time);
                IsActive = true;
            });
        }

        public override void Update()
        {
            if (!IsActive)
                return;
            if (_isMoovingLeft)
            {
                Left -= Speed;
                if (Left < _dropPoint && !_isDroped)
                {
                    _isDroped = true;
                    if (!_catcher.FirstGame.IsGameEnded)
                        EntityManager.AddEntity(new Ball(new(Left + Image.Width / 2, Top + Image.Height), _catcher));
                }
                if (Left < - Image.Width)
                    WaitForMooving();
            }
            else
            {
                Left += Speed;
                if (Left > _dropPoint && !_isDroped)
                {
                    _isDroped = true;
                    if (!_catcher.FirstGame.IsGameEnded)
                        EntityManager.AddEntity(new Ball(new(Left + Image.Width / 2, Top + Image.Height), _catcher));
                }
                if (Left > Screen.PrimaryScreen!.Bounds.Width)
                    WaitForMooving();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ImageAnimator.StopAnimate(Image, OnFrameChanged!);
                    Image.Dispose();
                }
                disposed = true;
            }
        }
    }
}
