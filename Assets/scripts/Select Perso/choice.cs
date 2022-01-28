using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts {
    public class choice : NetworkBehaviour
    {
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
        public bool state;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            state = false;
        }
        void Update()
        {

            if (GameManager.GetStateSpawn())
                Cmd_ReplacePlayer();


            if (Input.GetKeyDown(KeyCode.B))
                state = true;
            if (Input.GetKeyDown(KeyCode.N))
                state = false;


            if (!state && Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50, mask))
                {
                    Cmd_choice_prefab(hit.collider.name);
                }



            }
        }


        [Command]
        [Client]
        void Cmd_choice_prefab(string name)
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

        [Command][Client]
        public void Cmd_ReplacePlayer()
        {
            GameObject oldPlayer = connectionToClient.identity.gameObject;
            NetworkServer.ReplacePlayerForConnection(connectionToClient, Instantiate(PlayerPrefab), true);
            NetworkServer.Destroy(oldPlayer);
            
        }






    } 
}
