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

        void Start()
        {
            perso.health = perso.maxHealth;
        }


        public void UpdateLife()
        {
            if (heath_UI != null)
                heath_UI.health = perso.health;

            if (guard_UI != null)
                guard_UI.SliderChange(perso.guard);

            healthBar.health = perso.health;

            
            

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
                
                default:
                    break;
            }

        }
    }
}