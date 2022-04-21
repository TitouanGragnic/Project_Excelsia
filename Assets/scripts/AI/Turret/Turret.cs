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
        [SerializeField]
        Material colorBase;
        [SerializeField]
        GameObject[] parts;
        Material color;

        public bool activ;
        public bool on;
        public float disolve;

        int range = 40;
        public int maxCooldown = 200;
        private int cooldown;
        private bool shootState;
        void Start()
        {
            on = true;
            activ = true;
            disolve = 0;
            target = null;
            lr.positionCount = 2;
            shootState = true;
            cooldown = maxCooldown;
            color = new Material(colorBase);

            foreach (GameObject part in parts)
                part.GetComponent<MeshRenderer>().material = color ;
        }

        void Update()
        {

            lr.gameObject.SetActive(activ || on);
            foreach (GameObject part in parts)
                part.SetActive(activ || on);

            FindTaget();
            if (activ && on)
                Attack();
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
                color.SetFloat("_Cutoff", 0);
                on = true;
                lr.gameObject.SetActive(true);
                foreach (GameObject part in parts)
                    part.SetActive(true);
            }
            else
                color.SetFloat("_Cutoff", disolve);
        }
        void Desactivate()
        {
            disolve += 0.01f;
            if (disolve >= 1)
            {
                color.SetFloat("_Cutoff", 1);
                on = false;

                lr.gameObject.SetActive(false);
                foreach (GameObject part in parts)
                    part.SetActive(false);
            }
            else
                color.SetFloat("_Cutoff", disolve);
        }
        void Attack()
        {
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
            if (target != null && Vector3.Distance(transform.position, target.transform.position) > range)
                target = null;

            if (target != null)
                FollowTarget();
            else
                ResetLr();
            
        }
        private void FollowTarget()
        {

            RaycastHit hit;
            head.transform.LookAt(target.transform.position + Vector3.up*1.5f);


            if (Physics.Raycast(head.transform.position + head.transform.forward *1.5f, head.transform.forward, out hit, range, mask) && hit.collider.gameObject.layer != 11)
            {
                lr.SetPosition(0, head.transform.position);
                lr.SetPosition(1, target.transform.position + Vector3.up * 1.5f);
            }
            else
                ResetLr();

        }
        private void ResetLr()
        {
            lr.SetPosition(0, head.transform.position);
            lr.SetPosition(1, head.transform.position);
        }

        
    }
}
