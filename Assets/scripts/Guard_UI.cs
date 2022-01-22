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
        public GameObject guard_value;

        public float guard = 200;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A) && guard > 0)
            {
                guard_value.SetActive(true);
                SliderChange();
            }
            else
            {
                guard_value.SetActive(false);
            }
            
        }
        public void SliderChange()
        {
            slider.value = guard;
        }
    }
}