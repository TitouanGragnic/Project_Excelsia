using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class UI_choice : MonoBehaviour
    {
        public bool state;
        [SerializeField]
        GameObject Exelcia;
        void Start()
        {
            state = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (state)
                state = !GameManager.GetStateStart();
            else
                Exelcia.SetActive(false);
        }
    }
}