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
    public class CoinBox : CharacterEntity
    {
        private bool bumping = false;
        public int bumpCount = 5;


        public CoinBox(GameObject gameObject) : base(gameObject)
        {
            this.Name = "coinbox";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("coinbox");
            this.EntitySpriteSheet.DefineFrames(Direction.NONE, new int[] { 0, 0, 0, 1, 2 });
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
            this.AutoCycleStaticSpriteSheet = false;
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
            if (this.Delete)
                return;

            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    ResourceManager.Instance.GetSound("coin").Play();
                    e.Y = this.Y + this.sprite.TextureRect.Height;
                    e.Velocity = 5;
                    Velocity = -20;
                    bumping = true;

                    bumpCount--;

                    ((MainScene)_gameObject.SceneManager.CurrentScene).IncreaseScore(100);

                    if (bumpCount == 0)
                    {
                        Characters.EmptyCoinBox c = new Characters.EmptyCoinBox(this._gameObject);
                        c.X = this.X;
                        c.Y = this.Y;
                        c.OriginTileCol = this.OriginTileCol;
                        c.OriginTileRow = this.OriginTileRow;
                        ((MainScene)base._gameObject.SceneManager.CurrentScene).Entities.Add(c);

                        Tile t = new Tile();
                        t.Entity = "emptycoinbox";
                        t.Frames = 1;
                        t.ID = 25;
                        t.Resource = "emptycoinbox1";
                        t.Static = true;
                        ((MainScene)base._gameObject.SceneManager.CurrentScene).level.Tiles[c.OriginTileRow, c.OriginTileCol] = t;
                        this.Delete = true;
                    }
                    else
                    {
                        Characters.CoinBounce c = new Characters.CoinBounce(this._gameObject);
                        c.X = this.X;
                        c.Y = this.Y;
                        c.OriginTileCol = this.OriginTileCol;
                        c.OriginTileRow = this.OriginTileRow-2;
                        c.IsStatic = true;
                        //c.sprite.TextureRect = c.EntitySpriteSheet.GetNextSprite(Direction.RIGHT);
                        ((MainScene)base._gameObject.SceneManager.CurrentScene).Entities.Add(c);

                    }
                }
            }

            base.OnCharacterCollision(e, d);


        }


    }
}
