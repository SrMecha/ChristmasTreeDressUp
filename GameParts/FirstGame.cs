using ChristmasTreeDressUp.Classes.Controls;
using ChristmasTreeDressUp.Classes.Types;
using ChristmasTreeDressUp.Forms;
using ChristmasTreeDressUp.Properties;
using ChristmasTreeDressUp.Utility;

namespace ChristmasTreeDressUp.GameParts
{
    public class FirstGame : Control
    {
        private GameWindow _gameWindow;
        private HideBackground _cover;
        private Catcher? _cathcer = null;
        private Dragon? _dragon = null;

        public bool IsGameEnded { get; set; } = false;

        public FirstGame(GameWindow gameWindow, HideBackground cover) 
        {
            Size = gameWindow.Size;
            _gameWindow = gameWindow;
            _cover = cover;
        }

        public async Task Start()
        {
            _gameWindow.Activate();
            var t = Task.Run(() => {
                FirstDialog();
                Catching();
                WaitForCatchingEnd();
                EndCatching();
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
                "Привет!!!!",
                "Меня зовут Снежок!",
                "Я хотел нарядить ёлку к Новому году, но у меня нет ёлочных игрушек.",
                "Помоги мне найти их.",
                "Сперва давай добудем шарики.",
                "Они будут падать сверху, а тебе нужно будет их поймать.",
                "Используй стрелочки влево и вправо на своей клавиатуре, что бы управлять мной!", 
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
            _cover.Stop();
        }

        public void Catching() 
        {
            _cathcer = new Catcher(this);
            _dragon = new Dragon(_cathcer);
            EntityManager.RemoveWithTag("Tree");
            EntityManager.AddEntity(_cathcer, false);
            EntityManager.AddEntity(new ChristmasTree(Resources.ChristmasTreeEmpty), false);
            EntityManager.AddEntity(_dragon, false);
            _dragon.WaitBeforeStart();

        }

        public void EndCatching()
        {
            _cover.Start();
            _cover.WaitForCover();
            EntityManager.Remove(_cathcer!);
            EntityManager.Remove(_dragon!);
            EntityManager.RemoveWithTag("Tree");
            EntityManager.AddEntity(new ChristmasTree(Resources.ChristmasTreeWithBalls), false);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_cathcer is null)
                return;
            if (e.KeyCode == Keys.Left)
                _cathcer.IsMoovingLeft = true;
            if (e.KeyCode == Keys.Right)
                _cathcer.IsMoovingRight = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_cathcer is null)
                return;
            if (e.KeyCode == Keys.Left)
                _cathcer.IsMoovingLeft = false;
            if (e.KeyCode == Keys.Right)
                _cathcer.IsMoovingRight = false;
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void WaitForDialog(SpeechBubble speechBubble) 
        {
            var t = Task.Run(() => {
                while (!speechBubble.IsClicked)
                    Thread.Sleep(15);
            });
            t.Wait();
        }

        private void WaitForCatchingEnd()
        {
            var t = Task.Run(() => {
                while (!IsGameEnded)
                    Thread.Sleep(300);
            });
            t.Wait();
        }
    }
}
