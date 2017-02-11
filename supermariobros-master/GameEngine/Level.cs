using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SFML.Graphics;
using System.Data;

namespace GameEngine
{
    public class Level
    {
        private List<string> sMap = new List<string>();

        public int ID;
        public int Rows;
        public int Columns;
        public Tile[,] Tiles;
        private string _backgroundColor = "000000";

        public Color BackgroundColor
        {
            get
            {
                return new Color(
                    Convert.ToByte(Byte.Parse(_backgroundColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)),
                    Convert.ToByte(Byte.Parse(_backgroundColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber)),
                    Convert.ToByte(Byte.Parse(_backgroundColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)));

            }
        }

        public int GetTileRows
        {
            get { return Tiles.GetUpperBound(0); }
        }
        public int GetTileColumns
        {
            get { return Tiles.GetUpperBound(1); }
        }


        public Level()
        {
            
        }

        public void LoadMap(string filename, int levelId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // Load sounds
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/levelData/sounds/sound");
            foreach(XmlNode soundNode in nodes)
            {
                string name = soundNode.Attributes["name"].InnerText;
                string file = @"resources\" + soundNode.Attributes["res"].InnerText;
                ResourceManager.Instance.LoadSoundFromFile(name, file);
            }

            // Get the level ID
            this.ID = levelId;

            XmlNode tilemapNode = doc.DocumentElement.SelectSingleNode("/levelData/levels/level[@id='" + this.ID.ToString() + "']/tilemap");
            Rows = Convert.ToInt32(tilemapNode.Attributes["rows"].InnerText);
            Columns = Convert.ToInt32(tilemapNode.Attributes["cols"].InnerText);
            _backgroundColor = tilemapNode.Attributes["bgcolor"].InnerText;

            this.ValidateXML(doc, Rows, Columns);
            Tiles = new Tile[Rows, Columns];

            // Load the tile IDs into the map matrix
            XmlNode node = doc.DocumentElement.SelectSingleNode("/levelData/levels/level[@id='" + this.ID.ToString() + "']/tilemap/rows");

            for (int x = 0; x < Rows; x++)
            { 
                string[] vals =  node.ChildNodes[x].InnerText.Split(',');

                for (int y = 0; y < Columns; y++)
                {
                    Tile tile = new Tile();
                    tile.Value = Convert.ToInt32(vals[y]);

                    XmlNode n = doc.DocumentElement.SelectSingleNode("//levelData/tiles/tile[@id='" + tile.Value.ToString() + "']");

                    tile.ID = Convert.ToInt32(n.Attributes["id"].InnerText);

                    if (n.Attributes["goal"] != null)
                        tile.Goal = Convert.ToBoolean(n.Attributes["goal"].InnerText);
                    else
                        tile.Goal = false;

                    if (n.Attributes["entity"] != null)
                        tile.Entity = n.Attributes["entity"].InnerText;
                    else
                        tile.Entity = "";

                    if (n.Attributes["background"] != null)
                        tile.Background = Convert.ToBoolean(n.Attributes["background"].InnerText);
                    else
                        tile.Background = false;

                    if (n.Attributes["res"] != null)
                        tile.Resource = n.Attributes["res"].InnerText;
                    else
                        tile.Resource = "";

                    if (n.Attributes["frames"] != null)
                        tile.Frames = Convert.ToInt32(n.Attributes["frames"].InnerText);
                    else
                        tile.Frames = 0;

                    if (n.Attributes["solid"] != null)
                        tile.Solid = Convert.ToBoolean(n.Attributes["solid"].InnerText);
                    else
                        tile.Solid = false;

                    if (n.Attributes["static"] != null)
                        tile.Static = Convert.ToBoolean(n.Attributes["static"].InnerText);
                    else
                        tile.Static = false;

                    if (n.Attributes["breakable"] != null)
                        tile.Breakable = Convert.ToBoolean(n.Attributes["breakable"].InnerText);
                    else
                        tile.Breakable = false;

                    if (tile.Resource != "")
                        ResourceManager.Instance.LoadTextureFromFile(tile.Resource, @"resources\" + tile.Resource + ".png");


                    if(tile.Entity != "")
                    {
                        ResourceManager.Instance.LoadSpriteSheetFromFile(tile.Entity, @"resources\" + tile.Resource + ".png", tile.Frames);
                    }

                    Tiles[x, y] = tile;
                }
            }

        }

        private bool ValidateXML(XmlDocument doc, int rows, int cols)
        {
            //Validate the data
            XmlNode node = doc.DocumentElement.SelectSingleNode("/levelData/levels/level[@id='" + this.ID.ToString() + "']/tilemap/rows");

            if (rows != node.ChildNodes.Count)
                throw new Exception("tilemap 'rows' attribute does not match number of rows in XML level file");
            
            foreach (XmlNode n in node.ChildNodes)
            {
                string[] colVals = n.InnerText.Split(',');
                if(colVals.Length != cols)
                    throw new Exception("tilemap 'cols' attribute does not match number of columns in XML level file");
            }

            return true;
        }

    }
}
