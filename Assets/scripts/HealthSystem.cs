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

        [SerializeField]
        Transform groundCheck;
        [SerializeField]
        Posture posture;

        [SerializeField]
        LayerMask groundMask;




        public HealthBar healthBar;
        public Health_UI heath_UI;
        public Guard_UI guard_UI;
        public string nameId;

        //poison
        public bool poisonState;
        int poisonCooldown;
        int poinsonMaxCooldown = 5000 ;
        //bleeding
        public bool bleedingState;
        public int bleedingCooldown;
        int bleedingMaxCooldown = 2500;

        public bool blurState;
        public int blurCooldown;
        int blurMaxCooldown = 2500;

        public void PersoStart()
        {
            perso.health = perso.maxHealth;
        }

        
        public void UpdateLife()
        {
            if (poisonState)
                DegatPoison();
            if(bleedingState)
                DegatBleeding();
            DegatLava();
            if(blurState)
                Blur();
        }

        private void Blur()
        {
            if (blurState)
                blurCooldown -= 1;
            
            if (blurState && blurCooldown < 0)
            {
                perso.blur.SetActive(false);
                blurCooldown = 0;
                blurState = false;
                RpcBlur();
            }

        }
        [ClientRpc]void RpcBlur() {perso.blur.SetActive(false);}
        private void DegatLava()
        {
            if (Physics.CheckSphere(groundCheck.position, 1, groundMask))
                Damage(0.5f, perso.armor);
        }
        private void DegatPoison()
        {
            if (poisonState)
            {
                Damage(0.01f,0);
                poisonCooldown -= 1;
            }
            if (poisonCooldown < 0)
            {
                poisonCooldown = 0;
                poisonState = false;
            }
        }

        private void DegatBleeding()
        {
            if (bleedingState)
            {
                Damage(0.05f, 0);
                bleedingCooldown -= 1;
            }
            if (bleedingCooldown < 0)
            {
                bleedingCooldown = 0;
                bleedingState = false;
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdTakeBlur()
        {
            perso.blur.SetActive(true);
            blurState = true;
            blurCooldown = blurMaxCooldown;
            ClientTakeBlur();
        }
        [ClientRpc]
        void ClientTakeBlur()
        {
            perso.blur.SetActive(true);
            blurState = true;
            blurCooldown = blurMaxCooldown;

        }
        private void changeBlur(bool newState)
        {
            perso.blur.SetActive(newState);
        }

        private void TakePoison(int cooldown)
        {
            if(cooldown>poisonCooldown)
                poisonCooldown = cooldown ;
            poisonState = true;
        }

        private void TakeBleeding()
        {
            bleedingState = true;
            bleedingCooldown = bleedingMaxCooldown;

        }
        [Command]
        public void CmdTakeDamage(int damage,string type)
        {
            TakeDamage(damage, type);
        }


        public void TakeDamage(float damage, string type)
        {
            switch (type)
            {
                case "normal":
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                        Damage(damage, perso.armor);
                    break;

                case "poison":
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakePoison(poinsonMaxCooldown);
                        Damage(damage, perso.armor);
                    }
                    break;
                case "electric":
                    Damage(damage, 0);
                    break;
                case "bleeding":
                    if(posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakeBleeding();
                        Damage(damage,perso.armor);
                    }
                    break;
                case "actifTamo":
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakePoison(poinsonMaxCooldown*2);
                        Damage(damage, perso.armor);
                    }
                    break;
                case "ultiTamo":
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                    {
                        TakePoison(poinsonMaxCooldown*3);
                        Damage(damage, perso.armor);
                    }
                    break;
                case "blur":
                    CmdTakeBlur();
                    if (posture.State)
                        QuardDamage(damage, posture);
                    else
                        Damage(damage, perso.armor);
                    break;
                default:
                    break;
            }

        }

        void Damage(float damage,float armor)
        {
            perso.health -= damage * (1f - armor);
            RpcHealth(perso.health);

        }

        [ClientRpc]
        void RpcHealth(float newHealth)
        {
            perso.health = newHealth;
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