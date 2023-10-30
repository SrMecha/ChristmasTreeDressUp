using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.Forms;
using ChristmasTreeDressUp.GameParts;
using ChristmasTreeDressUp.Utility;
using System.Drawing;
using System;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Reflection;

namespace ChristmasTreeDressUp.Classes.Controls
{
    public class GamePictureBox : PictureBox
    {
        public event EventHandler CustomUpdate;

        public GamePictureBox(GameWindow gameWindow)
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Size = Screen.PrimaryScreen!.Bounds.Size;
            Thread workerThread = new Thread(Worker);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        public void OnUpdate()
        {
            EventHandler handler = CustomUpdate;
            if (null != handler) 
                handler(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();
            EntityManager.Update();
            foreach (var entity in EntityManager.Entities)
            {
                entity.Update();
                e.Graphics.DrawImage(entity.Image, entity.Location.X, entity.Location.Y);
            }
        }

        private void Worker()
        {
            while (true)
            {
                OnUpdate();
                Invalidate();
                Thread.Sleep(16);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs args)
        {
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            foreach (var entity in EntityManager.Entities)
                if (entity.Bounds.Contains(e.Location))
                {
                    entity.OnClick();
                }
        }
    }
}
