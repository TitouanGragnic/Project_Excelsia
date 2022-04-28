using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using System.Text;

namespace scripts
{
    public class WinOrLooseText : NetworkBehaviour
    {
        [SerializeField]
        public Text Text_stateloose;
        [SerializeField]
        public Text Text_statewin;

        public string netId; 
        string winner = "";
        string looser = "";
         
        // Start is called before the first frame update
        void Start()
        {
            netId = GetComponent<NetworkIdentity>().netId.ToString();
            Cursor.lockState = CursorLockMode.None;
        }

        // Update is called once per frame
        void Update()
        {
            Text_statewin.text = "Vainqueur: " + Whowinner(netId);
            Text_stateloose.text = "Perdant: " + Whoolooser(netId);
        }

        
        public string Whowinner(string netId)
        {
            if (GameManager.GetWinState(netId))
                winner = netId;
            return winner;
        }

        
        public string Whoolooser(string netId)
        {
            if (GameManager.GetLooseState(netId))
                looser = netId;
            return looser;
           
        }
    }
}
