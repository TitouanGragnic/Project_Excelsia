using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class POVcam : MonoBehaviour
    {
        [SerializeField] Movement playerMove;

        [SerializeField] private float sensX;
        [SerializeField] private float sensY;

        [SerializeField] Transform cam;
        [SerializeField] Transform orientation;

        float mouseX;
        float mouseY;

        public float multiplier = 0.1f;

        float xRotation;
        public float yRotation;

        public bool place;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (!place)
                MyInput();
            else
                place = false;


            cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, playerMove.tilt);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);


        }

        void MyInput()
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }

    }
}
