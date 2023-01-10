using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // �ڽ�
    // 0�� �̸�
    // 1�� �̹���
    // 2�� ����
    // 3�� �µθ�
    // 4�� ī�� ���̽� ����

    PlayerEnum pl;
    bool Choosed = false;
    Sprite _spriteSave;

    public void Set( string t, Sprite img, Sprite card, string ex, PlayerEnum pl, Sprite ab, bool Choosed  = false)
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

        if(Choosed == false)
            GameManager.Instance.GameStart();
        else
        {
            Choosed = false;
            GameManager.Instance.GameSet2();
        }
        
    }
    public void Choose()
    {
        StartCoroutine(ch());
    }


}
