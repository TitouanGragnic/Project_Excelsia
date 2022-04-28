using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Knife : NetworkBehaviour
    {
        [SyncVar]
        public Vector3 rotate;
        [SyncVar]
        bool touche = false;
        void OnCollisionEnter(Collision col)
        {    
            if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && !touche && col.gameObject.GetComponent<Ennhvala>() == null)// layer client
                TargetBlur(col.gameObject.name);

            CmdDestroy();
        }
        [Command(requiresAuthority = false)]
        void CmdDestroy()
        {
            touche = true;
            NetworkServer.Destroy(this.gameObject);
            ClientDestroy();
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
                transform.Rotate(rotate * 20);
                CLientFixPos(transform.position, transform.rotation);
            }
        }

        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
