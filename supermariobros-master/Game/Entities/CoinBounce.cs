using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;

namespace Mario.Characters
{
    public class CoinBounce : CharacterEntity
    {
   
        public CoinBounce(GameObject gameObject)
            : base(gameObject)
        {
            this.Name = "coinbounce";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("coinbounce");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0, 1, 2, 3, 4, 5 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
            this.AutoCycleStaticSpriteSheet = true;
        }

        public override void Update()
        {
            base.Update();

            if (this.EntitySpriteSheet.SpriteFrames[Direction.NONE].currentframepointer == 0)
                this.Delete = true;
        }

    }
}
