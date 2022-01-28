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



        private void Start()
        {
            if (!isLocalPlayer)
            {
                for (int i = 0; i < componentsToDisable.Length; i++)
                {
                    componentsToDisable[i].enabled = false;
                }
                gameObject.layer = LayerMask.NameToLayer(name_layer);

                if (GetComponent<Perso>() != null)
                {
                    GameObject hp_object = gameObject.transform.Find("HealthBar").gameObject;
                    hp_object.layer = LayerMask.NameToLayer(nameHP_layer);

                    SetLayerRecursively(hp_object);
                }

            }
        }

        private void SetLayerRecursively(GameObject obj)
        {
            obj.layer = LayerMask.NameToLayer(nameHP_layer);

            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject);
            }
        }
        public override void OnStartClient()
        {
            base.OnStartClient();

            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            Perso player = GetComponent<Perso>();

            if (player != null)
                GameManager.RegisterPlayer(netId, player);
            else
                GameManager.RegisterChoice(netId, GetComponent<choice>());
        }

        private void OnDisable()
        {
            GameManager.UnRegisterPlayer(transform.name);
        }
    }
}
