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
    public class Mario : CharacterEntity
    {
        Sound stompSound = new Sound(ResourceManager.Instance.GetSound("stomp"));

        public bool IsOnFlagpole = false;

        public Mario(GameObject gameObject) : base(gameObject)
        {
            this.Name = "mario";
            this.EntitySpriteSheet = ResourceManager.Instance.GetSpriteSheet("sm-mario-sprites");
            this.EntitySpriteSheet.DefineFrames(Direction.RIGHT, new int[] { 0, 1, 2, 3 });
            this.EntitySpriteSheet.DefineFrames(Direction.LEFT, new int[] { 5, 6, 7, 8 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPRIGHT, new int[] { 4 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPLEFT, new int[] { 9 });
            this.IsPlayer = true;
            this.Facing = Direction.RIGHT;
            this.X = 192;
            this.Y = 640;
            this.Acceleration = 10;
            this.AllowOffscreen = false;
        }

        public override void Update()
        {
            if (this.Y > this._gameObject.Window.Size.Y - 64)
                Die();

            base.Update();
        }

        public override void OnCharacterCollision(CharacterEntity e, Direction d)
        {
            if (e.IsStatic || e.IgnorePlayerCollisions)
                return;

            if (this.Y < e.Y)
            {
                stompSound.Play();
                e.Delete = true;

                this.IsJumping = true;
                this.Velocity = -45;
            }
            else
            {
                if(e.GetType() != typeof(Flag))
                    Die();
            }
                    

        }

        public void Die()
        {
            this.IsMoving = false;
            ResourceManager.Instance.GetSound("mariodie").Play();

            ResourceManager.Instance.StopSound("music-1");

            while (ResourceManager.Instance.GetSound("mariodie").Status == SFML.Audio.SoundStatus.Playing)
            { }

            ((MainScene)_gameObject.SceneManager.CurrentScene).PlayerLives--;

            if (((MainScene)_gameObject.SceneManager.CurrentScene).PlayerLives == 0)
                _gameObject.SceneManager.StartScene("gameover");
            else
                _gameObject.SceneManager.StartScene("start");
        }

    }
}
