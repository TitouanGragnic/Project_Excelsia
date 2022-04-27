using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;





namespace scripts
{
    public class Perso : NetworkBehaviour
    {
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
        private void Awake()
        {
            blur.SetActive(false);
            guard = 0;
            maxHealth = 1000;
            health = maxHealth;
            maxGuard = 200;
            armor = 0.1f;
            atk = 20;
            typeAtk = "normal";
            end = false;
        }

        private void Start()
        {
            healthSystem.PersoStart();
                
            
        }

       
        private void LateUpdate()
        {
            UpdateLife();
            TestEnd();

            if (end)
                ended.Cmd_ReplacePlayer();
        }
        
        public void Place(int pnb)
        {
            povCam.place = true;
            if (pnb == 1)
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
        }


        private void UpdateLife()
        {
            healthSystem.UpdateLife();
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
            healthSystem.TakeDamage(damage, type, posture);
            Debug.Log(transform.name + " a pv = " + health+guard);
        }

        [Command][Client]
        public void CmdPlayerAttack(string playerId)
        {
            Debug.Log(playerId + "tapé");
            try
            {
                Perso player = GameManager.GetPlayer(playerId);
                player.TakeDamage(atk, typeAtk);
            }
            catch
            {

            }
        }


        public void CmdEnd()
        {
            end = true;
        }
        private void TestEnd()
        {
            if (GameManager.GetWinState(name) || GameManager.GetLooseState(name))
                GameManager.End();
            

        }
        public void Actif() { Debug.Log("2"); }

        


    }
}


        
