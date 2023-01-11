using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapInfinInter : MonoBehaviour
{

    [SerializeField] float HitNum = 3;
    [SerializeField] TextMeshPro tmp;

    public void HitGimik(PlayerInterrabter pl)
    {
        HitNum--;

        if(HitNum <= 0)
        {
            pl.PlayerInfin = true;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        tmp.text = $"{HitNum}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ball>())
        {
            HitGimik(collision.GetComponent<Ball>().pl);
        }
    }
}
