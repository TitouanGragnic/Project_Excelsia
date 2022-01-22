using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace scripts
{
    public class Guard_UI : MonoBehaviour
    {
        public Slider slider1;
        [SerializeField]
        public GameObject guard_object1;

        public Slider slider2;
        [SerializeField]
        public GameObject guard_object2;


        // Update is called once per frame
       public void SetActive(bool state)
        {
            guard_object1.SetActive(state);
            guard_object2.SetActive(state);
        }


        public void SliderChange(float newguard)
        {
            slider1.value = newguard;
            slider2.value = newguard;
        }
    }
}