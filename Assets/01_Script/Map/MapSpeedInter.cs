using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSpeedInter : MonoBehaviour
{
    [SerializeField] float HitNum = 3;
    [SerializeField] TextMeshPro tmp;
    [SerializeField] float speed = 1;

    public void HitGimik(PlayerInterrabter pl)
    {
        HitNum--;

        if (HitNum <= 0)
        {
            pl.MapGimicspeed = speed;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private void Update()
    {
        if (HitNum >= 0)
            tmp.text = $"{HitNum}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            HitGimik(collision.GetComponent<Ball>().pl);
        }
    }



}
