using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class InputSkills : MonoBehaviour
    {
        [SerializeField]
        Gally gally;
        [SerializeField]
        Idriss idriss;
        [SerializeField]
        Ennhvala ennhvala;
        [SerializeField]
        Tamo tamo;


        void Update()
        {
            if (gally != null)
                UpG();
            else if (idriss != null)
                UpI();
            else if (ennhvala != null)
                UpE();
            else if (tamo != null)
                UpT();


        }

        void UpG()
        {
            if (Input.GetKeyDown(KeyCode.C))
                gally.Actif();
        }

        void UpI()
        {
            if (Input.GetKeyDown(KeyCode.C))
                idriss.Actif();
        }
        void UpE()
        {
            if (Input.GetKeyDown(KeyCode.C))
                ennhvala.Actif();
        }
        void UpT()
        {
            if (Input.GetKeyDown(KeyCode.C))
                tamo.Actif();
        }
    }
}
