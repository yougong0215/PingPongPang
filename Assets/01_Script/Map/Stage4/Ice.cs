using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Rigidbody2D _rigid;
    Vector3 a;
    [SerializeField] LayerMask ly;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _rigid.velocity = a * 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapUp")) // ¿≠∫Æ æ∆∑ø∫Æ ø°∞‘ ¥Í¿∏∏È
        {

            a.y *= -1;

        }
        if (collision.gameObject.CompareTag("MapDown")) // ¿≠∫Æ æ∆∑ø∫Æ ø°∞‘ ¥Í¿∏∏È
        {

            a.y *= -1;

        }
        if (collision.gameObject.tag == "A")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "B")
        {
            Destroy(collision.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            a = (transform.position - a).normalized;
            _rigid.velocity = a * 1.5f;
            gameObject.layer = 6;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }
}
