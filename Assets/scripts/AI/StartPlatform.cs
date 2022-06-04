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
        public Vector3 start;
        public Vector3 end;


        
        private void Update()
        {
            if (isServer)
                ServerUpdate();
            
        }
        void ServerUpdate()
        {
            if (transform.localPosition != end &&  GameManager.players.Count > 0)
                Translate();
            if (transform.localPosition != start && GameManager.players.Count == 0)
                Restart();
            if(transform.localPosition == end)
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, end, Time.deltaTime*0.1f);
            if (Mathf.Abs(transform.localPosition.z - end.z) <= 0.1f)
                transform.localPosition = end;
        }
        public void Restart()
        {
            transform.localPosition = start;
            begin = true;
        }

        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
