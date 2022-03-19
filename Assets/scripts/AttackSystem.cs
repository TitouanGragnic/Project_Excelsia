using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace scripts
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        [SerializeField]
        private Perso player;
        [SerializeField]
        private LayerMask mask;

        public float range;
        public bool stateDash;

        void Awake()
        {
            range = 2f;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                Taper();
            
        }

        [Client]
        private void Taper()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
            {

                player.CmdPlayerAttack(hit.collider.name);
            }
        }

        void OnCollisionEnter(Collision col)
        {
            
            if (col.gameObject.layer == 9 && stateDash)
                TaperDash();
        }

        [Client]
        private void TaperDash()
        {
            Debug.Log("Col");
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
            {

                player.CmdPlayerAttack(hit.collider.name);
            }
        }



    }
}
