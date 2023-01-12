using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public bool AI;
    [SerializeField] Sprite random;
    [SerializeField] List<Sprite> AISPrite = new List<Sprite>();
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
        if(!Char || AI == true)
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

        if(AI == true)
        {
            Two = AISPrite[Random.Range(0,AISPrite.Count-1)];
        }

        if(One != null && Two != null)
        {
            img.interactable = true;
        }


    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(GameObject.Find("Character_Canvas"))
            {
                Destroy(this.gameObject);
            }
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
