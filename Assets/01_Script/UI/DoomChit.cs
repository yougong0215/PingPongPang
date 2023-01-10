using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DoomChit : MonoBehaviour
{
    [SerializeField] GameObject a = null;
    [SerializeField] Color col;
    private void Start()
    {
        transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutElastic).OnComplete(()=>
        {
            transform.DOScale(1, 0.2f).SetEase(Ease.Linear).OnComplete(()=> Start());
        });
    }

    private void OnEnable()
    {
        if(a != null)
         a.SetActive(false);
    }

    public void Up()
    {
        GetComponent<TextMeshProUGUI>().color = col;
    }

    public void Down()
    {
        GetComponent<TextMeshProUGUI>().color = new Color(1,1,1);
    }

}
