using ChristmasTreeDressUp.Forms;

namespace ChristmasTreeDressUp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenu());
        }
    }
}