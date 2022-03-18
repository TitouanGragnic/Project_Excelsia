using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Knife : NetworkBehaviour
    {
        public Vector3 rotate;
        bool touche = false;
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.layer == 9  && !touche)// layer client
            {
                col.gameObject.GetComponent<Perso>().TakeDamage(20, "normal");
                touche = true;
            }
            NetworkServer.Destroy(this.gameObject);
        }

        void Update()
        {
            transform.Rotate(rotate*20);
        }
    }
}
