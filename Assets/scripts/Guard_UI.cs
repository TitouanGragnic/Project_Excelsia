using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace scripts
{
    public class Guard_UI : MonoBehaviour
    {
        public Slider slider;
        [SerializeField]
        public GameObject guard;

        public float guard_value = 200;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A) && guard_value > 0)
            {
                guard.SetActive(true);
                SliderChange();
            }
            else
            {
                guard.SetActive(false);
            }
            
        }
        public void SliderChange()
        {
            slider.value = guard_value;
        }
    }
}