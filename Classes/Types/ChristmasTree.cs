namespace ChristmasTreeDressUp.Classes.Types
{
    public class ChristmasTree : Entity
    {
        public ChristmasTree(Bitmap image) 
        {
            Image = image;
            Size = Image.Size;
            Location = new(0, Screen.PrimaryScreen!.WorkingArea.Height - Size.Height);
            TryAddToAnimator(Image);
            Tag = "Tree";

        }

        private void TryAddToAnimator(Bitmap image)
        {
            if (ImageAnimator.CanAnimate(image))
                ImageAnimator.Animate(image, OnFrameChanged!);
        }

        private void OnFrameChanged(object sender, EventArgs args)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (ImageAnimator.CanAnimate(Image))
                        ImageAnimator.StopAnimate(Image, OnFrameChanged!);
                    Image.Dispose();
                }
                disposed = true;
            }
        }
    }
}
