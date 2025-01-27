using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;
using Mirror;


namespace scripts
{
    public class Slash : NetworkBehaviour
    {
        public float speed = 30;
        public float slowDownRate = 0.01f;
        public float detectingDistance = 1f;
        public float destroyDelay = 100;

        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        LayerMask mask;
        [SerializeField]
        VisualEffect slashVFX;
        public bool stopped;
        float y = 0;
        bool touche = false;
        void Start()
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            if (Physics.Raycast(distance, Vector3.down, out hit, 20f, mask))
                y = hit.point.y;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            StartCoroutine(SlowDown());
        }

        // Update is called once per frame
        void Update()
        {
            if (stopped)
                destroyDelay--;
            if (destroyDelay < 0)
                NetworkServer.Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (isServer)
            {
                RaycastHit hit;
                Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                if (Physics.Raycast(distance, transform.TransformDirection(Vector3.forward), out hit, detectingDistance, mask))
                    y = hit.point.y + 1f;
                else if (Physics.Raycast(distance, transform.TransformDirection(Vector3.down), out hit, 200f, mask))
                    y = Mathf.Lerp(transform.position.y, hit.point.y, Time.deltaTime);



                transform.position = new Vector3(transform.position.x, y, transform.position.z);

                CLientFixPos(transform.position);
            }
        }

        IEnumerator SlowDown()
        {
            float t = 1;
            while (t > 0)
            {
                rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
                t -= slowDownRate;
                yield return new WaitForSeconds(0.3f);
            }
            stopped = true;
        }

        void Explosion() 
        {
            slashVFX.SetBool("Loop", true);
            stopped = true;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.layer == 9 || col.gameObject.layer == 8&& col.gameObject.GetComponent<Gally>()== null && !touche)
                col.gameObject.GetComponent<Perso>().TakeDamage(200f, "bleeding");
            else
                Explosion();
            touche = true;
        }
        [ClientRpc]
        void CLientFixPos(Vector3 pos)
        {
            if(!isServer)
                transform.position = pos;
        }

    }
}
