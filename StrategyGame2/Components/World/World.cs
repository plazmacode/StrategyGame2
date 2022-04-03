using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TugOfWar
{
    public enum GameState
    {
        PLAY,
        PAUSE
    }

    /// <summary>
    /// Class for making the world. Or should i say Game World
    /// This is in a separate class to make GameWorld less bloated
    /// </summary>
    public class World : Component, IGameListener
    {
        private static World instance;

        public static GameState CurrentGameState { get; set; } = GameState.PAUSE;

        public int WorldNumber { get; set; } = 0;

        public bool WorldRemoved { get; set; } = false;

        public Grid Grid { get; set; }

        public Vector2 WorldSize { get; set; }

        private World()
        {

        }

        public static World Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new World();
                }
                return instance;
            }
        }

        public RenderTarget2D RenderTarget;
        private GraphicsDevice GD;

        /// <summary>
        /// Call this everytime the renderTarget needs to be updated
        /// </summary>
        /// <param name="renderTarget"></param>
        public void DrawSceneToTexture(RenderTarget2D renderTarget)
        {
            // Set the render target
            GD.SetRenderTarget(renderTarget);

            GD.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            // Draw the scene
            GD.Clear(Color.DarkSlateBlue);

            for (int i = 0; i < GameWorld.Instance.GameObjects.Count; i++)
            {
                if (GameWorld.Instance.GameObjects[i].HasComponent<Cell>())
                {
                    GameWorld.Instance.GameObjects[i].Draw(GameWorld.Instance._spriteBatch);
                }
            }

            // Drop the render target
            GD.SetRenderTarget(null);
        }

        public override void Awake()
        {
            CreateWorld();
            GD = GameWorld.Instance.GraphicsDevice;
            RenderTarget = new RenderTarget2D(
                GD,
                GD.PresentationParameters.BackBufferWidth,
                GD.PresentationParameters.BackBufferHeight,
                false,
                GD.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

        }

        public void RemoveWorld()
        {
            //Grid.Cells.Clear();
            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                //Remove gameObject that do not have a cell
                if (gameObject.GetComponent<Cell>() as Cell == null)
                {
                    GameWorld.Instance.Destroy(gameObject);
                }
            }
            WorldRemoved = true;
        }

        public void CreateWorld()
        {
            WorldNumber++;
            WorldSize = new Vector2(5000, 5000);
            if (Grid == null)
            {
                Grid = new Grid(50);
                Grid.BuildGrid();
            }
            CreateTerrain();
            PopulateWorld();
        }
         
        public void CreateTerrain()
        {
            Vector2 WorldGrid = new Vector2(WorldSize.X / Grid.GridSize, WorldSize.Y / Grid.GridSize);
            for (int y = 0; y <WorldGrid.Y; y++)
            {
                for (int x = 0; x < WorldGrid.X; x++)
                {
                    Vector2 current = new Vector2(x, y);
                    GameObject currentGameObject = Grid.Cells[current].GameObject;
                    int randomCoastDistance = GameWorld._Random.Next((int)WorldGrid.X / 2 - 5, (int)WorldGrid.X / 2 - 2);
                    int randomGrassDistance = GameWorld._Random.Next((int)WorldGrid.X / 2 - 9, (int)WorldGrid.X / 2 - 6);
                    if (Vector2.Distance(WorldGrid / 2, current) > randomCoastDistance)
                    {
                        currentGameObject.AddComponent(new TerrainTile("pixel", Color.DarkSlateBlue));
                    } else if (Vector2.Distance(WorldGrid / 2, current) < randomGrassDistance) {
                        currentGameObject.AddComponent(new TerrainTile("pixel", Color.Green));
                    }
                    else
                    {
                        currentGameObject.AddComponent(new TerrainTile("pixel", Color.SandyBrown));
                    }
                }
            }
        }

        public void PopulateWorld()
        {
            PlayerManager.Instance.CreateRandomPlayers(5);

            Director director = new Director();
            //director.Builders.Add(new BaseBuilder());

            for (int i = 0; i < PlayerManager.Instance.Players.Count; i++)
            {
                //string randomPlayerKey = PlayerManager.Instance.PlayerKeys[GameWorld._Random.Next(0, PlayerManager.Instance.PlayerKeys.Count)];
                string iteratePlayerKey = PlayerManager.Instance.PlayerKeys[i];
                Player player = PlayerManager.Instance.Players[iteratePlayerKey];
                GameWorld.Instance.Instantiate(BaseFactory.Instance.Create(player));
            }

            for (int i = 0; i < director.Builders.Count; i++)
            {
                director.Builders[i].BuildGameObject();
                GameWorld.Instance.Instantiate(director.Builders[i].GetResult());
            }
            WorldRemoved = false;
        }

        public override void Start()
        {

        }

        public static float tickSpeed = 500;

        float nextTick = 0;

        public override void Update()
        {
            if (CurrentGameState == GameState.PLAY)
            {
                if (GameWorld._GameTime.TotalGameTime.TotalMilliseconds > nextTick)
                {
                    nextTick = (float)GameWorld._GameTime.TotalGameTime.TotalMilliseconds + tickSpeed;
                    Tick = true;
                }
                else
                {
                    Tick = false;
                }
            }
        }

        public static bool Tick { get; set; } = false;

        public void Notify(GameEvent gameEvent)
        {

        }
    }
}