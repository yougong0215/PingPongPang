using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] Transform pos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            collision.transform.position = pos.position;
            if(pos.transform.position.x > 0)
            {
                collision.GetComponent<Ball>().Origin_angle += new Vector2(0, Random.Range(-1f, 1f));
            }

            StartCoroutine(t(collision.GetComponent<Ball>()));

        }
    }

    IEnumerator t(Ball b)
    {
        float t = b.Origin_speed;
        b.Origin_speed = 0.1f;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.9f);

        b.Origin_speed = t*2;
    }
}
