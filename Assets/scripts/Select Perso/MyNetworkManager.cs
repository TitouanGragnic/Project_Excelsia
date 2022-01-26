using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class MyNetworkManager : NetworkManager
    {
        public void ReplacePlayer(NetworkConnection conn, GameObject newPrefab)
        {
            // Cache a reference to the current player object
            GameObject oldPlayer = conn.identity.gameObject;

            // Instantiate the new player object and broadcast to clients
            // Include true for keepAuthority paramater to prevent ownership change
            NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab), true);

            // Remove the previous player object that's now been replaced
            NetworkServer.Destroy(oldPlayer);
        }
    }
}
