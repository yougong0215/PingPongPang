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
    float Origin_size = 0.4f;
    Vector2 Origin_angle = Vector2.zero;
    float Origin_Alter = 1;



    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 100) % 2 == 0)
        {
            Origin_angle = new Vector2(1, Random.Range(-1f, 1f));
        }
        else
        {
            Origin_angle = new Vector2(-1, Random.Range(-1f, 1f));
        }
        Origin_angle.Normalize();
        _rigid.velocity = Origin_angle * Origin_speed;

    }

    private void Update()
    {
        Debug.Log(_rigid.velocity.y);
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

    public void SizeSetting()
    {
        transform.localScale *= Origin_size;

    }

    public void LastSet(int Dir, float angler)
    {


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

        

        _rigid.velocity = Origin_speed * Angle_Changer * Origin_Alter;

        Origin_angle = Angle_Changer;
    }
    public void AlterSetting()
    {
        if (Origin_Alter > 0.25f)
        {
            Origin_Alter *= 0.5f;

            Ball b = Instantiate(this);

            b.transform.position = transform.position;
            b.transform.localScale *= 0.5f;

            b.Setting(BallEnum.Angle, 1, 1, Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            
        }
    }
    public void SpeedSetting(float speed)
    {
        this.Origin_speed *= speed;
    }
    public void sizeSetting(float size)
    {
        this.Origin_size *= size;
    }
    public void AngleSetting(float a, float b)
    {
        Origin_angle = new Vector2(a, b).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map")) // À­º® ¾Æ·¿º® ¿¡°Ô ´êÀ¸¸é
        {

            _rigid.velocity = new Vector2(_rigid.velocity.x, -1 * _rigid.velocity.y); // ¹æÇâ ÀüÈ¯
            Origin_angle.y *= -1f;

            Debug.Log("dad" + _rigid.velocity);
        }
        if(collision.gameObject.CompareTag("GameOver"))
        {
            Debug.Log($"{collision.gameObject.name}");
        }

    }
}