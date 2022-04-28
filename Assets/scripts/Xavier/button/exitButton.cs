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
        
        public void exitGame() 
        {
            Debug.Log("whola ca marche pas ");
            networkManager.StopHost();
        }
    }
}
