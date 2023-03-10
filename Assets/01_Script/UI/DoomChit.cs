using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DoomChit : MonoBehaviour
{
    [SerializeField] GameObject a = null;
    [SerializeField] Color col;
    [SerializeField] bool ENDUI;
    [SerializeField] bool IntroUIMove;
    [SerializeField] float gotime = 1.2f;
    private void OnEnable()
    {
        if (IntroUIMove == true && ENDUI == false)
        {
            transform.DOMoveX(transform.position.x - 11, gotime + 0.4f);
        }
        if(a != null)
            a.gameObject.SetActive(false);
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
        a.gameObject.SetActive(true);
    }

    public void Down()
    {
        a.gameObject.SetActive(false);
    }

    public void EXIT()
    {
        Application.Quit();
    }

}
