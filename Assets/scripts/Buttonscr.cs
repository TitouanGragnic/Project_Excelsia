using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonscr : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image Act;
    [SerializeField] Image Rep;
    // Start is called before the first frame update
    public void Hover()
    {
        button.image = Act;
    }
    public void Exit()
    {
        button.image = Rep;
    }
}
