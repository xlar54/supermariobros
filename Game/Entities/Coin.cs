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
    public class Coin : CharacterEntity
    {
        public Coin(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "coin";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("coin");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0,1 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.IsAffectedByGravity = false;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
            this.AutoCycleStaticSpriteSheet = false;
        }

        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (this.Delete)
                return;

            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    ResourceManager.Instance.GetSound("coin").Play();
                    this.Delete = true;
                }
            }

            base.OnCharacterCollision(e, d);


        }


    }
}
