using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


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

        public float range;

        void Awake()
        {
            range = 2f;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Taper();
            }
        }

        [Client]
        private void Taper()
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
            {

                player.CmdPlayerAttack(hit.collider.name);
            }
        }
    }
}
