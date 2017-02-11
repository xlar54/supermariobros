using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace GameEngine
{
    public class CharacterEntity : Entity
    {
        public bool IsPlayer = false;
        public bool IsJumping = false;
        public bool IsMoving = false;
        public bool AllowOffscreen = false;
        public bool AutoCycleStaticSpriteSheet = true;
        public bool IsBumpedFromBelow = false;

        public SpriteSheet EntitySpriteSheet
        {
            get { return entitySpriteSheet; }
            set { 
                entitySpriteSheet = value;
                this.sprite.Texture = entitySpriteSheet.texture;
            }
        }
        public Direction Facing
        {
            get { return facing; }
            set
            {
                facing = value;
                if (facing == Direction.RIGHT)
                {
                    Acceleration = Math.Abs(Acceleration);
                }
                if (facing == Direction.LEFT)
                {
                    Acceleration = -1 * Math.Abs(Acceleration);
                }

            }
        }
        public bool HasUpwardVelocity {

            get { return Math.Sign(this.Velocity) == -1; }
        }

        private SpriteSheet entitySpriteSheet;
        private Direction facing = Direction.RIGHT;

        public CharacterEntity(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Update()
        {
            if (!Visible) return;

            if (!IsStatic)
            {
                if (IsAffectedByGravity)
                {
                    if (IsJumping)
                        Velocity += 5;
                    else
                        Velocity = 0;

                    Y = Y + Velocity;

                    IsJumping = true;
                }


                if (IsMoving)
                {
                    if (Facing == Direction.RIGHT)
                    {
                        X = X + Acceleration;

                        this.sprite.TextureRect = EntitySpriteSheet.GetNextSprite(Direction.RIGHT);
                    }

                    if (Facing == Direction.LEFT)
                    {
                        if (!AllowOffscreen)
                        {
                            if (X > 0) X = X + Acceleration;
                        }
                        else
                            X = X + Acceleration;

                        this.sprite.TextureRect = EntitySpriteSheet.GetNextSprite(Direction.LEFT);
                    }
                }

                if (!IsMoving)
                {
                    if (Facing == Direction.NONE)
                        this.sprite.TextureRect = EntitySpriteSheet.GetFirstSprite(Direction.NONE);

                    if (Facing == Direction.RIGHT)
                        this.sprite.TextureRect = EntitySpriteSheet.GetFirstSprite(Direction.RIGHT);

                    if (Facing == Direction.LEFT)
                        this.sprite.TextureRect = EntitySpriteSheet.GetFirstSprite(Direction.LEFT);
                }

                if (Velocity != 0 && Facing == Direction.RIGHT)
                {
                    this.sprite.TextureRect = EntitySpriteSheet.GetFirstSprite(Direction.JUMPRIGHT);
                }

                if (Velocity != 0 && Facing == Direction.LEFT)
                {
                    this.sprite.TextureRect = EntitySpriteSheet.GetFirstSprite(Direction.JUMPLEFT);
                }
            }
            else
            {
                if (this.AutoCycleStaticSpriteSheet && this.entitySpriteSheet.TotalFrames > 1)
                    this.sprite.TextureRect = EntitySpriteSheet.GetNextSprite(Direction.NONE);
               
                
                Viewport v = _gameObject.SceneManager.CurrentScene.viewPort;
                ScreenLocation sl = v.TileToScreen(this.OriginTileRow, this.OriginTileCol);
                this.X = sl.X + v.yOffset;
                this.Y = sl.Y + Velocity;
            }
            
            base.Move();
        }

        public virtual void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (d == Direction.DOWN)
            {
                e.Y = this.Y - e.sprite.TextureRect.Height;
                e.IsJumping = false;
                e.Velocity = 0;

                if (e.IsPlayer)
                    if (!Keyboard.IsKeyPressed(Keyboard.Key.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.Right))
                        e.IsMoving = false;
            }

            if (d == Direction.LEFT)
            {
                e.X = this.X - this.sprite.TextureRect.Width;
                e.IsJumping = false;
                e.Velocity = 0;
            }


            if (d == Direction.RIGHT)
            {
                e.X = this.X + this.sprite.TextureRect.Width;
                e.IsJumping = false;
                e.Velocity = 0;
            }

            if (d != Direction.DOWN && d != Direction.UP && e.IsPlayer == false)
            {
                if (e.Facing == Direction.RIGHT)
                    e.Facing = Direction.LEFT;
                else
                    e.Facing = Direction.RIGHT;
            }
        }

    }
}
