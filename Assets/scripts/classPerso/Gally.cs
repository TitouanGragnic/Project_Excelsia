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

        [SerializeField] Slider sliderAc;
        
        // Start is called before the first frame update
        public int dashCooldown;
        private int dashCooldownMax = 7000;
        public bool dashState;
        public bool coolDownDState;
        void Start()
        {
            //Passif
            movement.nbJump = 2;
            movement.sprintSpeed = 120f ;
            //Actif
            dashCooldown = dashCooldownMax;
            dashState = false;
            personnage = "Gally";
        }

        // Update is called once per frame
        void Update()
        {
            sliderAc.value = dashCooldownMax - dashCooldown;
            CoolDown();
        }
        public new void Actif() 
        {
            Debug.Log("Gally actif");
            if (dashCooldown == 0||true)
                Dash();
        }
        public new void Ulti()
        {
            Debug.Log("Gally actif");
            if (dashCooldown == 0 || true)
                Slash();
        }

        private void CoolDown()
        {
            if (dashCooldown <= 4 * dashCooldownMax / 5 && dashState)
                EndDash();
            if (dashCooldown <= 0 && coolDownDState)
                EndDashCoolDown(); 
            if (dashCooldown > 0)
                dashCooldown -= 1;
        }
        private void EndDashCoolDown()
        {
            dashCooldown = 0;
            coolDownDState = false;
        }

        private void EndDash()
        {
            attackSystem.stateDash = false;
            dashState = false;
            ChangeTypeATK("normal");
        }
        private void Dash()
        {
            movement.Dash();
            ChangeTypeATK("bleeding");
            dashCooldown = dashCooldownMax;
            dashState = true;
            coolDownDState = true;
            attackSystem.stateDash = true;
        }

        [SerializeField]
        GameObject SlashObj;


        void Slash()
        {
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
