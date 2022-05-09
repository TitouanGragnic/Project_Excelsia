using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class AIWall : NetworkBehaviour
    {
        [SerializeField]
        GameObject wall;
        
        [SerializeField]
        Turret[] turrets;


        public bool isWay;
        [SyncVar]public bool block;
        [SyncVar]public bool Turret = false;
        [SyncVar]public bool Open = false;
        [SyncVar]public bool change = false;
        public int time = 500;

        public List<Perso> persoList = new List<Perso> { };

        void Start()
        {
            foreach (var turret in turrets)
                turret.isServer = isServer;
        }

        void Update() 
        {

            ChangeWall(block);
            if (isServer)
                ServerUpdate();
        }
        void ServerUpdate()
        {
            if (persoList.Count != GameManager.players.Count)
            {
                if (persoList.Count < 2)
                {
                    persoList = new List<Perso> { };
                    foreach (KeyValuePair<string, Perso> ex in GameManager.players)
                    {
                        persoList.Add(ex.Value);
                    }
                }
            }
            if (!block)
                NormalUpdate();
        }
        void NormalUpdate()
        {
            
            
            Change();
            if (change)
            {
                time -= 1;
                if (time == 0)
                {
                    change = false;
                    time = 500;
                    ChangeWall(false);
                }
            }
            
        }

        public void Changed()
        {
            time = 500;
            change = true;
        }
        private void Change()
        {
            if (persoList.Count>0 &&((persoList[0] != null && Vector3.Distance(wall.transform.position, persoList[0].transform.position) <= 20 )|| (persoList[persoList.Count - 1] != null && Vector3.Distance(wall.transform.position, persoList[persoList.Count - 1].transform.position) <= 20)))
                Changed();
        }

        public void ChangeWall(bool forced)
        {
            if (forced || !change)
            {
                this.GetComponent<Wall>().activ  = !Open;
                foreach (Turret turret in turrets)
                    turret.activ = Turret;
            }
        }
        public void ChangeState(string new_state, bool forced)
        {
            switch (new_state)
            {
                case "Open":
                    Open = true;
                    Turret = false;
                    break;
                case "Close":
                    Open = false;
                    Turret = false;
                    break;
                case "Turret":
                    Open = false;
                    Turret = true;
                    break;
            }

            RcpChangeState(new_state, forced);
            ChangeWall(forced);
        }

        [ClientRpc]
        public void RcpChangeState(string new_state, bool forced)
        {
            switch (new_state)
            {
                case "Open":
                    Open = true;
                    Turret = false;
                    break;
                case "Close":
                    Open = false;
                    Turret = false;
                    break;
                case "Turret":
                    Open = false;
                    Turret = true;
                    break;
            }
            ChangeWall(forced);

        }

    }
}

