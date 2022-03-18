using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Knife : NetworkBehaviour
    {
        bool touche = false;
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.layer == 9  && !touche)// layer client
            {
                Debug.Log("detected");
                col.gameObject.GetComponent<Perso>().TakeDamage(500, "normal");
                touche = true;
            }
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
