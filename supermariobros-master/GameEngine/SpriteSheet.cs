using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace GameEngine
{
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        RIGHT,
        LEFT,
        JUMPRIGHT,
        JUMPLEFT
    }

    public struct SpriteFrame
    {
        public int currentframepointer;
        public int[] frames;
    }



    public class SpriteSheet
    {
        public Texture texture;
        public int TotalFrames = 1;
        public Dictionary<Direction, SpriteFrame> SpriteFrames = new Dictionary<Direction, SpriteFrame>();
        private int _frameWidth
        {
            get { return (int)texture.Size.X / TotalFrames; }
        }

        public SpriteSheet()
        {
        }

        public void DefineFrames(Direction d, int[] frames)
        {
            SpriteFrame sf = new SpriteFrame() { currentframepointer = 0, frames = frames };
            
            if (SpriteFrames.ContainsKey(d))
                SpriteFrames.Remove(d);

            SpriteFrames.Add(d, sf);
        }

        public IntRect GetSprite(Direction d, int index)
        {
            SpriteFrame sf = SpriteFrames[d];

            if (index >= sf.frames.Length)
                sf.currentframepointer = 0;
            else
                sf.currentframepointer = index;

            SpriteFrames[d] = sf;

            return new IntRect(_frameWidth * sf.frames[sf.currentframepointer], 0, _frameWidth, (int)texture.Size.Y);

        }

        public IntRect GetFirstSprite(Direction d)
        {
            SpriteFrame sf = SpriteFrames[d];
            sf.currentframepointer = 0;
            SpriteFrames[d] = sf;

            return new IntRect(_frameWidth * sf.frames[sf.currentframepointer], 0, _frameWidth, (int)texture.Size.Y);
        }

        public IntRect GetNextSprite(Direction d)
        {
            SpriteFrame sf = SpriteFrames[d];
            sf.currentframepointer++;

            if (sf.currentframepointer >= sf.frames.Length)
                sf.currentframepointer = 0;

            SpriteFrames[d] = sf;

            return new IntRect(_frameWidth * sf.frames[sf.currentframepointer], 0, _frameWidth, (int)texture.Size.Y);
        }
    }


}
