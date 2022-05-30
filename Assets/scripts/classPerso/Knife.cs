using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Knife : NetworkBehaviour
    {
        [SerializeField] public GameObject kn;
        [SyncVar]
        bool touche = false;
        int startStop;
        void OnCollisionEnter(Collision col)
        {
            if (!touche)
            {
                if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && !touche && col.gameObject.GetComponent<Ennhvala>() == null)// layer client
                    TargetBlur(col.gameObject.name);

                CmdStope();
            }
        }
        [Command(requiresAuthority = false)]
        void CmdDestroy()
        {
            NetworkServer.Destroy(this.gameObject);
            ClientDestroy();
        }
        [Command(requiresAuthority = false)]
        void CmdStope()
        {
            touche = true;
            GetComponent<Rigidbody>().isKinematic = true;
            startStop = GameManager.GetTime();
            
        }

        [ClientRpc]
        void ClientDestroy()
        {
            touche = true;
            NetworkServer.Destroy(this.gameObject);
        }

        [Command(requiresAuthority = false)]
        void TargetBlur( string name)
        {
            GameManager.players[name].TakeDamage(20, "normal");
            GameManager.players[name].healthSystem.CmdTakeBlur();
        }


        private void Start()
        {
            if(!isServer)
                GetComponent<Rigidbody>().isKinematic = true;
        }
        void Update()
        {
            if (isServer)
            {
                if (!touche)
                {
                    kn.transform.Rotate(Vector3.down * 20);
                    CLientFixPos(transform.position, kn.transform.rotation);
                }
                else if (GameManager.GetTime() - startStop > 10)
                    CmdDestroy();
            }
        }

        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            kn.transform.rotation = rot;
        }



    }
}
