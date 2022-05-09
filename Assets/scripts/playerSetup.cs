using UnityEngine;
using Mirror;

namespace scripts
{
    public class playerSetup : NetworkBehaviour
    {
        [SerializeField]
        Behaviour[] componentsToDisable;
        [SerializeField]
        private string name_layer = "client";
        [SerializeField]
        private string nameHP_layer = "HP_client";
        [SerializeField]
        private string body_layer = "body_client";
        [SerializeField]
        private string arm_layer = "Arm_client";



        private void Start()
        {
            if (!isLocalPlayer)
            {
                for (int i = 0; i < componentsToDisable.Length; i++)
                {
                    componentsToDisable[i].enabled = false;
                }
                gameObject.layer = LayerMask.NameToLayer(name_layer);
                Perso perso = GetComponent<Perso>();
                if ( perso != null)
                {
                    perso.blur.SetActive(false);
                    GameObject hp_object = gameObject.transform.Find("HealthBar").gameObject;
                    hp_object.layer = LayerMask.NameToLayer(nameHP_layer);

                    SetLayerRecursively(hp_object, nameHP_layer);

                    perso.body.layer = LayerMask.NameToLayer(body_layer);

                    SetLayerRecursively(perso.body, body_layer);

                    perso.blur.layer = LayerMask.NameToLayer(arm_layer);
                    perso.arm.layer = LayerMask.NameToLayer(arm_layer);

                    SetLayerRecursively(perso.arm, arm_layer);



                }
                

            }
        }

        private void SetLayerRecursively(GameObject obj,string layer)
        {
            obj.layer = LayerMask.NameToLayer(layer);

            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
        public override void OnStartClient()
        {
            base.OnStartClient();

            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            Perso player = GetComponent<Perso>();
            choice Choice = GetComponent<choice>();

            if (player != null)
                GameManager.RegisterPlayer(netId, player);
            else if (Choice != null)
                GameManager.RegisterChoice(netId, Choice);
            else
                GameManager.RegisterEnd(netId, GetComponent<GameOver>());

        }

        private void OnDisable()
        {
            if (gameObject.GetComponent<Perso>() != null)
                GameManager.UnRegisterPlayer(transform.name);
            else if (gameObject.GetComponent<choice>() != null)
                GameManager.UnRegisterChoice(transform.name);
            else
                GameManager.UnRegisterEnd(transform.name);
        }
    }
}
