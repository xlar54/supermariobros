using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace GameEngine
{
    public class GameObject : IDisposable
    {
        private RenderWindow _window;

        public RenderWindow Window { get { return this._window; } }
        public SceneManager SceneManager = new SceneManager();
        

        public GameObject(string Title)
        {
            // Initialize values
            _window = new RenderWindow(new VideoMode(1024u, 768u), Title, Styles.Default);

            _window.SetVisible(true);
            _window.SetVerticalSyncEnabled(true);
            _window.SetFramerateLimit(30);
            

            // Set up event handlers
            _window.Closed += _window_Closed;
            _window.KeyPressed += _window_KeyPressed;
            _window.KeyReleased += _window_KeyReleased;
        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            SceneManager.CurrentScene.HandleKeyPress(e);
        }

        void _window_KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            SceneManager.CurrentScene.HandleKeyReleased(e);
        }

        void _window_Closed(object sender, EventArgs e)
        {
            _window.Close();
        }

        public void Close()
        {
            this._window.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _window.Dispose();
            }
        }
    }
}
