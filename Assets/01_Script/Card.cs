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

    public void Set( string t, Sprite img, Sprite card, string ex, PlayerEnum pl)
    {
        GetComponent<RectTransform>().position = transform.parent.GetComponent<RectTransform>().position;
        GetComponent<RectTransform>().rotation = transform.parent.GetComponent<RectTransform>().rotation;
        GetComponent<Image>().sprite = card;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = t;
        transform.GetChild(1).GetComponent<Image>().sprite = img;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ex;
        this.pl = pl;
    }

    IEnumerator ch()
    {
        for (int i = 4; i < transform.childCount; i++)
        {
            yield return null;
            if (transform.GetChild(i).GetComponent<BaseBallAbility>())
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

        GameManager.Instance.GameStart();

    }
    public void Choose()
    {
        StartCoroutine(ch());
    }


}
