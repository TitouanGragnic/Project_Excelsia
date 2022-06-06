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
        string winner = GameManager.winner;
        string looser = GameManager.loser;
         
        // Start is called before the first frame update
        void Start()
        {
            netId = GetComponent<NetworkIdentity>().netId.ToString();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Update is called once per frame
        void Update()
        {
            Text_statewin.text = winner;//Whowinner(netId);
            Text_stateloose.text = "Perdant: " + looser;//Whoolooser(netId);
            //Debug.Log(Whoolooser(netId));
            //Debug.Log("Vainqueur: " + Whowinner(netId));
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
