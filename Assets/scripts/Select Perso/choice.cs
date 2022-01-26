using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts {
    public class choice : NetworkBehaviour
    {
        [SerializeField]
        GameObject PlayerPrefab;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                Cmd_ReplacePlayer();
        }

        [Command][Client]
        void Cmd_ReplacePlayer()
        {
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, Instantiate(PlayerPrefab), true);
            NetworkServer.Destroy(oldPlayer);
            
        }


    } 
}
