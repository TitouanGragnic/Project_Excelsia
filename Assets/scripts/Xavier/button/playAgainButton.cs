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
        [SerializeField]
        NetworkManager network;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Cmd_ReplacePlayer();
            }
        }

        [Command(requiresAuthority = false)]
        [Client]
        public void Cmd_ReplacePlayer()
        {
            if (isServer)
            {
                network.StopHost();
                network.StartClient();
                network.StartHost();
            }
            else
            {
                string coucou = network.networkAddress;
                network.StopClient();
                network.networkAddress = coucou;
                network.StartClient();
            }

            /*GameObject SnewPlayer = Instantiate(PlayerPrefab);
            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            GameManager.RegisterChoice(netId, SnewPlayer.GetComponent<choice>());

            SnewPlayer.transform.position = new Vector3(995.27f, 1.84f, -13.88f);// la camera dans le menu choice de titouan
            GameObject SoldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(SoldPlayer);*/
        }
    }
}
