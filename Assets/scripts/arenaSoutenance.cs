using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class arenaSoutenance : MonoBehaviour
    {
        [SerializeField] GameObject Video;
        [SerializeField] GameObject Other;
        // Start is called before the first frame update
        void Start()
        {
            Video.SetActive(false);
            Other.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Video.SetActive(true);
                Other.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Video.SetActive(false);
                Other.SetActive(true);
            }
        }
    }
}

