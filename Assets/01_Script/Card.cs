using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // 자식
    // 0번 이름
    // 1번 이미지
    // 2번 설명
    // 3번 태두리
    // 4번 카드 베이스 색상

    PlayerEnum pl;
    bool Choosed = false;
    Sprite _spriteSave;

    int o = 0;

    Vector3 vec;

    AbilityCard abb;
    bool ai = false;

    public void Set( string t, Sprite img, Sprite card, string ex, PlayerEnum pl, Sprite ab, AbilityCard abb,bool Choosed  = false, bool AImode = false)
    {
        GetComponent<RectTransform>().position = transform.parent.GetComponent<RectTransform>().position;
        GetComponent<RectTransform>().rotation = transform.parent.GetComponent<RectTransform>().rotation;
        GetComponent<Image>().sprite = card;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = t;
        transform.GetChild(1).GetComponent<Image>().sprite = img;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ex;
        this.pl = pl;
        this.Choosed = Choosed;
        _spriteSave = ab;
        Alpha();
        this.abb = abb;

    }
    

    IEnumerator ch()
    {
        if(pl == PlayerEnum.A)
        {
            GameManager.Instance.A_SpriteAdd(_spriteSave);
        }
        else
        {
            GameManager.Instance.B_SpriteAdd(_spriteSave);

        }


        for (int i = 3; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<BaseBallAbility>())
            {
                yield return null;
                if (pl == PlayerEnum.A)
                    GameManager.Instance.a_ability1.Add(transform.GetChild(i).GetComponent<BaseBallAbility>());
                if (pl == PlayerEnum.B)
                    GameManager.Instance.b_ability1.Add(transform.GetChild(i).GetComponent<BaseBallAbility>());
            }
            if (transform.GetChild(i).GetComponent<BasePlayerAbility>())
            {
                yield return null;
                if (pl == PlayerEnum.A)
                    GameManager.Instance.a_ability2.Add(transform.GetChild(i).GetComponent<BasePlayerAbility>());
                if (pl == PlayerEnum.B)
                    GameManager.Instance.b_ability2.Add(transform.GetChild(i).GetComponent<BasePlayerAbility>());
            }
            if(transform.GetChild(i).GetComponent<ALLAbility>())
            {
                yield return null;
                GameManager.Instance.All.Add(transform.GetChild(i).GetComponent<ALLAbility>());
            }
        }

        GameManager.Instance.cl._cardList.Find(a => a == abb).CardLuck--;


        if (Choosed == false)
            GameManager.Instance.GameStart();
        else
        {
            Choosed = false;
            GameManager.Instance.GameSet2();
        }
        GameManager.Instance.cl._cardListed.Clear();
    }
    public void Alpha() 
    {
        GetComponent<Image>().color = new Color(1, 1,1, 0.7f);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1,1, 0.7f);
        transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1,1, 0.7f);
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(1, 1,1, 0.7f);
    }

    public void NoAlpha()
    {
        GetComponent<Image>().color = new Color(1, 1, 1f, 1f);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1f, 1f);
        transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1f, 1);
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1f, 1);
    }
    public void Choose()
    {
        StartCoroutine(ch());
    }


}
