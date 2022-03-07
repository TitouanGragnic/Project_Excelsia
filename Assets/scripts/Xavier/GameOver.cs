using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class GameOver : NetworkBehaviour
    {
        [SyncVar]
        public bool stateStart;

        [SerializeField]
        GameObject PlayerPrefab;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            GameManager.CmdAtributPnb(name);
            stateStart = true; 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                NetworkManager.singleton.StopHost(); 

            if (Input.GetKeyDown(KeyCode.V))
                Cmd_ReplacePlayer();
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject SnewPlayer = Instantiate(PlayerPrefab);
            SnewPlayer.transform.position = new Vector3(10f, 10f, 10f);// la camera dans le menu choice de titouan
            GameObject SoldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(SoldPlayer);
        }
    }
}
