using ChristmasTreeDressUp.Enums;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Classes.Controls
{
    public class MainMenuButton : PictureBox
    {
        private MenuButtonState State = MenuButtonState.Normal;
        public MainMenuButton(Bitmap image, Point location, Size size)
        {
            Size = new(size.Width, size.Height + 8);
            BackColor = Color.Transparent;
            location.Y -= 8;
            Location = location;
            Image = ImageHelper.ResizeImage(image, size);
            MouseDown += ButtonMouseDown!;
            MouseUp += ButtonMouseUp!;
            MouseEnter += ButtonMouseEnter!;
            MouseLeave += ButtonMouseLeave!;
        }

        private void ButtonMouseDown(object sender, MouseEventArgs e) //кнопка в момент нажатия ЛКМ
        {
            State = MenuButtonState.Click;
            Invalidate();
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e) //кнопка в момент отпускания ЛКМ
        {
            State = MenuButtonState.Enter;
            Invalidate();
        }
        private void ButtonMouseEnter(object sender, EventArgs e) // кнопка при наведении на нее курсора
        {
            State = MenuButtonState.Enter;
            Invalidate();
        }

        private void ButtonMouseLeave(object sender, EventArgs e) // кнопка когда курсор отведен
        {
            State = MenuButtonState.Normal;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.DrawImage(Image, 0, (int)State);
        }
    }
}
