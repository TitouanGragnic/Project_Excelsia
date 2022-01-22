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

        [SerializeField]
        public Slider sliderguard;
        [SerializeField]
        public Slider sliderguard1;

        [SyncVar][SerializeField]
        float health;


        [SyncVar][SerializeField]
        float guard;

        private void Awake()
        {
            health = 1000;
            guard = 200;
        }


        private void Update()
        {
            healthSystem.UpdateLife(health, guard);
            SliderChange();
        }

        public void SliderChange()
        {
            slider.value = health;
            sliderguard.value = guard;
            sliderguard1.value = guard;
        }

        public void InitHs(GameObject target)
        {
            healthSystem.healthBar.target = target;
            healthSystem.nameId = perso_object.transform.name;

            Camera cam = perso_object.GetComponentInChildren<Camera>();
            cam.cullingMask = ~(1<<health_object.layer); 
        }

        public void NewGard(bool state)
        {
            if (state)
                guard = 200;
            else
                guard = 0;
        }
        public void TakeDamage(float damage, string type)
        {
            (this.health, this.guard) = healthSystem.TakeDamage(damage, type, posture);
            Debug.Log(transform.name + " a pv = " + health+guard);
        }

        [Command][Client]
        public void CmdPlayerAttack(string playerId)
        {
            Debug.Log(playerId + "tapé");
            Perso player = GameManager.GetPlayer(playerId);

            player.TakeDamage(25,"normal");

        }


    }
}


        
