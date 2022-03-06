using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class endMenu : NetworkBehaviour
    {
        [SyncVar]
        public bool stateSpawn;

        [SerializeField]
        public bool stateStart;

        [SerializeField]
        GameObject PlayerPrefab;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            stateSpawn = false;
            GameManager.CmdAtributPnb(name);
        }

        private void TestEnd()
        {
            if (GameManager.GetWinState(name) || GameManager.GetLooseState(name))
                Cmd_ReplacePlayer();     
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject newPlayer = Instantiate(PlayerPrefab);
            newPlayer.transform.position = new Vector3(20f, 20f, 20f);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.Destroy(oldPlayer);
        }



    }
}