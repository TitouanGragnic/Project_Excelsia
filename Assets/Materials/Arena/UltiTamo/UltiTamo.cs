using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UltiTamo : MonoBehaviour
{
    [SerializeField]
    VisualEffect smokeVFX;
    bool on = false;
    public bool arm;

    private void Start()
    {
        if (arm)
            transform.localScale /= 10 ;

    }
    void Update()
    {
        if (on)
            smokeVFX.SetVector3("SpawnPosition", transform.position);
    }

    [SerializeField] LayerMask mask;
    public void Active(bool state)
    {
        on = state;
        smokeVFX.SetBool("Loop", state);
        if (state)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 50f, mask))
                smokeVFX.SetFloat("Ground", hit.point.y);
            else
                smokeVFX.SetFloat("Ground", 0);
        }
    }
}
