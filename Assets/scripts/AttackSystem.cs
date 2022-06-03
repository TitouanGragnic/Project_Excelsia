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

        [SerializeField] AudioSource lecteur;
        [SerializeField] AudioClip[] sound;

        bool comboPossible = false;
        int comboStep = 0;

        bool otherCombo = false;
        int otherStep = 0;

        public float range;
        public bool stateDash =false;

        public bool testBlood;

        public int[] atk = new int[] { 15, 20, 25, 15, 30 };
        public int index = 0;

        void Awake()
        {
            range = 2f;
        }
        void Update()
        {
            index = comboStep;
            if (testBlood && Input.GetMouseButtonDown(1))
                player.TakeDamage(0f,"normal");
            if (anim.GetCurrentAnimatorStateInfo(0).length - anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit5"))
            {
                ComboReset();
                comboPossible = true;
            }

            if (!comboPossible && anim.GetCurrentAnimatorStateInfo(0).length - anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.001)
            {
                Combo();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!arm.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
                {
                    arm.Play("hit1");
                }
                if(anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime && comboPossible && comboStep<5)
                {
                    comboStep += 1;
                    comboPossible = false;
                }
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("idla arm"))
                {
                    ComboReset();
                    Attack();
                }
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idla arm")){
                if(lecteur.clip != sound[0])
                {
                    lecteur.clip = sound[0];
                    lecteur.loop = true;
                    lecteur.Play();
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
            Debug.Log($"layer :{col.gameObject.layer},  stateDash :{stateDash}");
            if ((col.gameObject.layer == 9 || col.gameObject.layer == 8) && stateDash)
            {
                player.CmdPlayerAttack(col.gameObject.name, transform.position + new Vector3(0, 1.6f, 0), 50);
                Debug.Log("dash colision");
            }
        }

        public void Attack()
        {
            index = 0;
            Taper();
            if(comboStep == 0)
            {
                anim.Play("hit1");
                comboStep = 1;
                comboPossible = true;
                return;
            }
        }
        public void ComboPossible()
        {
            comboPossible = true;
        }
        public void Combo()
        {
            index = comboStep - 1;
            Taper();
            comboPossible = true;
            if (comboStep == 2)
            {
                anim.Play("hit2");
            }
            if (comboStep == 3)
            {
                anim.Play("hit3");
            }
            if (comboStep == 4)
            {
                anim.Play("hit4");
            }
            if (comboStep == 5)
            {
                anim.Play("hit5");
            }
        }
        public void ComboReset()
        {
            comboPossible = false;
            comboStep = 0;
        }
    }
}
