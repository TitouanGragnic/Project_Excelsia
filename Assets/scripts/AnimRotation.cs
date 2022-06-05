using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimRotation : NetworkBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform target;
    public Vector3 pos;

    public Vector3 normalPos;
    public Vector3 crouchPos;

    void Update()
    {
        target.transform.rotation = Quaternion.Euler(new Vector3(target.transform.rotation.x, camera.rotation.eulerAngles.y, target.transform.rotation.z));
        target.transform.localPosition = pos;

        CmdFixPos(target.transform.position, target.transform.rotation);
    }
    [Command(requiresAuthority = false)]
    public void Crouch(bool state)
    {
        RpcCrouch(state);
    }
    [ClientRpc]
    void RpcCrouch(bool state)
    {
        pos = state ? crouchPos : normalPos;
    }

    [Command(requiresAuthority = false)]
    void CmdFixPos(Vector3 pos, Quaternion rot)
    {
        RpcFixPos(pos, rot);
    }

    [ClientRpc]
    void RpcFixPos(Vector3 pos, Quaternion rot)
    {
        target.transform.position = pos;
        target.transform.rotation = rot;
    }
}


