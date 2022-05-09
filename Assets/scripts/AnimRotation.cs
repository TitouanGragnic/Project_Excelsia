using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotation : MonoBehaviour
{
    [SerializeField] Transform camera;

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, camera.rotation.eulerAngles.y, transform.rotation.z));
    }
}


