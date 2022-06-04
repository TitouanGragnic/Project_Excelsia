using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using Mirror;
using System;
using System.Threading;

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


            multiplier = povcam.multiplier;
            tempMult = multiplier / 10;
        }

        
        void Update()
        {
            CoolDown();
            if(isServer)
                ServerUpdate();

            if (ultiWait)
                SetSensibility(false);
            if (!ultiOn && ! ultiWait)
                SetSensibility(true);

            if (ultiOn && gameObject.layer == 8)
                MajLaser();

        }
        void ServerUpdate()
        {
        }

        public new void Actif()
        {
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif)
                StartElectric();
        }
        public new void Ulti()
        {
            if (!ultiWait && GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti)
                PreUlti();
        }

        float multiplier;
        float tempMult;
        int startPreUlti;
        bool ultiWait;
        int timeBeforeUlti = 1700;
        void PreUlti()
        {
            startPreUlti = GameManager.GetTimeMili();
            ultiWait = true;
            arms.SetBool("ulti", true);
        }

        [SerializeField] POVcam povcam;

        void SetSensibility(bool end)
        {
            if (!end)
                povcam.multiplier =Mathf.Lerp(povcam.multiplier, tempMult,0.1f);
            else
                povcam.multiplier = Mathf.Lerp(povcam.multiplier, multiplier, 0.01f);
        }
        

        void Lightning()
        {
            ultiWait = false;
            ChangeTypeATK("electric");
            CmdLighning(true);
        }

        [Command] void CmdLighning(bool state) 
        {
            RpcLightning(state); 
        }
        void EndUlti()
        {            
            ChangeTypeATK("normal");
            CmdLighning(false);
        }
        [ClientRpc]void RpcLightning(bool state)
        {
            Debug.Log("yes"+state.ToString());
            ultiOn = state;
            if(state)
                startCooldownUlti = GameManager.GetTime();

            if (!state)
            {
                arms.SetBool("ulti", state);
                laserVFX.SetFloat("Lenght", 10);
            }
            laserVFX.SetBool("Loop", state);
        }
        void MajLaser()
        {
            RaycastHit hit;
            float lenght = 200f;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200f, mask))
            {
                lenght = hit.distance;
                if ((hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 8)&&hit.collider.gameObject.GetComponent<Idriss>()==null && GameManager.GetTimeMili() / 100 > predSpawn / 100)
                {
                    CmdPlayerAttack(hit.collider.name, hit.point, 0.5f - atk);
                    Debug.Log("touche");

                }
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
            if (ultiWait && !ultiOn && GameManager.GetTimeMili() - startPreUlti > timeBeforeUlti)
                Lightning();

        }
        private void EndElectric()
        {
            elecVFX.SetBool("Loop", false);
            electricState = false;
            ChangeTypeATK("normal");
            SetEffectLightActif(false);
        }
        [SerializeField] VisualEffect elecVFX;
        private void StartElectric()
        {
            elecVFX.SetBool("Loop",true); ;
            ChangeTypeATK("electric");
            startCooldownActif = GameManager.GetTime();
            electricState = true;
            arms.Play("actif");
            SetEffectLightActif(true);
        }


        [Command]void SetEffectLightActif(bool state) { ClientSetEffectLightActif(state); }
        [ClientRpc] void ClientSetEffectLightActif(bool state) { lightning.SetActive(state);}
    }
}
