using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyGame2
{
    class PlayerManager
    {
        private static PlayerManager instance;
        private Color[] colors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.HotPink };

        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        private PlayerManager()
        {

        }

        public Dictionary<string, Player> Players { get; set; } = new Dictionary<string, Player>();
        public List<string> PlayerKeys { get; set; } = new List<string>();

        public void CreateRandomPlayers(int amount)
        {
            if (Players.Count == 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    string playerName = "Player" + i;
                    Color colorIterate = colors[i];
                    Color playerColor = colors[GameWorld._Random.Next(0, colors.Length)];
                    Color randomColor = new Color(
                        GameWorld._Random.Next(0, 256),
                        GameWorld._Random.Next(0, 256),
                        GameWorld._Random.Next(0, 256));
                    Players.Add(playerName, new Player(playerName, colorIterate));
                    PlayerKeys.Add(playerName);
                }
            }
        }
    }
}
