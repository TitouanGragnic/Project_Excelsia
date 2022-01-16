using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace scripts
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;
        private const string playerIDPrefix = "Player";
        
        [SerializeField]
        public static Dictionary<string, Perso> players = new Dictionary<string, Perso>();

        void Awake()
        {
            instance = this;
        }
        public static void RegisterPlayer(string netID, Perso player)
        {
            string playerId = playerIDPrefix + netID;
            players.Add(playerId, player);
            player.transform.name = playerId;

            foreach (KeyValuePair<string, Perso> player_ex in players)
            {
                

                if (player_ex.Value.transform.name != player.transform.name)
                {
                    player_ex.Value.InitHs(player.perso_object); 
                    player.InitHs(player_ex.Value.perso_object);
                     
                }
            }
        }
        public static void UnRegisterPlayer(string playerId)
        {
            players.Remove(playerId);
        }

        public static Perso GetPlayer(string playerId)
        {
            return players[playerId];
        }

    } 
}
