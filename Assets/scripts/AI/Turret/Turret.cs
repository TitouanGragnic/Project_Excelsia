using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        GameObject head;
        [SerializeField]
        GameObject target;
        [SerializeField]
        LayerMask mask;
        [SerializeField]
        LineRenderer lr;
        [SerializeField]
        GameObject rocket;



        public int maxCooldown = 200;
        private int cooldown;
        private bool shootState;
        void Start()
        {
            target = null;
            lr.positionCount = 2;
            shootState = true;
            cooldown = maxCooldown;
            
        }

        void Update()
        {
            FindTaget();

            if (cooldown > 0)
                cooldown -= 1;
            else if (shootState && cooldown <= 0)
            {
                cooldown = 0;
                shootState = false;
            }
            else if (target != null)
                Shoot();
        }

        void Shoot()
        {
            cooldown = maxCooldown;
            shootState = true;
            GameObject rk = Instantiate(rocket);
            rk.transform.position = head.transform.position + head.transform.forward.normalized * 2;
            rk.transform.forward = head.transform.forward;
            Rigidbody rb = rk.GetComponent<Rigidbody>();
            rb.AddForce(head.transform.forward.normalized * 10, ForceMode.Impulse);
        }


        private void FindTaget()
        {
            foreach (KeyValuePair<string, Perso> player_ex in GameManager.players)
                if (target == null || Vector3.Distance(transform.position, player_ex.Value.transform.position) <= Vector3.Distance(transform.position, target.transform.position))
                    target = player_ex.Value.body;
            if (target != null && Vector3.Distance(transform.position, target.transform.position) > 40)
                target = null;

            if (target != null)
                FollowTarget();
            else
                ResetLr();
            
        }

        private void FollowTarget()
        {
            head.transform.LookAt(target.transform.position + Vector3.up*1.5f);

            lr.SetPosition(0, head.transform.position);
            lr.SetPosition(1, target.transform.position+ Vector3.up*1.5f);

        }
        private void ResetLr()
        {
            lr.SetPosition(0, head.transform.position);
            lr.SetPosition(1, head.transform.position);
        }
    }
}
