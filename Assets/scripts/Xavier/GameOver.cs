using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class GameOver : NetworkBehaviour
    {

        [SerializeField]
        GameObject PlayerPrefab;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            GameManager.CmdAtributPnb(name);
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
            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            GameManager.RegisterChoice(netId, SnewPlayer.GetComponent<choice>());

            SnewPlayer.transform.position = new Vector3(989.7f, 54f, -50.5f);// la camera dans le menu choice de titouan
            GameObject SoldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(SoldPlayer);
        }
    }
}
