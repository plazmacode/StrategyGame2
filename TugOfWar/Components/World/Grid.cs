using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    public class Grid
    {
        public float GridSize { get; set; }
        public Dictionary<Vector2, Cell> Cells { get; set; } = new Dictionary<Vector2, Cell>();

        public Grid(int gridSize)
        {
            GridSize = gridSize;
        }

        public void BuildGrid()
        {
            for (int y = 0; y < World.Instance.WorldSize.Y / GridSize; y++)
            {
                for (int x = 0; x < World.Instance.WorldSize.X / GridSize; x++)
                {
                    Vector2 cellPosition = new Vector2(x, y);
                    GameObject cell = new GameObject();
                    cell.AddComponent(new Cell(cellPosition));
                    Cells.Add(cellPosition, (Cell)cell.GetComponent<Cell>());
                    GameWorld.Instance.GameObjects.Add(cell);
                }
            }
        }
    }
}