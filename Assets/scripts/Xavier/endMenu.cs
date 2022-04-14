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
        GameObject target;

        public static bool state = false;
        
        void Update()
        {
            if (GameManager.GetWinState(name) || GameManager.GetLooseState(name))
                Cmd_ReplacePlayer();

        }
        void Start() 
        {
            Rotatearound();
        }


        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject newPlayer = Instantiate(PlayerPrefab);
            newPlayer.transform.position = new Vector3(20f, 20f, 20f);
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            NetworkServer.Destroy(oldPlayer);
        }

        [Command][Client]
        public void Rotatearound()
        {
            GameObject Player = connectionToClient.identity.gameObject; // on recupere notre perso 
            target = Instantiate(PlayerPrefab); // une target est défini
            target.transform.position = new Vector3(30f, 30f, 30f);  // ont la place en (0,0,0)
            Player.transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime); // on vas faire tourner les nouveaux prefab autour du nouveau game object qui est un prefab en (0,0,0)
            NetworkServer.Destroy(target);
        }

    }
}