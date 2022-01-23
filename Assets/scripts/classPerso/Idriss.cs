using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Idriss : Perso
    {
        [SerializeField]
        Grapple grapple;
        // Start is called before the first frame update
        void Start()
        {
            atk = 30;
            grapple.maxGrappleCooldown = 100;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
