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
        GameObject turret;

        public bool Turret = false;
        public bool Open = false;

        public bool change = false;
        public int time = 500;

        public List<Perso> persoList = new List<Perso> { };

        void Update()
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
            
            Change();
            if (change)
            {
                time -= 1;
                if (time == 0)
                {
                    change = false;
                    time = 2000;
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
            if (persoList.Count>0 &&(  Vector3.Distance(wall.transform.position, persoList[0].transform.position) <= 20 || Vector3.Distance(wall.transform.position, persoList[persoList.Count - 1].transform.position) <= 20))
                Changed();
        }
        public void ChangeWall(bool forced)
        {
            if (forced || !change)
            {
                wall.SetActive(!Open);
                turret.SetActive(Turret);
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

            ChangeWall(forced);
        }
    }
}

