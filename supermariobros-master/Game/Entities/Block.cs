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
    public class Block : CharacterEntity
    {
        public Block(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "block";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("block");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.Facing = Direction.NONE;
            this.AllowOffscreen = false;
        }




    }
}
