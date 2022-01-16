using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        [SerializeField]
        private Perso player;
        [SerializeField]
        private LayerMask mask;


        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Taper();
            }
        }
        private void Taper()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20f, mask))
            {

                player.CmdPlayerAttack(hit.collider.name);
            }
        }
    }
}
