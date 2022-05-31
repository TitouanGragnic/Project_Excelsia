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

        [SerializeField] GameObject lightning;
        [SerializeField] Animator arms;

        //[ACTIF]
        int maxCooldownActifON = 10;
        bool electricState;
        //[ULTI]
        int maxCooldownUltiON = 5;
        [SerializeField]
        VisualEffect laserVFX;
        bool ultiOn;
        [SerializeField]
        LayerMask mask;
        int predSpawn;
        [SerializeField] GameObject debris;

        void Start()
        {
            predSpawn = GameManager.GetTimeMili();
            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();

            //passif
            atk = 7;
            grapple.maxGrappleCooldown = 2500;
            //actif
            electricState = false;
            personnage = "Idriss";
            ultiOn = false;
        }

        
        void Update()
        {
            CoolDown();
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
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif)
                StartElectric();
        }
        public new void Ulti()
        {
            if (GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti)
                Lightning();
        }
        [SerializeField] POVcam povcam;

        [Command]
        void Lightning()
        {
            povcam.multiplier /= 10;
            startCooldownUlti = GameManager.GetTime();
            ChangeTypeATK("electric");
            laserVFX.SetBool("Loop", true);
            ultiOn = true;
            RpcLightning(true);
            arms.Play("ulti");
        }
        void EndUlti()
        {
            povcam.multiplier *= 10;
            laserVFX.SetFloat("Lenght", 10);
            ChangeTypeATK("normal");
            laserVFX.SetBool("Loop", false);
            ultiOn = false;
            RpcLightning(false);

        }
        [ClientRpc]void RpcLightning(bool state)
        {
            laserVFX.SetBool("Loop", state);
        }
        void MajLaser()
        {
            RaycastHit hit;
            float lenght = 200f;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200f, mask))
            {
                Debug.Log(hit.collider.gameObject.layer);
                lenght = hit.distance;
                if ((hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 8)&&hit.collider.gameObject.GetComponent<Idriss>()==null && GameManager.GetTimeMili() / 100 > predSpawn / 100)
                    CmdPlayerAttack(hit.collider.name, hit.point, 0.5f);
                else if ((hit.collider.gameObject.layer == 7 || hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 23) && GameManager.GetTimeMili() / 100 > predSpawn / 100)
                    SpawnDebris(hit.point, hit.collider.gameObject.layer.Equals(11) );
            }
            laserVFX.SetFloat("Lenght", lenght);
            RpcLenghtLaser(lenght);
        }
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
            //actif
            if (electricState && GameManager.GetTime() - startCooldownActif > maxCooldownActifON)
                EndElectric();
            //ulti 
            if (ultiOn &&  GameManager.GetTime() - startCooldownUlti > maxCooldownUltiON)
                EndUlti();
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
            startCooldownActif = GameManager.GetTime();
            electricState = true;
            arms.Play("actif");
        }
    }
}
