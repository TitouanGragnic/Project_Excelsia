using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; 
using Mirror;
public class Blood : NetworkBehaviour
{
    [SerializeField]
    VisualEffect shader;
    public int cooldown = 20;
    [SerializeField]
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion temp = transform.rotation;
        transform.rotation = new Quaternion(0,0,0,0);

        RaycastHit hit1;
        RaycastHit hit2;
        Physics.Raycast(transform.position, Vector3.down, out hit1,2000f, mask);
        Physics.Raycast(transform.position, Vector3.up, out hit2, 2000f, mask);

        float sy = hit1.distance + hit2.distance;
        float y = (sy / 2 - (hit1.distance < hit2.distance ? hit1.distance : hit2.distance)) * (hit1.distance < hit2.distance ? -1 : 1);


        Physics.Raycast(transform.position, Vector3.back, out hit1, 2000f, mask);
        Physics.Raycast(transform.position, Vector3.forward, out hit2, 2000f, mask);

        float sz = hit1.distance + hit2.distance;
        float z = (sz / 2 - (hit1.distance < hit2.distance ? hit1.distance  : hit2.distance )) * (hit1.distance < hit2.distance ? 1 : -1);


        Physics.Raycast(transform.position, Vector3.left, out hit1, 2000f, mask);
        Physics.Raycast(transform.position, Vector3.right, out hit2, 2000f, mask);

        float sx = hit1.distance + hit2.distance;
        float x = (sx / 2 - (hit1.distance < hit2.distance ? hit1.distance  : hit2.distance )) * (hit1.distance < hit2.distance ? 1 : -1);

        shader.SetVector3("Center", new Vector3(x,y,z));
        shader.SetVector3("Size", new Vector3(sx, sy, sz));
        transform.rotation = temp;


        shader.SetBool("Loop", true);
    }

    private void Update()
    {
        if (isServer)
            ServerUpdate();
    }
    // Update is called once per frame
    void ServerUpdate()
    {
        if (cooldown > 0)
            cooldown--;        
        else if(cooldown<-100)
            NetworkServer.Destroy(this.gameObject);
        else
            shader.SetBool("Loop", false);
    }
}
