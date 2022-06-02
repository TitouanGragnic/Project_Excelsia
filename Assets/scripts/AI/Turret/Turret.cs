using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace scripts
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        GameObject head;
        [SerializeField] GameObject support;
        [SerializeField]
        GameObject target;
        [SerializeField]
        LayerMask mask;
        [SerializeField]
        LineRenderer lr;
        [SerializeField]
        GameObject rocket;
        
        [SerializeField]
        Material[] colorBases;
        [SerializeField]
        GameObject[] parts0;
        [SerializeField] int[] index0;
        [SerializeField]
        GameObject[] parts1;
        [SerializeField] int[] index1;
        [SerializeField]
        GameObject[] parts2;
        [SerializeField] int[] index2;
        [SerializeField]
        GameObject[] parts3;
        [SerializeField] int[] index3;
        [SerializeField]
        GameObject[] parts4;
        [SerializeField] int[] index4;
        [SerializeField]
        GameObject[] parts5;
        [SerializeField] int[] index5;
        List<Material> colors;

        public bool isServer;

        public bool activ;
        public bool on;
        public float disolve;

        int range = 40;
        public int maxCooldown = 50;
        private int cooldown;
        private bool shootState;


        List<GameObject[]> Parts;
        List<int[]> Index;

        Quaternion start;
        Vector3 forward;
        void Start()
        {
            start = new Quaternion(head.transform.rotation.x, head.transform.rotation.y, head.transform.rotation.z, head.transform.rotation.w);
            forward = -head.transform.forward;
            on = true;
            activ = true;
            disolve = 0;
            target = null;
            lr.positionCount = 2;
            shootState = true;
            cooldown = maxCooldown;

            Parts = new List<GameObject[]>();
            Parts.Add(parts0); Parts.Add(parts1); Parts.Add(parts2); Parts.Add(parts3); Parts.Add(parts4); Parts.Add(parts5);

            Index = new List<int[]>();
            Index.Add(index0);Index.Add(index1);Index.Add(index2);Index.Add(index3);Index.Add(index4);Index.Add(index5);


            colors = new List<Material>();
            foreach (Material m in colorBases)
                colors.Add( new Material(m));

            for (int i = 0; i < colors.Count; i++)
                for (int j = 0; j < Parts[i].Length; j++)
                    Parts[i][j].GetComponent<MeshRenderer>().materials[Index[i][j]] = colors[i];

        }

        void Update()
        {
            head.SetActive(activ || on);
            support.SetActive(activ || on);
            lr.gameObject.SetActive(activ || on);
            foreach (GameObject[] parts in Parts)
                foreach(GameObject part in parts)
                    part.SetActive(activ || on);

            FindTaget();
            if (activ && on && isServer)
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
                foreach (GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        for (int i = 0; i < part.GetComponents<MeshRenderer>()[0].materials.Length; i++)
                            part.GetComponents<MeshRenderer>()[0].materials[i].SetFloat("_Dissolve", 0);
                on = true;
                lr.gameObject.SetActive(true);
                foreach(GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        part.SetActive(true);
            }
            else
                foreach(GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        for (int i = 0; i < part.GetComponents<MeshRenderer>()[0].materials.Length; i++)
                            part.GetComponents<MeshRenderer>()[0].materials[i].SetFloat("_Dissolve", disolve);
        }
        void Desactivate()
        {
            disolve += 0.01f;
            if (disolve >= 1)
            {
                foreach (GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        for (int i = 0; i < part.GetComponents<MeshRenderer>()[0].materials.Length; i++)
                            part.GetComponents<MeshRenderer>()[0].materials[i].SetFloat("_Dissolve", 1);
                on = false;

                lr.gameObject.SetActive(false);
                foreach (GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        part.SetActive(false);
            }
            else
                foreach (GameObject[] parts in Parts)
                    foreach (GameObject part in parts)
                        for (int i = 0; i < part.GetComponents<MeshRenderer>()[0].materials.Length; i++)
                            part.GetComponents<MeshRenderer>()[0].materials[i].SetFloat("_Dissolve", disolve);

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
        [SerializeField] Transform spawn;
        void Shoot()
        {
            cooldown = maxCooldown;
            shootState = true;
            GameObject rk = Instantiate(rocket, spawn.position, spawn.rotation);
            Rigidbody rb = rk.GetComponent<Rigidbody>();
            rb.AddForce(head.transform.forward.normalized *2, ForceMode.Impulse);
            rk.GetComponent<Rocket>().target = target.transform;
            NetworkServer.Spawn(rk);
        }
        private void FindTaget()
        {
            foreach (KeyValuePair<string, Perso> player_ex in GameManager.players)
                if (target == null || Vector3.Distance(transform.position, player_ex.Value.transform.position) <= Vector3.Distance(transform.position, target.transform.position))
                    target = player_ex.Value.body;
            if (target != null && Vector3.Distance(transform.position, target.transform.position) > range)
                target = null;

            if (target != null &&  Math.Abs(Vector3.Angle(forward,target.transform.position-head.transform.position)) < 90)
                FollowTarget();
            else
                ResetLr();
            
        }
        private void FollowTarget()
        {
            /*
            Vector3 direction = target.transform.position + Vector3.up- head.transform.position;
            //Quaternion toRotation =Quaternion.FromToRotation(-head.transform.forward, direction);
            //head.transform.rotation = Quaternion.Lerp(head.transform.rotation, toRotation, Time.deltaTime);

            if (Math.Abs(Vector3.Angle(-head.transform.forward, target.transform.position - head.transform.position)) > 5)
                head.transform.rotation = Quaternion.LookRotation(-head.transform.forward, (target.transform.position - head.transform.position).normalized);
            */

            head.transform.LookAt(target.transform.position);
            head.transform.Rotate(Vector3.up * 180);

            
            lr.SetPosition(0, head.transform.position);
            lr.SetPosition(1, target.transform.position + Vector3.up );
            
            
        }
        private void ResetLr()
        {
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, start, 0.1f);
            target = null;
            lr.SetPosition(0, head.transform.position);
            lr.SetPosition(1, head.transform.position);
        }
        
        
    }
}
