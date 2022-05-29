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

        [SerializeField] Animator anim;
        [SerializeField] Animator arm;

        public float cooldownTime = 2f;
        public float nextFireTime = 0f;
        public static int noOfClicks = 0;
        float lastClickedTime = 0;
        float maxComboDelay = 1;

        public float range;
        public bool stateDash;

        public bool testBlood;

        public int[] atk = new int[] { 15, 20, 25, 15, 30 };
        public int index = 0;

        void Awake()
        {
            range = 2f;
        }
        void Update()
        {
            if (testBlood && Input.GetMouseButtonDown(0))
                player.TakeDamage(0f,"normal");
            else
            {
                if (anim != null)
                    anim.SetBool("Attack", false);
                if(arm != null)
                    arm.SetBool("Attack", false);
            }
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1")){
                anim.SetBool("hit1", false); 
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                anim.SetBool("hit2", false);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            {
                anim.SetBool("hit3", false);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit4"))
            {
                anim.SetBool("hit4", false);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit5"))
            {
                anim.SetBool("hit5", false);
                noOfClicks = 0;
            }

            if(Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
            }
            if(Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    index = noOfClicks;
                    OnClick();
                    Taper();
                }
            }
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
        }

        void OnCollisionEnter(Collision col)
        {
            
            if (col.gameObject.layer == 9 && stateDash)
                player.CmdPlayerAttack(col.gameObject.name,transform.position + new Vector3(0,1.6f,0),50);
        }

        void OnClick()
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if(noOfClicks == 1)
            {
                anim.SetBool("hit1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 5);

            if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit1", false);
                anim.SetBool("hit2", true);
            }
            if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit2", false);
                anim.SetBool("hit3", true);
            }
            if (noOfClicks >= 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit3", false);
                anim.SetBool("hit4", true);
            }
            if (noOfClicks >= 5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                anim.SetBool("hit4", false);
                anim.SetBool("hit5", true);
            }
        }
    }
}
