using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace GameEngine
{
    public class SceneManager
    {
        Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

        public Scene CurrentScene = null;

        public SceneManager()
        {

        }

        public void AddScene(Scene s)
        {
            _scenes.Add(s.Name, s);

            s.Initialize();
        }

        public void StartScene(string name)
        {
            if (CurrentScene != null)
                CurrentScene.Exit();

            CurrentScene = _scenes[name];
            
            CurrentScene.Reset();
            CurrentScene.Run();
        }

        public void GotoScene(string name)
        {
            CurrentScene = _scenes[name];
            CurrentScene.Run();
        }

        public Scene GetScene(string name)
        {
            return _scenes[name];
        }
    }
}
