using ChristmasTreeDressUp.Classes.Controls;
using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.Properties;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Forms
{
    public partial class GameWindow : Form
    {
        public readonly GamePictureBox PictureBox;
        public GameWindow()
        {
            PictureBox = new GamePictureBox(this);
            Size = Screen.PrimaryScreen!.Bounds.Size;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            InitializeComponent();
            EntityManager.AddEntity(new ChristmasTree(Resources.ChristmasTreeEmpty));
            Controls.Add(PictureBox);
        }

        protected override void OnPaintBackground(PaintEventArgs e) 
        { 

        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
