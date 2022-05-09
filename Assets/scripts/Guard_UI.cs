using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
namespace scripts
{
    public class Guard_UI : MonoBehaviour
    {
        [SerializeField]
        Perso perso;

        public Slider slider1;
        [SerializeField]
        public GameObject guard_object1;

        public Slider slider2;
        [SerializeField]
        public GameObject guard_object2;

        [Client]
       public void SetActive(bool state)
        {
            guard_object1.SetActive(state);
            guard_object2.SetActive(state);
        }

        void Update()
        {
            SliderChange();
        }

        public void SliderChange()
        {
            float newguard = perso.guard / (perso.maxGuard + 1) * 200f;

            slider1.value = newguard;
            slider2.value = newguard;

            
        }
    }
}