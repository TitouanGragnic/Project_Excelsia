using UnityEngine;
using Mirror;
using UnityEngine.UI;
namespace scripts {
    public class Grapple : MonoBehaviour
    {

        //public Vector3 grapplePoint;
        public float grappleSpeed = 1f;
        public LayerMask Grappleable;
        public Transform grappleTip, cam, player;
        private float maxDistance = 100f;

        public SpringJoint joint;
        //float distanceFromPoint = 100f;
        [SerializeField]
        private GameObject grappleHole;

        [SerializeField] public Slider slider;

        public int startGrappleCooldown;
        public int maxGrappleCooldown;

        [SerializeField]
        Grapple_Sync grapple_Sync;

        private void Awake()
        {
            maxGrappleCooldown = 5000;
            startGrappleCooldown = GameManager.GetTimeMili();
        }




        void Update()
        {
            if (slider != null)
            {
                slider.value = GameManager.GetTimeMili() - startGrappleCooldown > maxGrappleCooldown ? 1 : (float)(GameManager.GetTimeMili() - startGrappleCooldown) / (float)maxGrappleCooldown; ;
            }
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance))
            {

                if (Input.GetKeyDown(KeyCode.E) && (grapple_Sync.distanceFromPoint > 5) && !grapple_Sync.IsGrappling() && Vector3.Distance(player.position, hit.point) > 5 && GameManager.GetTimeMili()-startGrappleCooldown > maxGrappleCooldown)
                {
                    grapple_Sync.Cmd_Changed_dt(Vector3.Distance(player.position, hit.point));
                    joint = player.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grapple_Sync.grapplePoint;


                    joint.maxDistance = grapple_Sync.distanceFromPoint + 1000;
                    joint.minDistance = 0;
                    grapple_Sync.Cmd_jointSync(hit.normal, hit.point);

                }
            }
            if (Input.GetKeyUp("e") || (grapple_Sync.distanceFromPoint <= 5))
            {
                Destroy(joint);
                if (grapple_Sync.state)
                    startGrappleCooldown = GameManager.GetTimeMili();
                grapple_Sync.Cmd_stat(false);
            }


            if (grapple_Sync.IsGrappling())
            {
                player.GetComponent<Rigidbody>().AddForce((grapple_Sync.grapplePoint - player.position).normalized * grappleSpeed * 0.5f, ForceMode.Acceleration);
                player.GetComponent<Rigidbody>().useGravity = false;
                grapple_Sync.Cmd_Changed_dt(Vector3.Distance(player.position, grapple_Sync.grapplePoint));

            }
            else
            {
                if (!player.GetComponent<Movement>().wallLeft && !player.GetComponent<Movement>().wallright)
                    player.GetComponent<Rigidbody>().useGravity = true;


                grapple_Sync.Cmd_Modified_GP();
                grapple_Sync.Cmd_Changed_dt(60f);


            }


        }


    }
    
}
