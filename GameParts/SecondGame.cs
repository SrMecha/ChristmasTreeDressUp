using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.Forms;
using ChristmasTreeDressUp.Properties;
using ChristmasTreeDressUp.Utility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ChristmasTreeDressUp.GameParts
{
    public class SecondGame : Control
    {
        private GameWindow _gameWindow;
        private HideBackground _cover;
        private BlackScreen? _blackScreen = null;

        public int ItemFounded { get; set; } = 0;
        public bool IsGameEnded { get; set; } = false;

        public SecondGame(GameWindow gameWindow, HideBackground cover)
        {
            Location = new(0, 0);
            Size = gameWindow.Size;
            _gameWindow = gameWindow;
            _cover = cover;
        }

        public async Task Start()
        {
            var t = Task.Run(() => {
                FirstDialog();
                Game();
                WaitForGameEnd();
                EndGame();
                SecondDialog();
            });
            await t.WaitAsync(new CancellationToken());
        }

        public void FirstDialog()
        {
            var npc = new Entity(Properties.Resources.TutorialNPC,
                Properties.Resources.TutorialNPC.Size,
                new(Screen.PrimaryScreen!.Bounds.Width - (Screen.PrimaryScreen!.Bounds.Width / 10),
                    Screen.PrimaryScreen!.WorkingArea.Height - Properties.Resources.TutorialNPC.Size.Height)
                );
            EntityManager.AddEntity(npc, false);
            _cover.Stop();
            var dialogs = new List<string>()
            {
                "Отлично!",
                "Теперь у нас есть шарики.",
                "Посмотри, как красиво наша ёлочка выглядит!",
                "Но это еще не все.",
                "Теперь, давай раздобудем остальные украшения. С ними она будет смотреться еще красивее!",
                "Я оставил их в своей комнате, но там очень темно, и сам я их не найду.",
                "Осмотрись в комнате с помощью своей мышки, а когда найдешь украшения, просто кликни на них!"
            };
            SpeechBubble speechBubble = null!;
            foreach (var text in dialogs)
            {
                speechBubble = new SpeechBubble(new(npc.Left - SpeechBubble.SpeechBubbleWidth, 0), text);
                speechBubble.Top = npc.Top - speechBubble.Size.Height + 50;
                EntityManager.AddEntity(speechBubble, false);
                WaitForDialog(speechBubble);
                EntityManager.Remove(speechBubble);
            }
            _cover.Start();
            _cover.WaitForCover();
            EntityManager.Remove(speechBubble);
            EntityManager.Remove(npc);
        }

        public void SecondDialog()
        {
            var npc = new Entity(Properties.Resources.TutorialNPC,
                Properties.Resources.TutorialNPC.Size,
                new(Screen.PrimaryScreen!.Bounds.Width - (Screen.PrimaryScreen!.Bounds.Width / 10),
                    Screen.PrimaryScreen!.WorkingArea.Height - Properties.Resources.TutorialNPC.Size.Height)
                );
            EntityManager.AddEntity(npc, false);
            _cover.Stop();
            var dialogs = new List<string>()
            {
                "Ура! Теперь наша ёлочка готова!",
                "Осталось только зажечь её."
            };
            SpeechBubble speechBubble = null!;
            foreach (var text in dialogs)
            {
                speechBubble = new SpeechBubble(new(npc.Left - SpeechBubble.SpeechBubbleWidth, 0), text);
                speechBubble.Top = npc.Top - speechBubble.Size.Height + 50;
                EntityManager.AddEntity(speechBubble, false);
                WaitForDialog(speechBubble);
                EntityManager.Remove(speechBubble);
            }


            EntityManager.RemoveWithTag("Tree");
            EntityManager.AddEntity(new ChristmasTree(Properties.Resources.ChristmasTreeFullOn), false);
            dialogs = new List<string>()
            {
                "Ура!",
                "Как красиво!",
                "Спасибо тебе, что помог мне нарядить ёлку!",
                "Надеюсь, тебе понравилось наше маленькое приключение.",
                "А нам пора прощаться",
                "Если захочешь поиграть снова, ты знаешь что делать.",
                "Пока! Увидимся снова!",
            };
            foreach (var text in dialogs)
            {
                speechBubble = new SpeechBubble(new(npc.Left - SpeechBubble.SpeechBubbleWidth, 0), text);
                speechBubble.Top = npc.Top - speechBubble.Size.Height + 50;
                EntityManager.AddEntity(speechBubble, false);
                WaitForDialog(speechBubble);
                EntityManager.Remove(speechBubble);
            }
            _cover.Start();
            _cover.WaitForCover();
            EntityManager.Remove(speechBubble);
            EntityManager.Remove(npc);
            _cover.Stop();
        }

        public void Game()
        {
            EntityManager.RemoveWithTag("Tree");
            _blackScreen = new BlackScreen();
            EntityManager.AddEntity(_blackScreen, false);
            EntityManager.AddEntity(new ItemInDark(Properties.Resources.Lights, true, this), false);
            EntityManager.AddEntity(new ItemInDark(Properties.Resources.Tinsel, false, this), false);
            EntityManager.AddEntity(new ItemInDark(Properties.Resources.Lights, false, this), false);
            EntityManager.AddEntity(new ItemInDark(Properties.Resources.Tinsel, true, this), false);
            EntityManager.AddEntity(new ItemInDark(Properties.Resources.Star, Random.Shared.Next(0, 2) == 1, this), false);
            EntityManager.AddEntity(new Entity(
                Properties.Resources.SecondBackground,
                Properties.Resources.SecondBackground.Size,
                new(0, 0), "Background"), false);
            _cover.Stop();
        }

        public void EndGame()
        {
            _cover.Start();
            _cover.WaitForCover();
            EntityManager.Remove(_blackScreen!);
            EntityManager.RemoveWithTag("Background");
            EntityManager.AddEntity(new ChristmasTree(Resources.ChristmasTreeFullOff), false);
        }

        private void WaitForDialog(SpeechBubble speechBubble)
        {
            var t = Task.Run(() => {
                while (!speechBubble.IsClicked)
                    Task.Delay(15);
            });
            t.Wait();
        }

        private void WaitForGameEnd()
        {
            var t = Task.Run(() => {
                while (!IsGameEnded)
                {
                    _blackScreen!.SetLocation(Cursor.Position);
                    Thread.Sleep(15);
                }
            });
            t.Wait();
        }
    }
}
