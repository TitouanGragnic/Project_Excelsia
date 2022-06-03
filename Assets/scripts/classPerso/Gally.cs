using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class Gally : Perso
    {
        [SerializeField]
        AttackSystem attackSystem;
        [SerializeField] Animator arms;
        [SerializeField] Animator sword;


        public bool actifState;
        public bool ultiState;
        void Start()
        {
            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();
            //Passif
            movement.nbJump = 2;
            movement.sprintSpeed = 120f ;
            //Actif
            actifState = false;
            personnage = "Gally";


            ultiWait = false;
        }

        // Update is called once per frame
        void Update()
        {
            CoolDown();
        }
        public new void Actif() 
        {
            Debug.Log("Gally actif");
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif || true)
                Dash();
        }
        public new void Ulti()
        {
            Debug.Log("Gally Ulti");
            if (!ultiWait && GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti || true)
                PreUlti();
        }
        int startPreUlti;
        bool ultiWait;
        int timeBeforeUlti = 1810;
        void PreUlti()
        {
            arms.Play("ulti");
            sword.Play("ulti");
            startPreUlti = GameManager.GetTimeMili();
            ultiWait = true;
        }

        private void CoolDown()
        {
            if ( GameManager.GetTime()- startCooldownActif > 2 && actifState)
                EndDash();
            if (ultiWait && GameManager.GetTimeMili() - startPreUlti > timeBeforeUlti)
                Slash();
        }

        private void EndDash()
        {
            attackSystem.stateDash = false;
            actifState = false;
            ChangeTypeATK("normal");
        }
        private void Dash()
        {
            movement.Dash();
            ChangeTypeATK("bleeding");
            startCooldownActif = GameManager.GetTime();
            actifState = true;
            attackSystem.stateDash = true;
            arms.Play("actif");
            sword.Play("actif");
        }

        [SerializeField]
        GameObject SlashObj;


        void Slash()
        {
            ultiWait = false;
            startCooldownUlti = GameManager.GetTime();
            Cmd_SpawnSlash(cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0)).GetPoint(1000));

        }

        [Command]
        private void Cmd_SpawnSlash(Vector3 destination)
        {
            GameObject sl = Instantiate(SlashObj, transform.position, transform.rotation);
            Slash Slash = sl.GetComponent<Slash>();
            sl.GetComponent<Rigidbody>().velocity= cam.transform.forward * Slash.speed;
            RotateDestination(sl, destination, true);
            NetworkServer.Spawn(sl);

        }

        void RotateDestination(GameObject obj, Vector3 destination, bool onlyY)
        {
            var direction = destination - obj.transform.position;
            var rotation = Quaternion.LookRotation(direction);
            if (onlyY)
            {
                rotation.x = 0;
                rotation.z = 0;
            }
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
        }
    }
}
