using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEnum
{
    A,
    B
}

public enum PlayerAbilityEnum
{
    None,
    Size,
    SizeOne,
    SizeTwe,
    Speed,
    Angler
}

public class PlayerInterrabter : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] public PlayerEnum _playerEnum;

    [Header("KeySetting")]
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode Down;

    public float Angler = 4f;


    public float Speed = 1;

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.3f, 1.4f, 1);
        GameManager.Instance.PlayerSetting(_playerEnum, this);
        if(_playerEnum == PlayerEnum.A)
            GetComponent<SpriteRenderer>().sprite = GameManager.Instance.A;
        else
            GetComponent<SpriteRenderer>().sprite = GameManager.Instance.B;
        
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Up))
            transform.position += new Vector3(0, 1) * Speed * Time.deltaTime;
        if (Input.GetKey(Down))
            transform.position += new Vector3(0, -1) * Speed * Time.deltaTime;


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.3f, 4.3f), 0);
    }

    public void SpeedSetting(float speed)
    {
        this.Speed = speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // 공에 닿음녀
        {

            SetAbility(collision.gameObject.GetComponent<Ball>());
            Debug.Log("닿음");
        }
    }

    void SetAbility(Ball b)
    {
        GameManager.Instance.SetAbility(_playerEnum, b, this);
    }
}
