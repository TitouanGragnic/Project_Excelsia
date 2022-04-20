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

        [SerializeField] Animator animator;
        [SerializeField] Animator arm;

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
            else
            {
                animator.SetBool("Attack", false);
                arm.SetBool("Attack", false);
            }
        }

        [Client]
        private void Taper()
        {
            RaycastHit hit;
            animator.SetBool("Attack", true);
            arm.SetBool("Attack", true);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
            {

                player.CmdPlayerAttack(hit.collider.name);
            }
        }

        void OnCollisionEnter(Collision col)
        {
            
            if (col.gameObject.layer == 9 && stateDash)
                player.CmdPlayerAttack(col.gameObject.name);
        }




    }
}
