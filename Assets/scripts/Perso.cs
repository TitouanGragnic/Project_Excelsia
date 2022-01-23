using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;





namespace scripts
{
    public class Perso : NetworkBehaviour
    {
        public GameObject perso_object;
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

        public string typeAtk;
        private void Awake()
        {
            guard = 0;
            maxHealth = 1000;
            health = maxHealth;
            maxGuard = 200;
            armor = 0.1f;
            atk = 20;
            typeAtk = "normal";
        }

        private void Start()
        {
            healthSystem.PersoStart();
        }


        private void Update()
        {
            UpdateLife();
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

            Camera cam = perso_object.GetComponentInChildren<Camera>();
            cam.cullingMask = ~(1<<health_object.layer); 
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
            Perso player = GameManager.GetPlayer(playerId);

            player.TakeDamage(atk,typeAtk);

        }


    }
}


        
