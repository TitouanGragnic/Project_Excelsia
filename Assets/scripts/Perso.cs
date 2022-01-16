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
        float health;

        private void Awake()
        {
            health = 1000;
        }


        private void Update()
        {
            healthSystem.UpdateLife(health);
            SliderChange();
        }

        public void SliderChange()
        {
            slider.value = health;
        }

        public void InitHs(GameObject target)
        {
            healthSystem.healthBar.target = target;
            healthSystem.nameId = perso_object.transform.name;

            Camera cam = perso_object.GetComponentInChildren<Camera>();
            cam.cullingMask = ~(1<<health_object.layer); 
        }

        public void TakeDamage(float damage, string type)
        {
            health = healthSystem.TakeDamage(damage,type,posture)  ;
            Debug.Log(transform.name + " a pv = " + health);
        }
        public void CmdPlayerAttack(string playerId)
        {
            Debug.Log(playerId + "tapé");
            Perso player = GameManager.GetPlayer(playerId);

            player.TakeDamage(100,"normal");

        }


    }
}


        
