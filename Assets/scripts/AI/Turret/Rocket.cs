using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Rocket : NetworkBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        GameObject boom;
        bool touche = false;
        [SerializeField]
        public Transform target;
        int cooldown;
        [SerializeField]
        Rigidbody body;
        [SerializeField]
        GameObject rocket;
        int cooldown2 = 500;
        void Start()
        {
            cooldown = 20;
        }
        private void Update()
        {
            if (isServer && !touche)
                ServerUpdate();
            if (isServer && touche)
                UpdateEnd();
        }
        private void UpdateEnd()
        {
            if (cooldown2 > 0)
                cooldown2--;
            else
                NetworkServer.Destroy(this.gameObject);
        }
        private void ServerUpdate()
        {
            CLientFixPos(transform.position, transform.rotation);
            if (cooldown <= 0)
                Rotate();
            else
                cooldown --;

        }

        private void Rotate()
        {
            cooldown = 20;
            Vector3 dir = target.position - transform.position;
            transform.LookAt(target.position + Vector3.up * 1.5f);
            transform.rotation = transform.rotation * Quaternion.Euler(10*Random.Range(-1f,1f), 10 * Random.Range(-1f, 1f), 10 * Random.Range(-1f, 1f));
            body.AddForce(transform.forward.normalized*2 , ForceMode.Impulse);

        }
        public ParticleSystem emit;
        // blah blah rest of code
        // Call this immediately before you destroy your missile
        [Command(requiresAuthority = false)]
        void CmdDetachParticles()
        {
            // This splits the particle off so it doesn't get deleted with the parent
            emit.transform.parent = null;

            // this stops the particle from creating more bits
            emit.Stop();
            RpcDetachParticles();
        }

        [ClientRpc]
        void RpcDetachParticles()
        {
            // This splits the particle off so it doesn't get deleted with the parent
            emit.transform.parent = null;

            // this stops the particle from creating more bits
            emit.Stop();
        }

        void OnCollisionEnter(Collision col)
        {
            if (isServer && col.gameObject.layer != 21)
            {
                if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && !touche)// layer client
                {
                    col.gameObject.GetComponent<Perso>().TakeDamage(20, "normal",transform.position);
                    rocket.SetActive(false);
                }
                touche = true;
                body.isKinematic = true;
                CmdDetachParticles();
                CmdSpawnBoom();

                //NetworkServer.Destroy(this.gameObject);
            }
        }

        void CmdSpawnBoom()
        {
            GameObject mi = Instantiate(boom, transform.position, new Quaternion(0,0,0,0));
            NetworkServer.Spawn(mi);
        }

        [ClientRpc]
        void CLientFixPos(Vector3 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }


    }
}