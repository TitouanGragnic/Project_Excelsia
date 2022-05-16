using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class exitButton : NetworkBehaviour
    {
        [SerializeField] 
        NetworkManager networkManager;

        [Command(requiresAuthority = false)]
        public void exitGame() 
        {
            if(NetworkServer.active)
                NetworkManager.singleton.StopHost();
            else
                NetworkManager.singleton.StopClient();
        }
    }
}
