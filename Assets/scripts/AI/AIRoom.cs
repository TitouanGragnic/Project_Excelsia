using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System.Security.Cryptography;

namespace scripts
{
    public class AIRoom : NetworkBehaviour
    {
        [SerializeField]
        AIWall[] Walls;

        [SerializeField]
        bool master = false;

        [SerializeField]
        public int level;

        public bool close;
        public int time;
        public int maxTime = 10000;
        private void Start()
        {
            if (isServer)
                ServerStart();
        }
        void ServerStart()
        {

            close = false;
            time = maxTime * 2;
            ChangeState(false);
        }

        void Update()
        {
            if (isServer)
                ServerUpdate();
        }
        void ServerUpdate()
        {
            if (!close)
                NormalUpdate();
            else if (level == 0)
                if (GameManager.IsOnCenter())
                    foreach (var wall in Walls)
                        wall.ChangeState("Turret", true);

        }

        void NormalUpdate()
        {
            if (time > 0)
                time -= 1;
            else
                ChangeState(false);
            if (master)
                testOpen();
        }

        private void testOpen()
        {
            int k = 0;
            foreach(AIWall wall in Walls)
                if (wall.Open)
                    k++;
            if (k < 2)
                ChangeState(true);         
        }

        public void ChangeState(bool forced)
        {
            time = maxTime;

            List<AIWall> alpha = Walls.ToList();
            for (int i = 0; i < alpha.Count; i++)
            {
                AIWall temp = alpha[i];
                temp.Changed();
                int randomIndex = Random.Range(i, alpha.Count);
                alpha[i] = alpha[randomIndex];
                alpha[randomIndex] = temp;
            }
            Walls = alpha.ToArray();

            int op = 0;

            for (int i = 0; i < Walls.Length; i++)
            {
                if (i % 2 == 0 )
                {
                    op++;
                    Walls[i].ChangeState("Open", forced);
                }
                else if (i % 4 == 1 )
                    Walls[i].ChangeState("Turret",forced);
                else if (i % 4 ==3 )
                    Walls[i].ChangeState("Close",forced);
            }
        }

        public void Close()
        {
            close = true;
            if (level != 0)
            {
                foreach (var wall in Walls)
                {
                    if (!wall.isWay)
                        wall.ChangeState("Turret", true);
                    else
                        wall.ChangeState("Open", true);
                    wall.block = true;
                }
            }
            else
            {
                foreach (var wall in Walls)
                    wall.ChangeState("Open", true);
            }

        }
    }
}

