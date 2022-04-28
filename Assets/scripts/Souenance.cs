using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Souenance : MonoBehaviour
    {
        [SerializeField] GameObject Idriss;
        [SerializeField] GameObject Gally;
        [SerializeField] GameObject Ennhvala;
        [SerializeField] GameObject Tamo;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Idriss.SetActive(true);
                Gally.SetActive(false);
                Tamo.SetActive(false);
                Ennhvala.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Z))
            {
                Idriss.SetActive(false);
                Gally.SetActive(true);
                Tamo.SetActive(false);
                Ennhvala.SetActive(false);
            }
            if (Input.GetKey(KeyCode.E))
            {
                Idriss.SetActive(false);
                Gally.SetActive(false);
                Tamo.SetActive(true);
                Ennhvala.SetActive(false);
            }
            if (Input.GetKey(KeyCode.R))
            {
                Idriss.SetActive(false);
                Gally.SetActive(false);
                Tamo.SetActive(false);
                Ennhvala.SetActive(true);
            }
            if (Input.GetKey(KeyCode.T))
            {
                Idriss.SetActive(false);
                Gally.SetActive(false);
                Tamo.SetActive(false);
                Ennhvala.SetActive(false);
            }
        }
    }
}

