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

        public bool again;

        public bool start = false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        void Update()
        {
            Rotatearound();
            /*
            if (Input.GetKeyDown(KeyCode.E))
                NetworkManager.singleton.StopHost(); 

            if (Input.GetKeyDown(KeyCode.V))
                Cmd_ReplacePlayer();*/
            if (isServer)
            {
                if (again)
                {
                    GameManager.RePlay();
                    //Cmd_ReplacePlayer();
                }
                else
                {
                    bool state = true;
                    foreach (KeyValuePair<string, GameOver> end in GameManager.ends)
                    {
                        state &= end.Value.start;
                    }
                    again = state;
                }
            }
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            GameObject SnewPlayer = Instantiate(PlayerPrefab);
            SnewPlayer.transform.position = new Vector3(995.27f, 1.84f, -13.88f);// la camera dans le menu choice de titouan
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(oldPlayer);
            /*string netId = GetComponent<NetworkIdentity>().netId.ToString();
            GameManager.RegisterChoice(netId, SnewPlayer.GetComponent<choice>());

            SnewPlayer.transform.position = new Vector3(995.27f, 1.84f, -13.88f);// la camera dans le menu choice de titouan
            GameObject SoldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, SnewPlayer, true);
            NetworkServer.Destroy(SoldPlayer);*/
        }

        public void Rotatearound()
        {
            transform.LookAt(new Vector3(0f, 0f, 0f));
            transform.RotateAround(new Vector3(0,0,0), Vector3.up, 20 * Time.deltaTime); // on vas faire tourner les nouveaux prefab autour du nouveau game object qui est un prefab en (0,0,0)
        }

        [Command]
        public void change()
        {
            start = true;
        }
    }
}
