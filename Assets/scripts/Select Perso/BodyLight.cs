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

    [SerializeField]
    GameObject P1;
    [SerializeField]
    GameObject P2;

    [SerializeField]
    [SyncVar]
    public int Pnb;




    void Start()
    {
        light_up.enabled = false;
        P1.SetActive(false);
        P2.SetActive(false);
        Pnb = 0;

    }

    void Update()
    {
        light_up.enabled = choised;
        UpdateGOup();
    }

    void UpdateGOup()
    {
        if (Pnb == 1)
            P1.SetActive(true);
        else
            P1.SetActive(false);
        if (Pnb == 2)
            P2.SetActive(true);
        else
            P2.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void Select(int Pnb)
    {
        this.Pnb = Pnb;
        choised = true;
        light_up.enabled = true;
    }

    [Command(requiresAuthority = false)]
    public void UnSelect()
    {
        Pnb = 0;
        choised = false;
        light_up.enabled = false;

    }
}

