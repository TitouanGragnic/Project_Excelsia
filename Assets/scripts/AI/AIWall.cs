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
        public bool Close = false;
        public bool Open = false;

        public bool change = false;
        public int time = 500;

        public List<Perso> persoList = new List<Perso> { };

        void Update()
        {
            if(persoList.Count < 2)
            {
                persoList = new List<Perso> { };
                foreach (KeyValuePair<string, Perso> ex in GameManager.players)
                {
                    persoList.Add(ex.Value);
                }
            }
            
            ChangeWall();
            Change();
            if (change)
            {
                time -= 1;
            }
            if (time == 0)
            {
                change = false;
                time = 500;
                ChangeState();
            }
        }
        private void Change()
        {

            if (Vector3.Distance(wall.transform.position, persoList[0].transform.position) <= 20 || Vector3.Distance(wall.transform.position, persoList[persoList.Count-1].transform.position) <= 20)
            {
                time = 500;
                change = true;
            }
        }
        public void ChangeWall()
        {
            wall.SetActive(!Open);
            turret.SetActive(Turret);
        }
        public void ChangeState()
        {
            if (Open)
            {
                Open = false;
                Turret = true;
                Close = false;
            }
            else if (Turret)
            {
                Open = false;
                Turret = false;
                Close = true;
            }
            else if (Close)
            {
                Open = true;
                Turret = false;
                Close = false;
            }
        }
    }
}

