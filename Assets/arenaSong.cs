using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class arenaSong : MonoBehaviour
    {
        [SerializeField] GameObject song;
        void Start()
        {
            song.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.players.Count > 1)
            {
                song.SetActive(true);
            }
        }
    }
    
}

