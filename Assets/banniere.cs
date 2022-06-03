using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class banniere : MonoBehaviour
    {
        [SerializeField] Material[] diff;
        [SerializeField] GameObject screen;
        [SerializeField] GameObject screen1;

        public bool yes = true;

        // Update is called once per frame
        void Update()
        {
            CheckBan();
        }

        public void CheckBan()
        {
            if (GameManager.players.Count > 0)
            {
                foreach (KeyValuePair<string, Perso> player in GameManager.players)
                {
                    if (player.Value.personnage == "Ennhvala" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().material = diff[0];
                    }
                    if (player.Value.personnage == "Gally" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().material = diff[1];
                    }
                    if (player.Value.personnage == "Idriss" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().material = diff[2];
                    }
                    if (player.Value.personnage == "Tamo" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().material = diff[3];
                    }
                    if (player.Value.personnage == "Ennhvala" && yes)
                    {
                        screen1.GetComponent<MeshRenderer>().material = diff[4];
                        yes = false;
                    }
                    if (player.Value.personnage == "Gally" && yes)
                    {
                        screen1.GetComponent<MeshRenderer>().material = diff[5];
                        yes = false;
                    }
                    if (player.Value.personnage == "Idriss" && yes)
                    {
                        screen1.GetComponent<MeshRenderer>().material = diff[6];
                        yes = false;
                    }
                    if (player.Value.personnage == "Tamo" && yes)
                    {
                        screen1.GetComponent<MeshRenderer>().material = diff[7];
                        yes = false;
                    }
                }
            }
        }
    }
}
