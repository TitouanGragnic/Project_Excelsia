using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotation : MonoBehaviour
{
    [SerializeField] Transform camera;
    public Vector3 pos;

    public Vector3 normalPos;
    public Vector3 crouchPos;

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, camera.rotation.eulerAngles.y, transform.rotation.z));
        transform.localPosition = pos;
    }
    public void Crouch(bool state)
    {
        pos = state ? crouchPos : normalPos;
    }
}


