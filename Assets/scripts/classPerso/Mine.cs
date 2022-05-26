using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Mine : NetworkBehaviour
    {
        // Start is called before the first frame update
        [SyncVar]
        public Vector3 rotate;
        [SyncVar]
        public bool touche = false;
        [SyncVar]
        public bool stick = false;

        void Start()
        {
            if(!isServer)
                GetComponent<Rigidbody>().isKinematic = true;
                       
        }

        void Update()
        {

            if (!stick && isServer)
                transform.Rotate(rotate * 5);
            if (isServer)
                CLientFixPos(transform.position, transform.rotation);
        }
        void OnCollisionEnter(Collision col)
        {

            if ((col.gameObject.layer == 9 || col.gameObject.layer == 9) && !touche && col.gameObject.GetComponent<Tamo>() == null)// layer client
                CmdBoom(col.gameObject.name);
            if ((col.gameObject.layer == 7 || col.gameObject.layer == 11) && !stick && isServer)// layer wall or ground
                CmdFreezePosition();


        }

        [Command(requiresAuthority = false)]
        void CmdBoom(string name)
        {
            if(GameManager.players.ContainsKey(name))
                GameManager.players[name].TakeDamage(500, "normal",transform.position);
            touche = true;
            NetworkServer.Destroy(this.gameObject);
            ClientBoom();
        }


        [ClientRpc]
        void ClientBoom()
        {
            touche = true;
            NetworkServer.Destroy(this.gameObject);
        }

        void CmdFreezePosition()
        {
            stick = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
