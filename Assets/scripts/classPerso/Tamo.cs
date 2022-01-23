using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Tamo : Perso
    {
        [SerializeField]
        AttackSystem attackSystem;
        // Start is called before the first frame update
        void Start()
        {
            typeAtk = "poison";
            attackSystem.range = 50f;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
