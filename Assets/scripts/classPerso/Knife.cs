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
        bool touche = false;
        void OnCollisionEnter(Collision col)
        {
            if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && !touche && col.gameObject.GetComponent<Ennhvala>() == null)// layer client
            {
                col.gameObject.GetComponent<Perso>().TakeDamage(20, "normal");
                col.gameObject.GetComponent<HealthSystem>().TakeBlur(); 
                touche = true;
            }
            Destroy(this.gameObject);
        }

        void Update()
        {
            transform.Rotate(rotate*20);
        }
    }
}
