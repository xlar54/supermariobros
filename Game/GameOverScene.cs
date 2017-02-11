using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

namespace Mario
{
    public class GameOverScene : Scene
    {
        Text text;

        public GameOverScene(GameObject gameObject)
            : base(gameObject)
        {
            this.BackgroundColor = Color.Black;
        }

        public override void Initialize()
        {
            Font arial = new Font(@"resources\arial.ttf");

            text = new Text("", arial);
            text.Position = new Vector2f(0, 0);
            text.CharacterSize = 30;

        }

        public override void Reset()
        {
            ResourceManager.Instance.PlaySound("gameover");
        }

        public override void HandleKeyPress(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                _gameObject.SceneManager.StartScene("start");
            }

            if (e.Code == Keyboard.Key.Escape)
                this._gameObject.Window.Close();

            base.HandleKeyPress(e);
        }

        public override void Update()
        {
            string t;
            t = "GAME OVER";
            text.DisplayedString = t;
            text.Position = new Vector2f(430, 380);
            text.Draw(this._gameObject.Window, RenderStates.Default);

            ((MainScene)_gameObject.SceneManager.GetScene("play")).PlayerLives = 3;
            ((MainScene)_gameObject.SceneManager.GetScene("play")).Score = 0;

        }

        public override void Draw()
        {

        }

        public override void DrawBackground()
        {
        }
    }
}
