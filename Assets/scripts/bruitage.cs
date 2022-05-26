using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class bruitage : MonoBehaviour
    {
        [SerializeField] AudioSource lecteur;
        [SerializeField] AudioClip[] soundBoard;
        [SerializeField] Movement movement;
        [SerializeField] Perso player;

        public float life;

        void Start()
        {
            life = player.health;
        }
        void Update()
        {
            if(player.health < life)
            {
                life = player.health;
                if (lecteur.clip != soundBoard[7])
                    lecteur.Stop();
                lecteur.clip = soundBoard[7];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (movement.isSprinting && movement.isGrounded)
            {
                if (lecteur.clip != soundBoard[1])
                    lecteur.Stop();
                lecteur.clip = soundBoard[1];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (movement.isGrounded && !movement.isSprinting && !movement.isCrouching && Input.GetAxisRaw("Vertical") > 0)
            {
                if (lecteur.clip != soundBoard[2])
                    lecteur.Stop();
                lecteur.clip = soundBoard[2];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (Input.GetKeyDown(movement.jumpkey) && movement.doubleJump > 0 && (!(movement.wallLeft | movement.wallright) | (movement.isGrounded && (movement.wallright | movement.wallLeft))))
            {
                if (lecteur.clip != soundBoard[0])
                    lecteur.Stop();
                lecteur.clip = soundBoard[0];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (movement.isSliding)
            {
                if (lecteur.clip != soundBoard[3])
                    lecteur.Stop();
                lecteur.clip = soundBoard[3];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (movement.isGrounded && movement.isCrouching && !movement.isSliding)
            {
                if (lecteur.clip != soundBoard[4])
                    lecteur.Stop();
                lecteur.clip = soundBoard[4];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (lecteur.clip != soundBoard[5] && lecteur.clip != soundBoard[6])
                    lecteur.Stop();
                if (lecteur.clip == soundBoard[5])
                    lecteur.clip = soundBoard[6];
                else
                    lecteur.clip = soundBoard[5];
                if (!lecteur.isPlaying)
                    lecteur.Play();
            }
        }
    }
}

