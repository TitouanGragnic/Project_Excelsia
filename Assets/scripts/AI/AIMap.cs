using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace scripts
{
    public class AIMap : NetworkBehaviour
    {
        [SerializeField]
        AIRoom[] Rooms;

        public bool inGame = false;
        public int time = 0;
        public bool[] stateRoom = { false, false, false, false };


        private void Update()
        {
            if (isServer)
                ServerUpdate();
        }
        void ServerUpdate()
        {
            if (inGame)
                NormalUpdate();
            else
                inGame = GameManager.players.Count > 0;


            

        }

        void NormalUpdate()
        {
            
            time += 1;
            if (!stateRoom[3] && time >= 5000)
                BlockRoom(3);
            else if (!stateRoom[2] && time >= 10000)
                BlockRoom(2);
            else if (!stateRoom[1] && time >= 15000)
                BlockRoom(1);
            else if (!stateRoom[0] && time >= 20000)
                BlockRoom(0);

            if (GameManager.players.Count == 0)
                Restart();
        }

        void Restart()
        {
            foreach (var room in Rooms)
                    room.Open();
        }
        public void BlockRoom(int level)
        {
            stateRoom[level] = true;
            foreach (var room in Rooms)
                if (room.level >= level)
                    room.Close();
        }
    }
}

