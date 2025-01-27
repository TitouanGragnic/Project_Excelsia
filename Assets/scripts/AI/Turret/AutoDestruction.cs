using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoDestruction : NetworkBehaviour
{
    public int maxCooldown;
    public int cooldown;
    public AudioSource EXPLOSION;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = maxCooldown;
        EXPLOSION.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (cooldown > 0)
                cooldown--;
            else
                NetworkServer.Destroy(this.gameObject);
        }
    }
}
