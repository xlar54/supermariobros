using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace GameEngine
{
    public class Entity : IDisposable
    {
        public Guid ID = Guid.NewGuid();
        public string Name;
        public int X = 0;
        public int Y = 0;
        public bool Visible = true;
        public int Velocity = 0;
        public int Acceleration = 0;
        public bool Delete = false;
        public bool IsStatic = false;
        public bool IgnorePlayerCollisions = false;
        public bool IgnoreAllCollisions = false;
        public bool IsAffectedByGravity = true;
        public int OriginTileRow = 0;
        public int OriginTileCol = 0;
        protected GameObject _gameObject;
        public Sprite sprite = new Sprite();
        public Texture NormalTexture;

               
        public Entity(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Draw()
        {
            if(Visible)
            { 
                sprite.Position = new Vector2f(this.X, this.Y);
                sprite.Draw(_gameObject.Window, RenderStates.Default);
            }
        }

        public virtual void Update()
        {
            
        }

        public virtual void Move()
        {

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
                sprite.Dispose();
            }
        }


    }
}
