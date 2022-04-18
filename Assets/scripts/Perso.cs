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

        [SerializeField]
        GameObject Win;

        [SerializeField]
        GameObject Loose;

        public bool end;
        public string typeAtk;
        public bool alone;
        private void Awake()
        {
            guard = 0;
            maxHealth = 1000;
            health = maxHealth;
            maxGuard = 200;
            armor = 0.1f;
            atk = 20;
            typeAtk = "normal";
            end = false;
            alone = true;
        }

        private void Start()
        {
            healthSystem.PersoStart();
            if(GameManager.players.Count == 2)
                foreach (KeyValuePair<string, Perso> player in GameManager.players)
                    player.Value.alone = false ;
                
            
        }

       
        private void LateUpdate()
        {
            UpdateLife();
            TestEnd();

        }
        
        public void Place(int pnb)
        {
            povCam.place = true;
            if (pnb == 1)
            {
                perso_object.transform.position = new Vector3(0, 32, 394);
                povCam.yRotation = 180;
            }

            else
            {
                perso_object.transform.position = new Vector3(0, 32, -394);
                povCam.yRotation = 0;
            }

        }

        [Command]
        public void ChangeTypeATK(string newType)
        {
            typeAtk = newType;
        }


        [Command]
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
            Debug.Log(playerId + "tap�");
            try
            {
                Perso player = GameManager.GetPlayer(playerId);
                player.TakeDamage(atk, typeAtk);
            }
            catch
            {

            }
        }



        private void TestEnd()
        {
            if (!alone && (GameManager.GetWinState(name) || GameManager.GetLooseState(name)))
                end = true;
            

        }
        public void Actif() { Debug.Log("2"); }

        


    }
}


        
