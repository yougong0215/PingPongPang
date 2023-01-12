using UnityEngine;

public class WaterMelon : MonoBehaviour
{
    [SerializeField] Transform A;
    [SerializeField] Transform B;

    int A_Score = 0;
    int B_Score = 0;
    public void HitGimik(Transform pl)
    {

        try
        {

            if (pl.transform.position.x > 0 && B_Score < 5)
            {
                B.position += new Vector3(0, 1.8f);
                B_Score++;
            }
            else if ( A_Score < 5)
            {
                A.position += new Vector3(0, 1.8f);
                A_Score++;
            }

            if (A_Score == 5)
            {
                GameObject.Find("UserB").SetActive(false);
                if (GameObject.Find("UserB(Clone)"))
                {
                    GameObject.Find("UserB(Clone)").SetActive(false);
                }
            }
            if (B_Score == 5)
            {
                GameObject.Find("UserA").SetActive(false);
                if (GameObject.Find("UserA(Clone)"))
                {
                    GameObject.Find("UserA(Clone)").SetActive(false);
                }
            }
        }
        catch
        {

        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            GetComponent<AudioSource>().Play();
            HitGimik(collision.transform);
        }
    }
}
