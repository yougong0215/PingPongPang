using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinA : MonoBehaviour
{
    public bool AWin = false;
    Sprite spi;
    // Start is called before the first frame update
    void Start()
    {
        if(AWin == true)
            spi = GameManager.Instance.B;
        else
        {
            spi = GameManager.Instance.A;
        }
        
        GetComponent<SpriteRenderer>().sprite = spi;
    }



}
