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
        Camera cam;

        [SerializeField]
        private LayerMask mask;

        [SyncVar]
        public bool stateSpawn;

        public bool stateStart;

        BodyLight select_light;

        public bool spawn;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            stateSpawn = false;
            spawn = false;
            GameManager.CmdAtributPnb(name);
        }
        void Update()
        {
            if (stateStart)
                UpdateChoice();
            else
                stateStart = (Input.GetKeyDown(KeyCode.B) || GameManager.GetStateStart());
                 
           
        }


        void UpdateChoice()
        {
            if (GameManager.GetStateSpawn())
                Cmd_ReplacePlayer();


            if (Input.GetKeyDown(KeyCode.B))
                stateSpawn = true;
            if (Input.GetKeyDown(KeyCode.N))
                stateSpawn = false;


            if (!stateSpawn && Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50, mask))
                {

                    Cmd_Work_light(hit, hit.collider.gameObject);
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
                    break;
                case "Gally":
                    PlayerPrefab = Gally;
                    break;
                case "Tamo":
                    PlayerPrefab = Tamo;
                    break;
                case "Enhvala":
                    PlayerPrefab = Enhvala;
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
            NetworkServer.ReplacePlayerForConnection(connectionToClient, Instantiate(PlayerPrefab), true);
            NetworkServer.Destroy(oldPlayer);
            
        }






    } 
}
