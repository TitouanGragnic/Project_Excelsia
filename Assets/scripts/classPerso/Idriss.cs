using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using Mirror;
using System;

namespace scripts
{
    public class Idriss : Perso
    {
        [SerializeField]
        Grapple grapple;
        [SerializeField] Slider sliderAc;

        [SerializeField] GameObject lightning;



        public int electricCooldown;
        private int electricCooldownMax = 7000;
        public bool electricState;
        public bool coolDownEState;

        // Start is called before the first frame update
        void Start()
        {
            predSpawn = GameManager.GetTimeMili();
            startUlti = GameManager.GetTime();
            //passif
            atk = 7;
            grapple.maxGrappleCooldown = 100;
            //actif
            electricCooldown = electricCooldownMax;
            electricState = false;
            coolDownEState = false;
            personnage = "Idriss";
            ultiOn = false;
        }

        
        // Update is called once per frame
        void Update()
        {
            CoolDown();
            sliderAc.value = electricCooldownMax - electricCooldown;
            if(isServer)
                ServerUpdate();
        }
        void ServerUpdate()
        {
            if (ultiOn)
                MajLaser();
        }

        public new void Actif()
        {
            if (electricCooldown == 0)
                StartElectric();
        }

        int startUlti;
        int maxCooldownUlti=10;
        [SerializeField]
        VisualEffect laserVFX;
        bool ultiOn;

        public new void Ulti()
        {
            if (GameManager.GetTime() - startUlti > maxCooldownUlti)
                Lightning();
        }

        [Command]
        void Lightning()
        {
            startUlti = GameManager.GetTime();
            ChangeTypeATK("electric");
            laserVFX.SetBool("Loop", true);
            ultiOn = true;
            RpcLightning(true);
        }
        void EndUlti()
        {
            laserVFX.SetFloat("Lenght", 10);
            ChangeTypeATK("normal");
            laserVFX.SetBool("Loop", false);
            ultiOn = false;
            RpcLightning(false);

        }

        [ClientRpc]
        void RpcLightning(bool state)
        {
            laserVFX.SetBool("Loop", state);
        }

        [SerializeField]
        LayerMask mask;
        int predSpawn;
        void MajLaser()
        {
            RaycastHit hit;
            float lenght = 200f;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200f, mask))
            {
                Debug.Log(hit.collider.gameObject.layer);
                lenght = hit.distance;
                if ((hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 8)&&hit.collider.gameObject.GetComponent<Idriss>()==null)
                    CmdPlayerAttack(hit.collider.name, hit.point, 0.5f);
                else if ((hit.collider.gameObject.layer == 7 || hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 23) || GameManager.GetTimeMili() / 100 > predSpawn / 100)
                    SpawnDebris(hit.point, hit.collider.gameObject.layer.Equals(11) );
            }
            laserVFX.SetFloat("Lenght", lenght);
            RpcLenghtLaser(lenght);
            if (GameManager.GetTime() - startUlti > maxCooldownUlti)
                EndUlti();
        }
        [SerializeField] GameObject debris;
        void SpawnDebris(Vector3 pos,bool wall)
        {
            predSpawn = GameManager.GetTimeMili();
            GameObject obj = Instantiate(debris, pos, new Quaternion(0, 0, 0, 0));
            obj.GetComponent<DebrisElec>().WallSet(wall);
            NetworkServer.Spawn(obj);
        }


        [ClientRpc]
        void RpcLenghtLaser(float l)
        {
            laserVFX.SetFloat("Lenght", l);
        }


        private void CoolDown()
        {
            if (electricCooldown <= 4* electricCooldownMax/5 && electricState)
                EndElectric();
            if (electricCooldown <= 0 && coolDownEState)
                EndECoolDown();
            if (electricCooldown>0)
                electricCooldown -= 1;
        }
        private void EndECoolDown()
        {
            electricCooldown = 0;
            coolDownEState = false;
        }
        private void EndElectric()
        {
            electricState = false;
            lightning.SetActive(false);
            ChangeTypeATK("normal");
        }
        private void StartElectric()
        {
            ChangeTypeATK("electric");
            lightning.SetActive(true);
            electricCooldown = electricCooldownMax;
            electricState = true;
            coolDownEState = true;
        }
    }
}
