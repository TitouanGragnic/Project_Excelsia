using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
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
        [SyncVar]
        public bool boom = false;

        public GameObject mine;

        public Transform target;


        DateTime startBoom;
        int startBoomInt;

        [SerializeField]
        VisualEffect fogVFX;
        [SerializeField] Transform effect;

        void Start()
        {
            if(!isServer)
                GetComponent<Rigidbody>().isKinematic = true;
            else
            {
                foreach (KeyValuePair<string, Perso> ex in GameManager.players)
                    if (ex.Value.GetType() != typeof(Tamo))
                        target = ex.Value.transform;
            }
                       
        }
        void Update()
        {
            if (isServer)
                ServerUpdate();

            effect.rotation = Quaternion.Inverse(transform.rotation);
        }

        void ServerUpdate()
        {
            if (boom && DateTime.Now.Subtract(startBoom).Seconds > 10)
                EndBoom();
            if (end && DateTime.Now.Subtract(startBoom).Seconds > 20)
                Destroy();
            if (!stick)
                mine.transform.Rotate(rotate * 5);
            CLientFixPos(transform.position, mine.transform.rotation);
            
            TestBoom();
        }
        void OnCollisionEnter(Collision col)
        {
            if ((col.gameObject.layer == 7) && !stick && isServer)// layer wall or ground
                CmdFreezePosition();

        }
        bool end = false;
        void EndBoom()
        {
            boom = false;
            end = true;
            fogVFX.SetBool("Loop", false);
            ClientEndBoom();
        }
        [ClientRpc]
        void ClientEndBoom()
        {
            boom = false;
            end = true;
            fogVFX.SetBool("Loop", false);
        }

        void TestBoom()
        {
            if (!boom && !end && target != null && Vector3.Distance(target.position, transform.position) < 10)
                CmdBoom();
            else if (boom && target != null && Vector3.Distance(target.position, transform.position) < 15)
                target.gameObject.GetComponent<Perso>().TakeDamage(0.1f, "actifTamo");
        }

        void CmdBoom()
        {
            startBoom = DateTime.Now;
            fogVFX.SetBool("Loop", true);
            boom = true;
            touche = true;
            ClientBoom();
        }


        void Destroy()
        {
            NetworkServer.Destroy(this.gameObject);
            RcpDestroy();
        }

        [ClientRpc]
        void RcpDestroy()
        {
            NetworkServer.Destroy(this.gameObject);
        }

        [ClientRpc]
        void ClientBoom()
        {
            fogVFX.SetBool("Loop", true);
            touche = true;
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
            mine.transform.rotation = rot;
        }
    }
}
