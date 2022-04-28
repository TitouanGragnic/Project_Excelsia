using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class playAgainButton : NetworkBehaviour
    {
        [SerializeField]
        GameObject PlayerPrefab;

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject SnewPlayer = Instantiate(PlayerPrefab);
            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            GameManager.RegisterChoice(netId, SnewPlayer.GetComponent<choice>());

            SnewPlayer.transform.position = new Vector3(995.27f, 1.84f, -13.88f);// la camera dans le menu choice de titouan
            GameObject SoldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(SoldPlayer);
        }
    }
}
