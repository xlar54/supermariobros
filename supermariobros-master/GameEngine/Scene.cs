using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Diagnostics;

namespace GameEngine
{
    public class Scene : IDisposable 
    {
        public string Name;
        public List<Entity> Entities = new List<Entity>();
        public Viewport viewPort;

        public Texture BackgroundTexture;
        public long LastCycleTime = 0;
        DateTime currentTime = System.DateTime.Now;
        DateTime targetTime = System.DateTime.Now;
        private bool pause = false;

        public bool IsPaused
        {
            get { return pause; }
        }

        protected GameObject _gameObject;
        protected Sprite BackSprite;

        public Color BackgroundColor = Color.Black;

        public Level level = new Level();
       
        public Scene(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public virtual void Initialize()
        {
            // This method is called to initialize the game

            if (this.BackgroundTexture == null)
                BackSprite = new Sprite();
            else
                BackSprite = new Sprite(this.BackgroundTexture);
        }

        public virtual void Reset()
        {
            // This method is called when the Scene is reset
        }

        public void Run()
        {
            // This is the main loop for the scene          
            while (_gameObject.Window.IsOpen)
            {
                currentTime = System.DateTime.Now;

                _gameObject.Window.Clear(this.BackgroundColor);

                this.DrawBackground();

                if (!pause)
                {
                    this.Update();
                    this.Move();
                    this.Draw();

                    _gameObject.Window.Display();
                }
                else
                {
                    if (currentTime >= targetTime)
                    {
                        pause = false;
                        this.AfterPause();
                    }
                    else
                        this.OnPause();
                }

                

                this.AfterDraw();

                _gameObject.Window.DispatchEvents();
            }
        }

        public virtual void HandleKeyPress(KeyEventArgs e)
        {
            // This is the input handler for the scene
        }

        public virtual void HandleKeyReleased(KeyEventArgs e)
        {
            // This is the input handler for the scene
        }

        public virtual void DrawBackground()
        {
            BackSprite.Position = new Vector2f(0, 0);

            BackSprite.Draw(_gameObject.Window, RenderStates.Default);
        }

        public virtual void Update()
        {
            // This is the update method for the scene.  It will call entity update methods

        }

        public virtual void Move()
        {
            // This is the update method for the scene.  It will call entity update methods

        }

        public virtual void Draw()
        {
            // This is the draw method for the scene. It will call entity draw methods

            
         
        }

        public virtual void AfterDraw()
        {
            // This method is called after the window drawing is complete
        }

        public void Pause(int milliseconds)
        {
            targetTime = currentTime.AddMilliseconds(milliseconds);
            pause = true;
        }

        public void Pause()
        {
            // A VERY long pause..
            targetTime = currentTime.AddYears(10);
            pause = true;
        }

        public void Resume()
        {
            pause = false;
        }

        public virtual void OnPause()
        {
            // This method is called each frame during the pause
        }

        public virtual void AfterPause()
        {
        }

        public virtual void Exit()
        { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                BackSprite.Dispose();
            }
        }

    }
}
