using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinA : MonoBehaviour
{
    Sprite spi;
    // Start is called before the first frame update
    void Start()
    {
       spi = GameManager.Instance.A;
        GetComponent<SpriteRenderer>().sprite = spi;
    }



}
