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
    public class Goomba : CharacterEntity
    {
        public Goomba(GameObject gameObject) : base(gameObject)
        {
            this.Name = "goomba";

            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("goomba");
            this.EntitySpriteSheet.DefineFrames(Direction.RIGHT, new int[] { 0,1 });
            this.EntitySpriteSheet.DefineFrames(Direction.LEFT, new int[] { 0,1 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPRIGHT, new int[] { 0 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPLEFT, new int[] { 0 });
            this.IsPlayer = false;
            
            this.Acceleration = -10;
            this.Facing = Direction.LEFT;
            this.IsMoving = true;
            this.AllowOffscreen = true;
        }

        public override void Update()
        {
            if(IsBumpedFromBelow)
            {
                this.IgnoreAllCollisions = true;
                this.IsAffectedByGravity = true;
                this.Velocity = -40;
                this.IsJumping = true;

                // Major bug here.  Need to be able to change a single entity's sprite without affecting all similar entities
                //this.EntitySpriteSheet.DefineFrames(Direction.RIGHT, new int[] { 2 });
                //this.EntitySpriteSheet.DefineFrames(Direction.LEFT, new int[] { 2 });
                this.EntitySpriteSheet.DefineFrames(Direction.JUMPRIGHT, new int[] { 2 });
                this.EntitySpriteSheet.DefineFrames(Direction.JUMPLEFT, new int[] { 2 });

                IsBumpedFromBelow = false;
            }

            base.Update();
        }

        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            base.OnCharacterCollision(e, d);

            if (e.GetType() == typeof(Mario) && e.Y < this.Y)
            {
                e.IsJumping = true;
                e.Velocity = -45;
                ((MainScene)_gameObject.SceneManager.CurrentScene).IncreaseScore(500);
            }
        }


    }
}
