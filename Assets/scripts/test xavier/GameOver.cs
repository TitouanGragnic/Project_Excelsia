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
        public bool stateSpawn;

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

        void UpdateChoice()
        {
            if (GameManager.GetStateSpawn())
                Cmd_ReplacePlayer();

            if (Input.GetMouseButton(0))
                stateSpawn = true;
            if (Input.GetKeyDown(KeyCode.Escape))
                stateSpawn = false;

        }

        void Update()
        {
            if (stateStart)
                UpdateChoice();
            else
                stateStart = (Input.GetKeyDown(KeyCode.Escape) || GameManager.GetStateStart());

            if (stateStart == ((Input.GetMouseButton(0)) && (Input.GetKeyDown(KeyCode.Escape))))
            {
                NetworkManager.singleton.StopClient();
            }
            else
                stateStart = (Input.GetKeyDown(KeyCode.Escape) || GameManager.GetStateStart());

            if (stateStart == (Input.GetKeyDown(KeyCode.Escape)) && (Input.GetMouseButton(0)) || (Input.GetKeyDown(KeyCode.Escape)) && (Input.GetKeyDown(KeyCode.Escape)))
            {
                NetworkManager.singleton.StopClient();
                NetworkManager.singleton.StopHost();
            }

        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject SnewPlayer = Instantiate(PlayerPrefab);
            SnewPlayer.transform.position = new Vector3(10f, 10f, 10f);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.Destroy(oldPlayer);
        }
    }
}
