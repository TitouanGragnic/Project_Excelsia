using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] Movement playerMove;
    Vector3 initPos;

    void Update()
    {
        transform.position = cameraPosition.position + Vector3.down * playerMove.camTranslate;
    }
}
