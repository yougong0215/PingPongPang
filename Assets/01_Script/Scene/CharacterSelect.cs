using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Button img;

    [SerializeField] Image f_img;
    [SerializeField] Image s_img;

    Sprite One;
    Sprite Two;

    bool Char = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Select(Sprite spi)
    {
        if(!Char)
        {
            f_img.sprite = spi;
            One = spi;
            Char = true;
        }
        else
        {
            s_img.sprite = spi;
            Two = spi;
            Char = false;
        }

        if(One != null && Two != null)
        {
            img.interactable = true;
        }


    }

    public Sprite GetA()
    {
        return One;
    }
    public Sprite GetB()
    {
        return Two;
    }

}
