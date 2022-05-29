using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;





namespace scripts
{
    public class Perso : NetworkBehaviour
    {
        public string personnage;

        public POVcam povCam;
        public Camera cam;
        public GameObject perso_object;
        public GameObject orientation;
        public GameObject camHolder;
        public Movement  movement;
        public GameObject body;
        public GameObject arm;
        public GameObject health_object;
        [SerializeField]
        public HealthSystem healthSystem;
        [SerializeField]
        private Posture posture;
        [SerializeField]
        public Slider slider;

        [SyncVar][SerializeField]
        public float health;
        public float maxHealth;

        public float armor; // = 0.1f;

        [SyncVar][SerializeField]
        public float guard;
        public float maxGuard;
        public float atk;

        [SyncVar][SerializeField]
        public bool end;
        public string typeAtk;

        [SerializeField]
        public GameObject blur;
        [SerializeField]
        endMenu ended;
        [SerializeField]
        GameObject blood;
        private void Awake()
        {
            blur.SetActive(false);
            guard = 0;
            maxHealth = 1000;
            health = maxHealth;
            maxGuard = 200;
            armor = 0.1f;
            atk = 0;
            typeAtk = "normal";
            end = false;
        }  
        private void LateUpdate()
        {
            if(isServer)
                UpdateLife();
            TestEnd();

            if (end && isServer)
                ended.Cmd_ReplacePlayer();
        }
        
        public void Place(int pnb)
        {
            povCam.place = true;
            if (pnb == 1|| true)
            {
                perso_object.transform.position = new Vector3(0, 23.5f, 296);
                povCam.yRotation = 180;
            }

            else
            {
                perso_object.transform.position = new Vector3(0, 23.5f, -296);
                povCam.yRotation = 0;
            }

        }
        [Command]
        public void ChangeTypeATK(string newType)
        {
            typeAtk = newType;
            RpcChangeTypeATK(newType);
        }
        [ClientRpc]
        void RpcChangeTypeATK(string newType)
        {
            typeAtk = newType;
        }
        private void UpdateLife()
        {
            healthSystem.UpdateLife();

            if (bloodC > 0)
                bloodC--;
            else if (stateBlood)
                CmdSpawnBlood(false);
            
        }
        public void InitHs(GameObject target)
        {
            healthSystem.healthBar.target = target;
            healthSystem.nameId = perso_object.transform.name;

        }
        public void NewGard()
        {
            if (guard < maxGuard)
                guard += 0.02f;
            else
                guard = maxGuard;          

        }

        
        public void TakeDamage(float damage, string type)
        {
            healthSystem.TakeDamage(damage, type);
            //Debug.Log(transform.name + " a pv = " + health);
            CmdSpawnBlood(true);
        }
        [Command][Client]
        public void CmdPlayerAttack(string playerId,Vector3 pos,float damage)
        {
            Debug.Log(playerId + "tapé");
            try
            {
                Perso player = GameManager.GetPlayer(playerId);

                player.TakeDamage(atk, typeAtk);

                player.TakeDamage(damage+atk, typeAtk);
            }
            catch
            {

            }
        }

        public int maxBloodC = 100;
        private int bloodC;
        private bool stateBlood;

        [Command(requiresAuthority = false)]
        public void CmdSpawnBlood(bool state )//Vector3 pos, Quaternion forward)
        {
            stateBlood = state;
            //NetworkServer.Spawn(Instantiate(blood, pos, forward));
            blood.SetActive(state);
            if (state)
                bloodC = maxBloodC;
            RcpBlood(state);
        }

        [ClientRpc]
        private void RcpBlood(bool state)
        {
            blood.SetActive(state);
        }
        [ClientRpc]
        public void Change()
        {
            foreach (KeyValuePair<string, Perso> elt in GameManager.players)
            {
                if (GameManager.GetLooseState(elt.Key))
                    GameManager.loser = elt.Value.personnage;
                else
                    GameManager.winner = elt.Value.personnage;
            }
        }
        public void CmdEnd()
        {
            end = true;
        }
        private void TestEnd()
        {
            if (GameManager.GetWinState(name) || GameManager.GetLooseState(name))
            {
                if (isServer)
                {
                    Change();
                }
                GameManager.End();
            }

        }
        public void Actif() { Debug.Log("actif not impl"); }
        public void Ulti() { Debug.Log("ulti not impl"); }




    }
}


        
