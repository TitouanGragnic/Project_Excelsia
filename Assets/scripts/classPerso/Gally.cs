using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class Gally : Perso
    {
        [SerializeField]
        Movement movement;
        // Start is called before the first frame update
        void Start()
        {
            movement.nbJump = 2;
            movement.sprintSpeed = 120f ;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
