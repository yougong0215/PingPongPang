using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISquence : MonoBehaviour
{

    Vector3 A_pos;
    Vector3 B_pos;

    [SerializeField] GameObject UI_A;
    [SerializeField] GameObject UI_B;

    public bool SequenceEnd = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Reset()
    {

        A_pos = UI_A.transform.position;
        B_pos = UI_B.transform.position;
        SequenceEnd = false;
        UI_A.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.A;
        UI_B.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.B;
        UI_A.transform.position = new Vector3(-4, -0.3f);
        UI_B.transform.position = new Vector3(4, -0.3f);
        UI_A.transform.localScale = new Vector3(1, 1, 1);
        UI_B.transform.localScale = new Vector3(1, 1, 1);
    }


    public void RoundWinPlayer1()
    {
        Reset();
        UI_A.transform.DOMove(new Vector3(0, 0, 0), 1.2f);
        UI_A.transform.DOScale(2, 1.2f).OnComplete(()=> SequenceEnd = true);
        UI_B.transform.DOMoveX(100, 1);
    }

    public void RoundWinPlayer2()
    {
        Reset();
        UI_B.transform.DOMove(new Vector3(0, 0, 0), 1.2f);
        UI_B.transform.DOScale(2, 1.2f).OnComplete(() => SequenceEnd = true);
        UI_A.transform.DOMoveX(-100, 1);
    }


    public void RoundWinPlayerOther()
    {
        Reset();
        UI_A.transform.DOScale(2, 1.2f).OnComplete(() => SequenceEnd = true);
        UI_B.transform.DOScale(2, 1.2f);
    }

    public void GameWinA()
    {
        Reset();
        UI_A.transform.DOMove(new Vector3(0, 0, 0), 2f).OnComplete(() => SequenceEnd = true);
        UI_A.transform.DOScale(10, 1.8f);
        UI_B.transform.DOMoveX(-100, 0.6f);
    }
    public void GameWinB()
    {
        Reset();
        UI_B.transform.DOMove(new Vector3(0, 0, 0), 2f).OnComplete(() => SequenceEnd = true);
        UI_B.transform.DOScale(10, 1.8f);
        UI_A.transform.DOMoveX(-100, 0.6f);
    }
}
