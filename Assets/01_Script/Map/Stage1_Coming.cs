using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Coming : MonoBehaviour
{
    [SerializeField] bool isLeft = false;
    void Update()
    {
        if(isLeft == true)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 0.1f;
            transform.localEulerAngles += new Vector3(0, 0, 200);
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 0.1f;
            transform.localEulerAngles += new Vector3(0, 0, -200);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("A"))
        {
            Debug.Log("しけしけし222");
            if (collision.gameObject.GetComponent<PlayerInterrabter>().PlayerInfin == false)
            {
                GetComponent<AudioSource>().Play();
                Destroy(collision.gameObject);
            }
            else
            {
                transform.position += new Vector3(0, 1000, 0);
            }
        }
        if (collision.gameObject.CompareTag("B"))
        {
            if (collision.gameObject.GetComponent<PlayerInterrabter>().PlayerInfin == false)
            {
                GetComponent<AudioSource>().Play();
                Destroy(collision.gameObject);
            }
            else
            {
                transform.position += new Vector3(0, 1000, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("A"))
        {
            Debug.Log("しけしけし222");
            if (collision.gameObject.GetComponent<PlayerInterrabter>().PlayerInfin == false)
                Destroy(collision.gameObject);
            else
            {
                Destroy(this);
            }
        }
        if (collision.gameObject.CompareTag("B"))
        {
            if (collision.gameObject.GetComponent<PlayerInterrabter>().PlayerInfin == false)
                Destroy(collision.gameObject);
            else
            {
                Destroy(this);
            }
        }
    }
}
