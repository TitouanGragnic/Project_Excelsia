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

        private void Start()
        {
            ChangeRoom();
        }
        public void ChangeRoom()
        {
            foreach (var room in Rooms)
            {
                room.ChangeState(false);
            }
        }
    }
}

