using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace scripts
{
    public class endAnim : MonoBehaviour
    {
        [SerializeField] VideoClip[] video;
        [SerializeField] VideoPlayer lecteur;
        [SerializeField] GameObject secteur;
        public bool start = true;
        void Start()
        {
            start = true;
            secteur.SetActive(true);
            Checkvid();
            lecteur.Play();
        }

        void Update()
        {
            if (start)
            {
                if (lecteur.isPlaying)
                {
                    start = false;
                }
            }
            else
            {
                if (!lecteur.isPlaying)
                {
                    secteur.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                secteur.SetActive(false);
            }
        }
        public void Checkvid()
        {
            if (GameManager.winner == "Gally")
                lecteur.clip = video[0];
            if (GameManager.winner == "Tamo")
                lecteur.clip = video[1];
            if (GameManager.winner == "Idriss")
                lecteur.clip = video[2];
            if (GameManager.winner == "Ennhvala")
                lecteur.clip = video[3];
        }
    }
}

