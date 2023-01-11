using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DoomChit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI a = null;
    [SerializeField] Color col;
    [SerializeField] bool IntroUIMove;
    [SerializeField] float gotime = 1.2f;
    private void OnEnable()
    {
        if (IntroUIMove == true)
        {
            transform.DOMoveX(transform.position.x - 1200, gotime + 0.4f);
        }
    }

    private void Start()
    {
        if(IntroUIMove == false)
        transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutElastic).OnComplete(()=>
        {
            transform.DOScale(1, 0.2f).SetEase(Ease.Linear).OnComplete(()=> uimove());
        });
    }

    void uimove()
    {
        transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {
            transform.DOScale(1, 0.2f).SetEase(Ease.Linear).OnComplete(() => uimove());
        });
    }

    public void Up()
    {
        a.GetComponent<TextMeshProUGUI>().color = col;
    }

    public void Down()
    {
        a.GetComponent<TextMeshProUGUI>().color = new Color(1,1,1);
    }

}
