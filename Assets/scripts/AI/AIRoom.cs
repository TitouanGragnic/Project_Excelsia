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

        private void Start()
        {
            ChangeState();
        }

        public void ChangeState()
        {
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

