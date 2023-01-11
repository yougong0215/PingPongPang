using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            Vector3 a = collision.transform.position;
            _rigid.velocity = (a - transform.position).normalized * 1.5f;
        }

        if (collision.gameObject.CompareTag("MapUp")) // ¿≠∫Æ æ∆∑ø∫Æ ø°∞‘ ¥Í¿∏∏È
        {
            if (_rigid.velocity.y >= 0)
            {
                _rigid.velocity = new Vector3(_rigid.velocity.x, -_rigid.velocity.y);
            }
        }
        if (collision.gameObject.CompareTag("MapDown")) // ¿≠∫Æ æ∆∑ø∫Æ ø°∞‘ ¥Í¿∏∏È
        {
            if (_rigid.velocity.y <= 0)
            {
                _rigid.velocity = new Vector3(_rigid.velocity.x, -_rigid.velocity.y);
            }
        }
        if(collision.gameObject.tag == "A")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "B")
        {
            Destroy(collision.gameObject);
        }
    }
}
