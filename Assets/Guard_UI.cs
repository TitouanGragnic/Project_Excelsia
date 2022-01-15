using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PersoScripts
{
    public class Guard_UI : MonoBehaviour
    {
        public Slider slider;
        int guard_value = 200;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SliderChange();
            }
        }
        public void SliderChange()
        {
            guard_value -= 10;
            slider.value = guard_value;
        }
    }
}