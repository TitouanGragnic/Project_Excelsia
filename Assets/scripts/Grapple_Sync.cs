using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Grapple_Sync : NetworkBehaviour
{

    public Transform grappleTip, cam, player;
   
    [SerializeField]
    private GameObject grappleHole;


    [SyncVar]
    public Vector3 grapplePoint;
    [SyncVar]
    public float distanceFromPoint = 100f;
    [SyncVar]
    public bool state;


    [Command(requiresAuthority = false)]
    public void Cmd_jointSync(Vector3 normal , Vector3 point)
    {
        state = true;
        grapplePoint = point;
        NetworkServer.Spawn(Instantiate(grappleHole, grapplePoint, Quaternion.LookRotation(normal)));
    }

    [Command]
    public void Cmd_stat(bool b)
    {
        state = b;
    }


    public bool IsGrappling()
    {
        return state;
    }
 
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    
    public void Cmd_Modified_GP()
    {
        grapplePoint = player.position;
    }

    [Command(requiresAuthority = false)]
    public void Cmd_Changed_dt(float dt)
    {
        distanceFromPoint = dt;
    }

}
