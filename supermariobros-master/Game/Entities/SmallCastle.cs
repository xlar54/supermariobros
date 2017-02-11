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
    public class SmallCastle : CharacterEntity
    {
        public SmallCastle(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "smallcastle";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("smallcastle");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }




    }
}
