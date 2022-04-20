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

        public int time = 20000;
        private void Start()
        {
            ChangeState();
        }

        void Update()
        {
            if (time>0)
                time -= 1;
            else
                ChangeState();
        }

        public void ChangeState()
        {
            time = 10000;
            for (int i = 0; i < Walls.Length; i++)
            {
                if (i % 3 == 0)
                {
                    Walls[i].Open = true;
                    Walls[i].Close = false;
                    Walls[i].Turret = false;
                }
                if (i % 3 == 1)
                {
                    Walls[i].Open = false;
                    Walls[i].Close = false;
                    Walls[i].Turret = true;
                }
                if (i % 3 == 2)
                {
                    Walls[i].Open = false;
                    Walls[i].Close = true;
                    Walls[i].Turret = false;
                }
            }

            List<AIWall> alpha = Walls.ToList();
            for (int i = 0; i < alpha.Count; i++)
            {
                AIWall temp = alpha[i];
                int randomIndex = Random.Range(i, alpha.Count);
                alpha[i] = alpha[randomIndex];
                alpha[randomIndex] = temp;
            }
            Walls = alpha.ToArray();
        }
    }
}

