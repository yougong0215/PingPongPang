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

    public void Set(string t, Sprite img, string ex, PlayerEnum pl)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = t;
        //transform.GetChild(1).GetComponent<Image>().sprite = img;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ex;
        this.pl = pl;
    }

    public void Choose()
    {
        for(int i = 4; i< transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<BaseBallAbility>())
            {
                if (pl == PlayerEnum.A)
                    GameManager.Instance.a_ability1.Add(transform.GetChild(i).GetComponent<BaseBallAbility>());
                if (pl == PlayerEnum.B)
                    GameManager.Instance.b_ability1.Add(transform.GetChild(i).GetComponent<BaseBallAbility>());
            }

            if (transform.GetChild(i).GetComponent<BasePlayerAbility>())
            {
                if (pl == PlayerEnum.A)
                    GameManager.Instance.a_ability2.Add(transform.GetChild(i).GetComponent<BasePlayerAbility>());
                if (pl == PlayerEnum.B)
                    GameManager.Instance.b_ability2.Add(transform.GetChild(i).GetComponent<BasePlayerAbility>());
            }
        }
    }


}
