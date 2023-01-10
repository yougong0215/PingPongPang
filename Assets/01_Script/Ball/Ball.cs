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

    public void Reset()
    {
        Origin_speed = 5;
        Origin_size = 0.4f;
    }

    private void OnEnable()
    {

        StartCoroutine(GameManager.Instance.TeamSetting(gameObject));

        Origin_Alter = 1;
        _rigid = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 101) % 2 == 0)
        {
            Origin_angle = new Vector2(1, Random.Range(-1f, 1f));
        }
        else
        {
            Origin_angle = new Vector2(-1, Random.Range(-1f, 1f));
        }

        Origin_angle.Normalize();
        _rigid.velocity = Origin_angle * Origin_speed * 1;


    }

    private void Update()
    {
        if (Mathf.Abs(_rigid.velocity.x) < 1f || _rigid.velocity.x + _rigid.velocity.y < Origin_speed * Origin_Alter - 1 && Mathf.Abs(_rigid.velocity.y) < 0.2f)
        {
            if (Origin_Alter < 1)
            {
                _rigid.velocity = Origin_speed * Origin_angle * Random.Range(Origin_Alter - 0.5f, Origin_Alter + 0.2f) * MapList.MapSpeed; ;
            }
            else
            {
                _rigid.velocity = Origin_speed * Origin_angle * MapList.MapSpeed;
            }
        }

    }


    public void Setting(BallEnum a = BallEnum.None, float speed = 1, float size = 1, float angle1 = 0, float angle2 = 0)
    {
        switch (a)
        {
            case BallEnum.None:
                // �ʾ���
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
        if (pl.MoonGwa == false)
        {
            if (Origin_Alter < 1)
            {
                _rigid.velocity = Origin_speed * Angle_Changer * Random.Range(Origin_Alter - 0.5f, Origin_Alter + 0.2f) * MapList.MapSpeed; ;
            }
            else
            {
                _rigid.velocity = Origin_speed * Angle_Changer * MapList.MapSpeed;
            }

        }
        else
        {
            if (transform.position.x > 0)
            {
                _rigid.velocity = Origin_speed * new Vector3(Random.Range(-2f, -0.3f), Random.Range(-1f, 1f), 0).normalized * MapList.MapSpeed;

            }
            else
                _rigid.velocity = Origin_speed * new Vector3(Random.Range(0.3f, 2f), Random.Range(-1f, 1f), 0).normalized * MapList.MapSpeed;

        }


        transform.localScale = new Vector3(Origin_size, Origin_size, 1)* Origin_Alter;

        Origin_angle = Angle_Changer;
    }

    public void AlterSetting()
    {
        Debug.Log($"{gameObject.name} : {Origin_Alter}");
        if (Origin_Alter > 0.6f)
        {
            Origin_Alter -= 0.2f;
            Ball b = Instantiate(this);

            b._rigid.velocity *= -1; 

            b.Origin_Alter = Origin_Alter;
            
            b.transform.position = transform.position;


            if(transform.position.y > 0)
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
        
        }


        if (collision.gameObject.CompareTag("MapUp")) // ���� �Ʒ��� ���� ������
        {
            if (_rigid.velocity.y >= 0)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, -1 * _rigid.velocity.y - 1); // ���� ��ȯ
                Origin_angle.y *= -1f;
            }
        }
        if (collision.gameObject.CompareTag("MapDown")) // ���� �Ʒ��� ���� ������
        {
            if (_rigid.velocity.y <= 0)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, -1 * _rigid.velocity.y + 1); // ���� ��ȯ
                Origin_angle.y *= -1f;
            }
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
    }
}