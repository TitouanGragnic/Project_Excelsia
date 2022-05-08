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

        public static Dictionary<string, Perso> players = GameManager.players;

        public string netId; 
        string winner = "";
        string looser = "";
         
        // Start is called before the first frame update
        void Start()
        {
            netId = GetComponent<NetworkIdentity>().netId.ToString();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach(var elt in players)
            {
                Debug.Log(elt.Key);
                if (elt.Value.win)
                {
                    winner = elt.Key;
                }
                if (elt.Value.lose)
                {
                    looser = elt.Key;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var elt in players)
            {
                Debug.Log(elt.Key);
            }
            Text_statewin.text = "Vainqueur: " + winner;//Whowinner(netId);
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
