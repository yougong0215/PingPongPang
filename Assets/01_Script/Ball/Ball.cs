using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallEnum
{
    None,
    Speed,
    Angle,
    Size,
    Alter
}

public class Ball : MonoBehaviour
{
    Rigidbody2D _rigid;

    [SerializeField] float Origin_speed = 3;
    [SerializeField] float Origin_size = 0.4f;
    [SerializeField] Vector2 Origin_angle = Vector2.zero;
    [SerializeField] float Origin_Alter = 1;

    public float mapSpeed = 1;

    public PlayerInterrabter pl;

    public void Reset()
    {
        Origin_speed = 5;
        Origin_size = 0.4f;
    }

    private void OnEnable()
    {

        StartCoroutine(GameManager.Instance.TeamSetting(gameObject));

        _rigid = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 101) % 2 == 0)
        {
            Origin_angle = new Vector2(1, Random.Range(-1f, 1f));
        }
        else
        {
            Origin_angle = new Vector2(-1, Random.Range(-1f, 1f));
        }
        if(Origin_Alter == 1)
            StartCoroutine(StartBox());
        Origin_angle.Normalize();


    }

    IEnumerator StartBox()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Update()
    {

        _rigid.velocity = Origin_speed * Origin_angle * MapList.MapSpeed * mapSpeed * Origin_Alter;



    }


    public void Setting(BallEnum a = BallEnum.None, float speed = 1, float size = 1, float angle1 = 0, float angle2 = 0)
    {
        switch (a)
        {
            case BallEnum.None:
                // ÇÊ¾øµë
                break;
            case BallEnum.Speed:
                SpeedSetting(speed);
                break;
            case BallEnum.Angle:
                AngleSetting(angle1, angle2);
                break;
            case BallEnum.Size:
                sizeSetting(size);
                break;
            case BallEnum.Alter:
                AlterSetting();
                break;
        }
    }




    public void LastSet(int Dir, float angler, PlayerInterrabter pl)
    {
        this.pl = pl;

        Vector2 Angle_Changer = Origin_angle;

        //loat t = Random.Range(-angler, angler);


        if (Dir == 1 && Angle_Changer.x < 0)
        {
            Angle_Changer.x *= -1;
        }
        else
        {
            Angle_Changer.x *= Dir;
        }

        //Angle_Changer.x -= angler * 5;


        Angle_Changer.Normalize();
        Debug.Log($"{Dir} * {Origin_speed} * {Angle_Changer.y} * {Origin_Alter}");
        if (pl.toza == true)
        {
            Origin_speed *= pl.TOZAZA;
        }

        transform.localScale = new Vector3(Origin_size, Origin_size, 1) * Origin_Alter;

        Origin_angle = Angle_Changer;
    }

    public void AlterSetting()
    {
        Debug.Log($"{gameObject.name} : {Origin_Alter}");
        if (Origin_Alter > 0.6f)
        {
            Origin_Alter -= 0.2f;
            Ball b = Instantiate(this);

            b.Origin_angle *= -1;

            b.Origin_Alter = Origin_Alter;

            b.transform.position = transform.position;


            if (transform.position.y > 0)
                b.Setting(BallEnum.Angle, 1, 1, -1, Random.Range(-1f, 1f));
            else
            {
                b.Setting(BallEnum.Angle, 1, 1, 1, Random.Range(-1f, 1f));
            }
            b.transform.parent = this.transform.parent;

        }
    }



    public void SpeedSetting(float speed)
    {
        this.Origin_speed *= speed;
    }
    public void sizeSetting(float size)
    {
        this.Origin_size *= size;
        transform.localScale = new Vector3(Origin_size, Origin_size, 1) * Origin_Alter;
    }
    public void AngleSetting(float a, float b)
    {
        Origin_angle = new Vector2(a, b).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {

            GameManager.Instance.PlayerRound(collision.gameObject.GetComponent<BallCheck>().Player());

            foreach (GameObject a in GameObject.FindGameObjectsWithTag("GameOver"))
            {
                a.SetActive(false);
            }

        }

        if (collision.gameObject.CompareTag("MapUp")) // À­º® ¾Æ·¿º® ¿¡°Ô ´êÀ¸¸é
        {
            if (_rigid.velocity.y >= 0)
            {
                Origin_angle.y *= -1f;
                Origin_angle.y += Random.Range(-0.2f, 0.2f);

                Origin_angle.Normalize();
            }
        }
        if (collision.gameObject.CompareTag("MapDown")) // À­º® ¾Æ·¿º® ¿¡°Ô ´êÀ¸¸é
        {
            if (_rigid.velocity.y <= 0)
            {
                Origin_angle.y *= -1f;
                Origin_angle.y += Random.Range(-0.2f, 0.2f);

                Origin_angle.Normalize();
            }
        }


    }

}