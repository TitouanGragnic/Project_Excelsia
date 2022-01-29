using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class BodyLight : NetworkBehaviour
{
    [SerializeField]
    public Light light_up;

    [SyncVar]
    public bool choised;


    void Start()
    {
        light_up.enabled = false;
    }

    void Update()
    {
        light_up.enabled = choised;
    }

    [Command(requiresAuthority = false)]
    public void Select()
    {
        choised = true;
        light_up.enabled = true;
    }

    [Command(requiresAuthority = false)]
    public void UnSelect()
    {
        choised = false;
        light_up.enabled = false;

    }
}

