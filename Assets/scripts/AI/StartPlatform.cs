using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class StartPlatform : NetworkBehaviour
    {
        [SerializeField]
        public int dir;
        Vector3 start;
        public int move = 0;

        private void Update()
        {
            if (isServer)
                ServerUpdate();
            
        }
        void ServerUpdate()
        {
            start = transform.position;
            if (move <= 10000 && GameManager.players.Count > 0)
                Translate();
            if (transform.position != start && GameManager.players.Count == 0)
                Restart();

            CLientFixPos(transform.position, transform.rotation);
        }

        public void Translate()
        {
            gameObject.transform.Translate(Vector3.forward * dir*0.001f);
            move += 1;
        }
        public void Restart()
        {
            transform.position = start;
            move = 0;
        }

        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
