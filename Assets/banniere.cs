using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class banniere : MonoBehaviour
    {
        [SerializeField] Texture[] diff;
        [SerializeField] GameObject screen;

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
                        screen.GetComponent<MeshRenderer>().materials[3].SetTexture("ennhvala", diff[0]);
                    }
                    if (player.Value.personnage == "Gally" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[3].SetTexture("gally", diff[1]);
                    }
                    if (player.Value.personnage == "Idriss" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[3].SetTexture("idriss", diff[2]);
                    }

                    if (player.Value.personnage == "Tamo" && !yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[3].SetTexture("tamo", diff[3]);
                    }
                    if (player.Value.personnage == "Ennhvala" && yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[0].SetTexture("ennhvala", diff[0]);
                        yes = false;
                    }
                    if (player.Value.personnage == "Gally" && yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[0].SetTexture("gally", diff[1]);
                        yes = false;
                    }
                    if (player.Value.personnage == "Idriss" && yes)
                    {
                        Debug.Log(player.Value.personnage);
                        screen.GetComponent<MeshRenderer>().materials[0].SetTexture("idriss", diff[2]);
                        Debug.Log(screen.GetComponent<MeshRenderer>().materials[0]);
                        Debug.Log(diff[2]);
                        yes = false;
                    }
                    if (player.Value.personnage == "Tamo" && yes)
                    {
                        screen.GetComponent<MeshRenderer>().materials[0].SetTexture("tamo", diff[3]);
                        yes = false;
                    }
                }
                yes = true;
            }
        }
    }
}
