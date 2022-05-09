using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Wall : MonoBehaviour
    {

        [SerializeField]
        Material colorBase;
        [SerializeField]
        GameObject wall;
        Material color;

        
         
        public bool activ;
        public bool on;
        public float disolve;

        void Start()
        {
            on = true;
            activ = true;
            disolve = 0;

            color = new Material(colorBase);

            wall.GetComponent<MeshRenderer>().material = color;
        }

        void Update()
        {

            wall.SetActive(activ || on);
            if (!activ && on)
                Desactivate();
            if (activ && !on)
                Activate();
        }

        void Activate()
        {
            disolve -= 0.01f;
            if (disolve <= 0)
            {
                color.SetFloat("_Dissolve", 0);
                on = true;
            }
            else
                color.SetFloat("_Dissolve", disolve);
        }
        void Desactivate()
        {
            disolve += 0.01f;
            if (disolve >= 1)
            {
                color.SetFloat("_Dissolve", 1);
                on = false;
            }
            else
                color.SetFloat("_Dissolve", disolve);
        }
    }
}

