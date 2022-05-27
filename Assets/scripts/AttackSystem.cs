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

        public bool testBlood;

        public int[] atk = new int[] { 15, 20, 25, 15, 30 };
        public int index = 0;
        public int cool = 0;

        void Awake()
        {
            range = 2f;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(1) && cool<10)
                Taper();
            if (testBlood && Input.GetMouseButtonDown(0))
                player.TakeDamage(0f,"normal");
            else
            {
                if (animator != null)
                    animator.SetBool("Attack", false);
                if(arm != null)
                    arm.SetBool("Attack", false);
            }
            if (cool > 0)
                cool--;
            else
                index = 0;
        }

        [Client]
        private void Taper()
        {
            RaycastHit hit;
            //animator.SetBool("Attack", true);
            //arm.SetBool("Attack", true);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
            {
                player.CmdPlayerAttack(hit.collider.name,hit.point,atk[index]);
            }
            index = (index + 1) % 5;
            cool = 20;
        }

        void OnCollisionEnter(Collision col)
        {
            
            if (col.gameObject.layer == 9 && stateDash)
                player.CmdPlayerAttack(col.gameObject.name,transform.position + new Vector3(0,1.6f,0),50);
        }




    }
}
