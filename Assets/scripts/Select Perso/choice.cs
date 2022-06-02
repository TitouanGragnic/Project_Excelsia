using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts {
    public class choice : NetworkBehaviour
    {
        [SerializeField][SyncVar]
        public int Pnb;

        [SerializeField]
        GameObject PlayerPrefab;
        [SerializeField]
        GameObject Idriss;
        [SerializeField]
        GameObject Gally;
        [SerializeField]
        GameObject Tamo;
        [SerializeField]
        GameObject Enhvala;
        [SerializeField]
        GameObject image;
        [SerializeField]
        Camera cam;

        [SerializeField]
        private LayerMask mask;

        [SyncVar]
        public bool stateSpawn;

        public bool stateStart;

        BodyLight select_light;

        public bool spawn;
        [SyncVar]
        public bool minchoice;

        private void Start()
        {
            image.SetActive(false);
            minchoice = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            stateSpawn = false;
            spawn = false;
            GameManager.CmdAtributPnb(name);
            transform.rotation = Quaternion.Euler(0,-40,0);
        }
        void Update()
        {
            if (stateStart)
            {
                UpdateChoice();
            }
                
            else
                stateStart = (Input.GetKeyDown(KeyCode.B) || GameManager.GetStateStart());
                 
           
        }
        public void Close()
        {
            image.SetActive(false);
        }

        void UpdateChoice()
        {
            if (GameManager.GetStateSpawn())
            {
                Cmd_ReplacePlayer();
                image.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.B)&& minchoice)
                stateSpawn = true;
            if (Input.GetKeyDown(KeyCode.N))
                stateSpawn = false;


            if (!stateSpawn && Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50, mask))
                {
                    if(hit.collider.name == "jukebox")
                    {
                        image.SetActive(true);
                    }
                    else
                    {
                        Cmd_Work_light(hit, hit.collider.gameObject);
                    }
                }



            }
        }


        
        

        [Command]
        public void Cmd_Select_Prefab(string name)
        {
            switch (name)
            {
                case "Idriss":
                    PlayerPrefab = Idriss;
                    minchoice = true;
                    break;
                case "Gally":
                    PlayerPrefab = Gally;
                    minchoice = true;
                    break;
                case "Tamo":
                    PlayerPrefab = Tamo;
                    minchoice = true;
                    break;
                case "Enhvala":
                    PlayerPrefab = Enhvala;
                    minchoice = true;
                    break;
            }
        }

        
        public void Cmd_Work_light(RaycastHit hit, GameObject obj_collider)
        {

            BodyLight light_collider = obj_collider.GetComponent<BodyLight>();

            if (!light_collider.choised)
            {
                light_collider.Select(Pnb);
                if (select_light != null)
                    select_light.UnSelect();

                select_light = light_collider;
                Cmd_Select_Prefab(hit.collider.name);
            }
        }

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            spawn = true;
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            GameObject newPlayer = Instantiate(PlayerPrefab);
            Perso script = newPlayer.GetComponent<Perso>();
            script.Place(Pnb);
            NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, true);
            NetworkServer.Destroy(oldPlayer);
        }
    } 
}
