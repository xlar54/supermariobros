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
    public class FlagPole : CharacterEntity
    {
        public FlagPole(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "flagpole";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("flagpole");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Facing = Direction.RIGHT;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }


        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (e.IsPlayer)
            {
                Mario m = (Mario)e;

                if (m.IsOnFlagpole)
                {
                    m.IsJumping = true;
                    m.Acceleration = 0;
                    m.Velocity = 5;
                    m.X = this.X;

                    // Initiate flag falling sequence (enable gravity for it)
                    Flag f = (Flag)this._gameObject.SceneManager.CurrentScene.Entities.Find(x => x.Name == "flag");
                    f.IsAffectedByGravity = true;
                    f.Velocity = 10;
                    f.IgnorePlayerCollisions = true;
                    f.IsStatic = false;                    
                }
                else
                {
                    m.IsMoving = false;
                    ResourceManager.Instance.GetSound("flagpole").Play();
                    ResourceManager.Instance.StopSound("music-1");
                    m.IsOnFlagpole = true;

                }
            }
        }


    }
}
