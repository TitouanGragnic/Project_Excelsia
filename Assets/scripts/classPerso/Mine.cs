using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Mine : NetworkBehaviour
    {
        // Start is called before the first frame update
        public Vector3 rotate;
        bool touche = false;
        bool stick = false;
        void OnCollisionEnter(Collision col)
        {
            if ((col.gameObject.layer == 9 || col.gameObject.layer == 9 )&& !touche && col.gameObject.GetComponent<Tamo>() == null)// layer client
            {
                col.gameObject.GetComponent<Perso>().TakeDamage(500, "normal");
                touche = true;

                Destroy(this.gameObject);
            }
            if ((col.gameObject.layer == 7 || col.gameObject.layer == 11) && !stick)// layer wall or ground
            {
                stick = true;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
        }

        void Update()
        {
            if(!stick)
                transform.Rotate(rotate * 5);
        }
    }
}
