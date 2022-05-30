using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


namespace scripts
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;
        private const string playerIDPrefix = "Player";
        private const string choiceIDPrefix = "Choice"; 
        private const string endIDPrefix = "End";

        public static string winner;
        public static string loser;
        //public GameOverScreen gameOverScreen;  

        public static Dictionary<string, Perso> players = new Dictionary<string, Perso>();
        public static Dictionary<string, Perso> Winner = new Dictionary<string, Perso>();

        public static Dictionary<string, choice> choices = new Dictionary<string, choice>();


        public static Dictionary<string, GameOver> ends = new Dictionary<string, GameOver>();
        static int Pnb=1;
        void Awake()
        {
            instance = this;
        }

        public static void CmdAtributPnb(string choiceId)
        {
            choices[choiceId].Pnb = Pnb;
            Pnb += 1;
        }

        public static bool GetStateSpawn()
        {
            //List<string> rm = new List<string>();
            bool spawnState = true;
            
            foreach (KeyValuePair<string, choice> choice_ex in choices)
            {
                if (choice_ex.Value != null)
                    spawnState &= choice_ex.Value.stateSpawn;
                /*//remove unused choice dico
                if (choice_ex.Value == null || choice_ex.Value.spawn)
                    rm.Add(choice_ex.Key);*/
            }
            /*
            foreach (string name in rm)
                choices.Remove(name);

            if (choices.Count == 0)
                Pnb = 1;*/
            return spawnState;
        }
        

        public static bool GetStateStart()
        {
            int  start = 0;
            foreach (KeyValuePair<string, choice> choice_ex in choices)
            {
                if (choice_ex.Value != null)
                {
                    start += 1;
                    if (choice_ex.Value.stateStart)
                        start += 1;
                }
            }
            return start>=2;
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
            return i==2&&looseState;
        }

        public static void End()
        {
            foreach (KeyValuePair<string, Perso> elt in GameManager.players)
            {
                if (GameManager.GetLooseState(elt.Key))
                    GameManager.loser = elt.Value.personnage;
                else
                    GameManager.winner = elt.Value.personnage;
            }
            foreach (KeyValuePair<string, Perso> player_ex in players)
            {
                player_ex.Value.CmdEnd();
            }
        }

        public static void RegisterPlayer(string netID, Perso player)
        {
            string playerId = playerIDPrefix + netID;
            players.Add(playerId, player);
            player.transform.name = playerId;
            /*
            foreach (KeyValuePair<string, Perso> player_ex in players)
            {

                if (player_ex.Value.transform.name != player.transform.name)
                {
                    player_ex.Value.InitHs(player.perso_object); 
                    player.InitHs(player_ex.Value.perso_object);
                     
                }
            }*/
        }

        public static void RegisterChoice(string netID, choice player)
        {
            string choiceId = choiceIDPrefix + netID;
            choices.Add(choiceId, player);
            player.transform.name = choiceId;

        }

        public static void RegisterEnd(string netID, GameOver player)
        {
            string choiceId = endIDPrefix + netID;
            ends.Add(choiceId, player);
            player.transform.name = choiceId;
        }

        public static void UnRegisterPlayer(string playerId)
        {
            players.Remove(playerId);
        }
        public static void UnRegisterChoice(string playerId)
        {
            choices.Remove(playerId);
            Pnb = choices.Keys.Count+1;
        }
        public static void UnRegisterEnd(string playerId)
        {
            ends.Remove(playerId);
        }

        public static Perso GetPlayer(string playerId)
        {
            return players[playerId];
        }

        public static bool IsOnCenter()
        {
            bool res = true;

            foreach (KeyValuePair<string, Perso> player_ex in players)
                res &= Math.Pow(player_ex.Value.transform.position.x,2) +  Math.Pow(player_ex.Value.transform.position.z,2)  <= 12.25/9;

            return res;
        }

        public static int GetTime() { return DateTime.Now.Second + 60 * DateTime.Now.Minute + 60 * 60 * DateTime.Now.Hour + DateTime.Now.Day*3600*24; }
        public static int GetTimeMili() { return DateTime.Now.Millisecond+(DateTime.Now.Second + 60 * DateTime.Now.Minute + 3600 * DateTime.Now.Hour + DateTime.Now.Day * 3600 * 24) *1000; }
    } 
}
