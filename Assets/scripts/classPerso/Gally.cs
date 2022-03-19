using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class Gally : Perso
    {
        [SerializeField]
        Movement movement;
        [SerializeField]
        AttackSystem attackSystem;
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
        }

        // Update is called once per frame
        void Update()
        {
            CoolDown();
        }
        public new void Actif() 
        {
            Debug.Log("Gally actif");
            if (dashCooldown == 0)
                Dash();
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
    }
}
