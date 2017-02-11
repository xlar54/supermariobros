using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

namespace GameEngine
{
    public struct ScreenLocation
    {
        public int X;
        public int Y;
    }

    public struct TileLocation
    {
        public int row;
        public int col;
    }

    public class Viewport : IDisposable
    {
        private GameObject _gameObject;
        private int _tileHeight = 0;
        private int _tileWidth = 0;
        private int _screenWidth = 0;
        private int _screenHeight = 0;
        private int _scrollSpeed = 0;

        private int _screenTilesPerColumn;   // Number of tiles wide (including off screen tiles - left & right)
        private int _screenTilesPerRow;      // Number of tiles tall (including above / below screen tiles)

        private Level _level;

        private int originXtile = 0; // The tile number to start drawing at x,0
        public int xOffset = 0;     // the offset for the X tile for smooth scrolling

        private int originYtile = 0; // the tile number to start drawing at 0,y
        public int yOffset = 0;     // the offset for the Y tile for smooth scrolling

        public List<Entity> BackSprites = new List<Entity>();

        Sprite spBack = new Sprite();

        // Scroll bounds checking
        public bool IsEndOfLevel
        {
            get 
            {
                return (_screenTilesPerColumn + originYtile >= _level.GetTileColumns-1);
            }

        }

        // Scroll bounds checking
        public bool IsStartOfLevel
        {
            get
            {
                return(originYtile == 0);
            }

        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public int ScrollSpeed
        {
            get { return _scrollSpeed; }
            set { _scrollSpeed = value; }
        }

        public Viewport(GameObject gameObject, int tileHeight, int tileWidth, Level level)
        {
            _gameObject = gameObject;
            _screenHeight = (int)gameObject.Window.Size.X;
            _screenWidth = (int)gameObject.Window.Size.Y;
            _tileHeight = tileHeight;
            _tileWidth = tileWidth;
            _level = level;

            _screenTilesPerRow = _screenWidth / _tileWidth;  // Add two additional for offscreen tiles
            _screenTilesPerColumn = _screenHeight / _tileHeight;  // Add two additional for offscreen tiles
        }

        public bool Scroll (Direction d, int Speed)
        {
            _scrollSpeed = Speed;



            return Scroll(d);
        }

        public bool Scroll(Direction d)
        {
            if (d == Direction.RIGHT)
            {
                if (!IsEndOfLevel)
                {
                    yOffset = yOffset - _scrollSpeed;

                    if (yOffset <= -_tileWidth)
                    {
                        // If not, display the next tile
                        yOffset = 0;
                        originYtile++;
                    }

                    return true;
                }
                else
                    return false;
            }

            if (d == Direction.LEFT)
            {
                if (!IsStartOfLevel)
                {
                    yOffset = yOffset - _scrollSpeed;

                    if (yOffset > _tileWidth)
                    {
                        // If not, display the next tile
                        yOffset = 0;
                        originYtile--;
                    }

                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        public List<Entity> Render()
        {
            List<Entity> newEntities = new List<Entity>();

            BackSprites.Clear();

            int screenX = 0;
            int screenY = -1;

            for (int x = originXtile; x < _screenTilesPerRow + originXtile; x++)
            {
                for (int y = originYtile; y < _screenTilesPerColumn + originYtile + 2; y++)
                {
                    if (!_level.Tiles[x, y].Background)
                    {
                        if (_level.Tiles[x, y].Entity != "")
                        {
                            // If this tile contains an entity, add it to the return list
                            string entityName = _level.Tiles[x, y].Entity;

                            // Create a background tile to replace the entity
                            if (_level.Tiles[x, y].Static != true)
                            {
                                Tile t = new Tile();
                                t.Background = true;
                                _level.Tiles[x, y] = t;
                            }

                            ScreenLocation sl = TileToScreen(x, y);
                            Entity e2 = new Entity(this._gameObject);
                            e2.Name = entityName;
                            e2.X = sl.X;
                            e2.Y = sl.Y;
                            e2.OriginTileRow = x;
                            e2.OriginTileCol = y;
                            newEntities.Add(e2);
                        }
                        else
                        { 
                            // Get the texture resoruce for this map location, and assign to the sprite
                            spBack = new Sprite(ResourceManager.Instance.GetTexture(_level.Tiles[x,y].Resource));

                            // Set the sprite/tile location and draw it
                            int x1 = (_tileHeight * screenY) + yOffset;
                            int y1 = (_tileWidth * screenX) + xOffset;

                            spBack.Position = new Vector2f(x1, y1);
                            spBack.Draw(_gameObject.Window, RenderStates.Default);

                        }
                        
                    }
                    screenY++;
                }

                screenY = -1;
                screenX++;
            }

            return newEntities;
        }

        public TileLocation ScreenToTile(int x, int y)
        {
            TileLocation t = new TileLocation();

            t.row = ((y - xOffset) / _tileHeight) + (originXtile);
            t.col = ((x- yOffset) / _tileWidth) + (originYtile+1) ;

            return t;
        }

        public ScreenLocation TileToScreen(int row, int col)
        {
            ScreenLocation s = new ScreenLocation();

            s.Y = (row * _tileHeight) * (originXtile+1);
            s.X = (col * _tileWidth) - ((originYtile+1) * _tileWidth);  
            return s;
        }

        public void Reset()
        {
            originYtile = 0;
            originXtile = 0;
            xOffset = 0;
            yOffset = 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                spBack.Dispose();
            }
        }

    }
}
