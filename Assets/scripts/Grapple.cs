using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Vector3 grapplePoint;
    public float grappleSpeed = 1f;
    public LayerMask Grappleable;
    public Transform grappleTip, cam, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    float distanceFromPoint = 100f;
    [SerializeField]
    private GameObject grappleHole;


    void Update()
    {
 
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance))
        {
            
            if (Input.GetKeyDown(KeyCode.E) && (distanceFromPoint > 5) && !IsGrappling() && Vector3.Distance(player.position, hit.point) > 5)
            {
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                joint.maxDistance = distanceFromPoint;
                joint.minDistance = 0;

                Instantiate(grappleHole, grapplePoint, Quaternion.LookRotation(hit.normal));
            }
        }
        if (!Input.GetKey(KeyCode.E) || (distanceFromPoint <= 5))
            {
                Destroy(joint);
            }
        

        if (IsGrappling()) 
        {
            player.GetComponent<Rigidbody>().AddForce((grapplePoint - player.position).normalized * grappleSpeed * 0.5f, ForceMode.Acceleration);
            player.GetComponent<Rigidbody>().useGravity = false;           
            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
        }
        else
        {
            distanceFromPoint = 60f;
            grapplePoint = player.position;
        }
            

    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
