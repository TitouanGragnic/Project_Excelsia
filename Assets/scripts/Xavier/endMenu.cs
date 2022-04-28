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
        [SerializeField]
        Perso perso;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject newPlayer = Instantiate(PlayerPrefab);
            newPlayer.transform.position = new Vector3(300f, 150f, 300f);
            newPlayer.transform.rotation = new Quaternion(20f, 0f, 0f,0f);
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.Spawn(newPlayer);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            NetworkServer.Destroy(oldPlayer);
        }

        

    }
}