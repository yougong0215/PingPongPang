using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DG.Tweening;

public class MapKeyUnlock : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] TextMeshPro tmp;

    int HitNum = 3;
    public void HitGimik()
    {
        HitNum--;

        if (HitNum <= 0)
        {
            obj.transform.DOMoveY(100, 1.2f);
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<AudioSource>().Play();
        }
    }
    private void Update()
    {
        if(HitNum >= 0)
            tmp.text = $"{HitNum}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            HitGimik();
        }
    }
}
