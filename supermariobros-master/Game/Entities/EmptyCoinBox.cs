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
    public class EmptyCoinBox : CharacterEntity
    {
        public EmptyCoinBox(GameObject gameObject): base(gameObject)
        {
            this.Name = "emptycoinbox";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("emptycoinbox");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }


        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    ResourceManager.Instance.GetSound("bump").Play();
                    e.Y = this.Y + this.sprite.TextureRect.Height;
                    e.Velocity = 5;
                }
            }

            base.OnCharacterCollision(e, d);

        }


    }
}
