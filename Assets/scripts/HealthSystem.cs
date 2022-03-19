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
                perso.health -= 0.01f;
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
                        QuardDamage(damage, posture);
                    else
                        Damage(damage);
                    break;

                case "poison":
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakePoison();
                        Damage(damage);
                    }
                    break;
                case "electric":
                    perso.health -= damage;
                    break;
                case "bleeding":
                    if(posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakePoison();
                        Damage(damage);
                    }
                    break;
                    break;
                default:
                    break;
            }

        }

        void Damage(float damage)
        {
            perso.health -= damage * (1f - perso.armor);
        }

        void QuardDamage(float damage,Posture posture)
        {
            perso.guard -= damage;
            if (perso.guard <= 0)
            {
                posture.Break();
            }

        }
    }
}