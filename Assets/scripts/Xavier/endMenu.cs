using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace scripts
{
    public class endMenu : NetworkBehaviour
    {
        [SerializeField]
        GameObject PlayerPrefab;
        [SerializeField]
        Perso perso;

        public void Cmd_ReplacePlayer()
        {
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            GameObject newPlayer = Instantiate(PlayerPrefab);
            /*foreach(string elt in GameManager.players.Keys)
            {
                if (GameManager.GetWinState(elt))
                    GameManager.winner = perso.personnage;
                else
                    GameManager.loser = perso.personnage;
            }*/
            newPlayer.transform.position = new Vector3(300f, 150f, 300f);
            newPlayer.transform.rotation = new Quaternion(20f, 0f, 0f,0f);
            NetworkServer.Spawn(newPlayer);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            NetworkServer.Destroy(oldPlayer);
        }
    }
}