using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Audio;

namespace GameEngine
{
    public class ResourceManager
    {
        private static ResourceManager instance = null;
        Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();
        Dictionary<string, SpriteSheet> _spriteSheets = new Dictionary<string, SpriteSheet>();
        Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();
        Dictionary<string, Font> _fonts = new Dictionary<string, Font>();

        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceManager();
                }
                return instance;
            }
        }

        public void LoadTextureFromFile(string name, string path)
        {
            if(GetTexture(name) == null)
            { 
                Texture texture = new Texture(path);
                _textures.Add(name, texture);
            }
        }

        public Texture GetTexture(string name)
        {
            if (_textures.ContainsKey(name))
                return _textures[name];
            else return null;
        }

        public void LoadSpriteSheetFromFile(string name, string path, int totalFrames)
        {
            if (!_spriteSheets.ContainsKey(name))
            {
                SpriteSheet spriteSheet = new SpriteSheet();
                spriteSheet.texture = new Texture(path);
                spriteSheet.TotalFrames = totalFrames;

                _spriteSheets.Add(name, spriteSheet);
            }
        }

        public SpriteSheet GetSpriteSheet(string name)
        {
            if (_spriteSheets.ContainsKey(name))
                return _spriteSheets[name];
            else return null;
        }


        public bool LoadSoundFromFile(string name, string path)
        {
            if (GetSound(name) == null)
            {
                SoundBuffer _soundBuffer = new SoundBuffer(path);
                Sound s = new Sound(_soundBuffer);
                _sounds.Add(name, s);
                return true;
            }
            return true;
        }

        public bool LoadFontFromFile(string name, string path)
        {
            Font font = new Font(path);
            _fonts.Add(name, font);

            return true;

        }


        public Sound GetSound(string name)
        {
            if (_sounds.ContainsKey(name))
                return _sounds[name];
            else return null;
        }

        public void PlaySound(string name)
        {
            if (_sounds.ContainsKey(name))
                _sounds[name].Play();
        }

        public void StopSound(string name)
        {
            if (_sounds.ContainsKey(name))
                _sounds[name].Stop();
        }

        public SoundStatus GetSoundStatus(string name)
        {
            if (_sounds.ContainsKey(name))
                return _sounds[name].Status;
            else
                return SoundStatus.Stopped;
        }

        public Font GetFont(string name)
        {
            if (_fonts.ContainsKey(name))
                return _fonts[name];
            else
                return null;
        }


    }
}
