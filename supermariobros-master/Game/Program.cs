using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine;

namespace Mario
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           /* Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

            var game = new GameObject("Mario");

            ResourceManager.Instance.LoadSpriteSheetFromFile("sm-mario-sprites", @"resources\sm-mario-sprites.png", 10);

            ResourceManager.Instance.LoadFontFromFile("arial", @"resources\arial.ttf");
            
            

            // Build the startup menu scene
            MainScene s = new MainScene(game);
            s.Name = "play";
            //s.BackgroundTexture = ResourceManager.Instance.GetTexture("start");
            game.SceneManager.AddScene(s);

            StartScene s2 = new StartScene(game);
            s2.Name = "start";
            game.SceneManager.AddScene(s2);

            GameOverScene s3 = new GameOverScene(game);
            s3.Name = "gameover";
            game.SceneManager.AddScene(s3);

            // Start the game
            game.SceneManager.StartScene("start");

        }
    }
}
