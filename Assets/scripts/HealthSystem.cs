using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class HealthSystem : NetworkBehaviour
    {
        [SerializeField]
        Perso perso;

        

        public HealthBar healthBar;
        public Health_UI heath_UI;
        public Guard_UI guard_UI;
        public string nameId;

        public bool poisonState;
        int poisonCooldown;
        int poinsonMaxCooldown = 5000 ;
        
        public void PersoStart()
        {
            perso.health = perso.maxHealth;
        }


        public void UpdateLife()
        {


            DegatPoison();
            

        }
        private void DegatPoison()
        {
            if (poisonState)
            {
                perso.health -= 1f;
                poisonCooldown -= 1;
            }
            if (poisonCooldown < 0)
            {
                poisonCooldown = 0;
                poisonState = false;
            }
        }

        private void TakePoison()
        {
            poisonCooldown = poinsonMaxCooldown;
            poisonState = true;
        }

        public void TakeDamage(float damage, string type, Posture posture)
        {
            switch (type)
            {
                case "normal":
                    if (posture.State)
                    {
                        perso.guard -= damage;
                        if (perso.guard <= 0)
                        {
                            posture.Break();
                        }
                    }
                    else
                    {
                        perso.health -= damage * (1f - perso.armor);
                    }
                    break;

                case "poison":
                    if (posture.State)
                    {
                        perso.guard -= damage;
                        if (perso.guard <= 0)
                        {
                            posture.Break();
                        }
                    }
                    else
                    {
                        TakePoison();
                        perso.health -= damage * (1f - perso.armor);
                    }
                    break;
                default:
                    break;
            }

        }
    }
}