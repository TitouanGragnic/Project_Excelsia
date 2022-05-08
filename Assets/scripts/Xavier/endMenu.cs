using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace scripts
{
    public class endMenu : NetworkBehaviour
    {
        [SerializeField]
        GameObject PlayerPrefab;
        [SerializeField]
        Perso perso;

        [SerializeField]
        public Text Text_stateloose;
        [SerializeField]
        public Text Text_statewin;

        public bool win;
        public string Name;

        public void Cmd_ReplacePlayer()
        {
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            GameObject newPlayer = Instantiate(PlayerPrefab);
            if (GameManager.GetWinState(name))
                perso.win = true;
            else
                perso.lose = true;
            Name = name;
            newPlayer.transform.position = new Vector3(300f, 150f, 300f);
            newPlayer.transform.rotation = new Quaternion(20f, 0f, 0f,0f);
            NetworkServer.Spawn(newPlayer);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            //NetworkServer.Destroy(oldPlayer);
        }
    }
}