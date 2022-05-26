using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Slash : NetworkBehaviour
{
    public float speed = 30;
    public float slowDownRate = 0.01f;
    public float detectingDistance = 0.1f;
    public float destroyDelay = 1000;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    LayerMask mask;
    private bool stopped;
    float y = 0;
    void Start()
    {
        RaycastHit hit;
        Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        
        if (Physics.Raycast(distance, Vector3.down, out hit, 20f, mask))
            y = hit.point.y;
        transform.position = new Vector3(transform.position.x,y,transform.position.z);
        StartCoroutine(SlowDown());
    }

    // Update is called once per frame
    void Update()
    {
        destroyDelay--;
        if (destroyDelay < 0)
            NetworkServer.Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!stopped)
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            if(Physics.Raycast(distance, Vector3.down, out hit, 200f, mask))
                y =hit.point.y;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
        }
    }

    IEnumerator SlowDown()
    {
        float t = 1;
        while (t > 0)
        {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }
        stopped = true;
    }


}
