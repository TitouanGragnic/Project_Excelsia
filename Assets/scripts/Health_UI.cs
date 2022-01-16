using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
namespace scripts
{
    public class Health_UI : NetworkBehaviour
    {
        public Slider slider;
        public float health = 1000f;

        // Update is called once per frame
        void Update()
        {
           SliderChange();
           
        }
        public void SliderChange()
        {
            slider.value = health;
        }
    }
}
