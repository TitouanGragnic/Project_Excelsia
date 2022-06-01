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
        [SerializeField]
        public AudioSource sound;

        [SerializeField]
        public AudioClip[] lect;

        public bool begin = true;
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
            if(move == 4000)
            {
                sound.clip = lect[1];
                sound.Play();
                sound.loop = false;
            }

            CLientFixPos(transform.position, transform.rotation);
        }

        public void Translate()
        {
            if (begin)
            {
                sound.clip = lect[0];
                sound.Play();
                sound.loop = true;
                begin = false;
            }
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
