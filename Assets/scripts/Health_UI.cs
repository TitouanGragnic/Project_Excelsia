using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace scripts
{
    public class Health_UI : MonoBehaviour
    {
        public Slider slider;
        [SerializeField]
        public Perso perso;

        // Update is called once per frame
        void Update()
        {
           SliderChange();
           
        }
        public void SliderChange()
        {
            slider.value = perso.health/perso.maxHealth * 1000f;
        }
    }
}
