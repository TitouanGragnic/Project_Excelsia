using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


namespace scripts
{

    public class trailer_lobby : MonoBehaviour
    {
        [SerializeField] GameObject Canvas;
        [SerializeField] GameObject Trailer;
        [SerializeField] GameObject Network;
        [SerializeField] VideoPlayer Video;

        public bool start;
        // Start is called before the first frame update
        void Start()
        {
            start = true;
            Canvas.SetActive(false);
            Network.SetActive(false);
            Trailer.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (start)
            {
                if (Video.isPlaying)
                {
                    start = false;
                }
            }
            else
            {
                if (!Video.isPlaying)
                {
                    Canvas.SetActive(true);
                    Network.SetActive(true);
                    Trailer.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Canvas.SetActive(true);
                Network.SetActive(true);
                //Video.SetActive(false);
            }
        }
    }
}


