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
    public class Brick : CharacterEntity
    {
        private bool bumping = false;

        public Brick(GameObject gameObject) : base(gameObject)
        {
            this.Name = "brick";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("brick");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }

        public override void Update()
        {
            base.Update();

            if (bumping)
            {
                Velocity += 5;
                if (Velocity == 0)
                    bumping = false;
            }
            else
                Velocity = 0;

            
        }

        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            // If player is jumping upward...
            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                // and this brick is above the player, bump it
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    ResourceManager.Instance.GetSound("bump").Play();
                    e.Y = this.Y + this.sprite.TextureRect.Height;
                    e.Velocity = 5;
                    Velocity = -20;
                    bumping = true;
                }
            }
            
            // If this block is being bumped from below and entity is on it
            if(bumping && d == Direction.DOWN)
                e.IsBumpedFromBelow = true;

            base.OnCharacterCollision(e, d);

        }


    }
}
