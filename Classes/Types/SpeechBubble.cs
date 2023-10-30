using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ChristmasTreeDressUp.Classes.Types
{
    public class SpeechBubble : Entity
    {
        public const int SpeechBubbleWidth = 500;
        public const int SpeechBubbleLineWidth = 480;
        public const int SpeechBubbleLineHeight = 34;
        private Font _font = new(FontFamily.GenericSansSerif, 24);

        public bool IsClicked { get; private set; } = false;

        public SpeechBubble(Point location, string text)
        {
            Location = location;
            Image = CreateSpeechBubbleImage(text);
            Size = Image.Size;
            Tag = "SpeechBubble";
        }

        private Bitmap CreateSpeechBubbleImage(string text)
        {
            var lines = (int)(text.Length * _font.Size / SpeechBubbleLineWidth) + 1;
            var finalImage = new Bitmap(500, (int)(10 + (lines * SpeechBubbleLineHeight) + 76), PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(finalImage);
            graphics.CompositingMode = CompositingMode.SourceOver;

            graphics.DrawImage(Properties.Resources.SpeechBubbleTop, 0, 0);
            for (int i = 0; i < lines; i++)
                graphics.DrawImage(Properties.Resources.SpeechBubbleMiddle, 0, 10 + (i * SpeechBubbleLineHeight));
            graphics.DrawImage(Properties.Resources.SpeechBubbleBottom, 0, finalImage.Size.Height - 76);
            graphics.DrawString(text, _font, Brushes.Black, new RectangleF(10, 10, 490, 10 + (lines * SpeechBubbleLineHeight)));

            graphics.Flush();
            return finalImage;
        }

        public override void OnClick()
        {
            IsClicked = true;
        }
    }
}
