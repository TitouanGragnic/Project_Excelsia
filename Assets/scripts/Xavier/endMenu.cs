using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class endMenu : NetworkBehaviour
    {
        [SerializeField]
        GameObject PlayerPrefab;

        void Update()
        {
            if (GameManager.GetWinState(name) || GameManager.GetLooseState(name))
                Cmd_ReplacePlayer();
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject newPlayer = Instantiate(PlayerPrefab);
            newPlayer.transform.position = new Vector3(20f, 20f, 20f);
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            NetworkServer.Destroy(oldPlayer);
        }



    }
}