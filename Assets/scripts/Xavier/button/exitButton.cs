using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class exitButton : NetworkBehaviour
    {
        public void exitGame() 
        {
            print("ExitGame");
            NetworkManager.singleton.StopHost();
        }
    }
}
