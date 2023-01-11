using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSommon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Ball>())
        {
            GameObject obj = Instantiate(collision.gameObject);

            obj.GetComponent<Ball>().Origin_angle = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            obj.transform.parent = collision.transform.parent;
        }
    }
}
