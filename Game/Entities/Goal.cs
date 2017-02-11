using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace Mario.Characters
{
    public class Goal : CharacterEntity
    {
        public Goal(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "goal";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("goal");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }




        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (d == Direction.DOWN)
            {
                e.Y = this.Y - e.sprite.TextureRect.Height;
                e.IsJumping = false;
                e.Velocity = 0;
                e.IsMoving = false;
            }

            if (e.GetType() == typeof(Flag))
            {
                ResourceManager.Instance.GetSound("goalsound").Play();
                ResourceManager.Instance.StopSound("music-1");

                while (ResourceManager.Instance.GetSound("goalsound").Status == SFML.Audio.SoundStatus.Playing)
                { }

                _gameObject.SceneManager.StartScene("start");
            }

        }

    }
}
