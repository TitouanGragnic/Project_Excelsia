using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Mirror;

namespace scripts
{
    public class DebrisElec : MonoBehaviour
    {
        int start;
        [SerializeField]
        VisualEffect debrisVFX;
        bool on;
        void Start()
        {
            start = GameManager.GetTime();
            on = true;
        }
        public void WallSet(bool state) { debrisVFX.SetBool("Wall", state); }
        // Update is called once per frame
        void Update()
        {
            if (on && GameManager.GetTime() - start > 1)
            {
                debrisVFX.SetBool("Loop", false); 
                on = false;
            }
            if (!on && GameManager.GetTime() - start > 3)
                NetworkServer.Destroy(this.gameObject);

        }
    }
}
