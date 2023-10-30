using ChristmasTreeDressUp.Classes.Controls;
using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.GameParts;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.Forms
{
    public partial class MainMenu : Form
    {
        private readonly GameWindow _gameWindow = new();
        private readonly MainMenuButton _playButton = new(
            Properties.Resources.PlayButton,
            new(398, 128),
            new(159, 49)
            );
        private readonly MainMenuButton _hideButon = new(
            Properties.Resources.HideButton,
            new(398, 196),
            new(159, 49)
            );
        private readonly MainMenuButton _exitButton = new(
            Properties.Resources.ExitButton,
            new(398, 264),
            new(159, 49)
            );
        private NotifyIcon _notifyIcon = new()
        {
            Visible = true,
            Icon = Properties.Resources.Icon,
            Text = "Продолжить играть!",
            BalloonTipIcon = ToolTipIcon.Info,
            BalloonTipTitle = "Хей!",
            BalloonTipText = "Нажми сюда, что бы продолжить играть."
        };
        private bool _isGameStarted = false;

        public MainMenu()
        {
            InitializeComponent();
            _notifyIcon.MouseClick += NotifyIconClick!;
            _playButton.MouseDown += PlayButtonClick!;
            _hideButon.MouseDown += HideButtonClick!;
            _exitButton.MouseDown += ExitButtonClick!;
            Controls.Add(_playButton);
            Controls.Add(_hideButon);
            Controls.Add(_exitButton);
        }

        private void HideInTray(bool withBalloon)
        {
            Hide();
            if (withBalloon)
                _notifyIcon.ShowBalloonTip(500);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            _gameWindow.Show();
        }

        private async void PlayButtonClick(object sender, EventArgs e)
        {
            if (_isGameStarted)
                return;
            EntityManager.RemoveWithTag("Tree");
            EntityManager.AddEntity(new ChristmasTree(Properties.Resources.ChristmasTreeEmpty));
            _isGameStarted = true;
            var cover = new HideBackground();
            _gameWindow.Controls.Add(cover);
            var firstGame = new FirstGame(_gameWindow, cover);
            _gameWindow.Controls.Add(firstGame);
            cover.Start();
            _gameWindow.Activate();
            var result = Task.Run(cover.WaitForCover);
            result.Wait();
            HideInTray(false);
            await firstGame.Start();
            _gameWindow.Controls.Remove(firstGame);
            var secondGame = new SecondGame(_gameWindow, cover);
            await secondGame.Start();
            _isGameStarted = false;
        }

        private void HideButtonClick(object sender, EventArgs e)
        {
            if (_isGameStarted)
                return;
            HideInTray(true);
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            if (_isGameStarted)
                return;
            _notifyIcon.Visible = false;
            Application.Exit();
        }

        private void NotifyIconClick(object sender, MouseEventArgs args)
        {
            if (_isGameStarted)
                return;
            WindowState = FormWindowState.Normal;
            Show();
        }
    }
}
