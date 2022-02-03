using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace scripts
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;
        private const string playerIDPrefix = "Player";
        private const string choiceIDPrefix = "Choice";
   
        public static Dictionary<string, Perso> players = new Dictionary<string, Perso>();
       
        public static Dictionary<string, choice> choices = new Dictionary<string, choice>();

        void Awake()
        {
            instance = this;
        }
    
        public static bool GetStateSpawn()
        {
            bool spawnState = true;
            foreach (KeyValuePair<string, choice> choice_ex in choices)
            {
                if (choice_ex.Value != null)
                    spawnState &= choice_ex.Value.state;
            }
            return spawnState;
        }

        public static bool GetWinState(string playerID)
        {
            int i = 0;
            bool winState = true;
            foreach (KeyValuePair<string, Perso> player in players)
            {
                i++;
                if (player.Key == playerID)
                    winState &= player.Value.health > 0;
                else
                    winState &= player.Value.health <= 0;
            }
            return i==2 && winState;
        }

        public static bool GetLooseState(string playerID)
        {
            int i = 0;
            bool looseState = true;
            foreach (KeyValuePair<string, Perso> player in players)
            {
                i++;
                if (player.Key != playerID)
                    looseState &= player.Value.health > 0;
                else
                    looseState &= player.Value.health <= 0;
            }
            return i==2 &&looseState;
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

        public static void RegisterChoice(string netID, choice player)
        {
            string choiceId = choiceIDPrefix + netID;
            choices.Add(choiceId, player);
            player.transform.name = choiceId;

            Debug.Log("add : "+ choiceId);

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
