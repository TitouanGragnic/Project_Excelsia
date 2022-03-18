using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class Gally : Perso
    {
        [SerializeField]
        Movement movement;
        // Start is called before the first frame update
        public int dashCooldown;
        private int dashCooldownMax = 7000;
        public bool dashState;
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
            if (dashCooldown <= 0 && dashState)
                EndDashCoolDown();
            if (dashCooldown > 0)
                dashCooldown -= 1;
        }
        private void EndDashCoolDown()
        {
            dashCooldown = 0;
            dashState = false;
        }
        private void Dash()
        {
            movement.Dash();
            dashCooldown = dashCooldownMax;
            dashState = true;
        }
    }
}
