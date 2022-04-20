using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Rocket : NetworkBehaviour
    {
        // Start is called before the first frame update
        bool touche = false;
        void OnCollisionEnter(Collision col)
        {
            if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && !touche)// layer client
            {
                col.gameObject.GetComponent<Perso>().TakeDamage(100, "normal");
                touche = true;
            }
            NetworkServer.Destroy(this.gameObject);
        }

        
    }
}