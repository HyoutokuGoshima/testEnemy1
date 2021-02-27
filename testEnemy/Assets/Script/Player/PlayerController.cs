using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 velocity;       // 移動方法

    public float    moveSpeed = 5.0f;   // 移動速度
    public float    jumpPower = 5f;     // ジャンプする力  

    Rigidbody rb;


    private bool jumpFig = false;       // ジャンプ中かどうか
    private bool jumpButton = false;    // ジャンプボタンを押しているか


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerKey();

        UpdateJump();   // ジャンプ処理

        // カメラの方向から、x-z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * velocity.z + Camera.main.transform.right * velocity.x;

        // スピードの値分移動方向へ移動
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y + velocity.y, 0);

        // キャラクターの向きを進行方向に
        if(moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        
    }


    //=================================
    //      プレイヤーの操作
    //=================================
    private void PlayerKey()
    {
        velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) velocity.z += 1;
        if (Input.GetKey(KeyCode.S)) velocity.z -= 1;
        if (Input.GetKey(KeyCode.D)) velocity.x += 1;
        if (Input.GetKey(KeyCode.A)) velocity.x -= 1;

        if(Input.GetButtonDown("Jump"))
        {
            if(!jumpButton)
            {
                jumpFig = true;
                jumpButton = true;
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpButton = false;
        }
    }


    //ジャンプの処理
    private void UpdateJump()
    {
        if(jumpFig)
        {
            velocity.y += jumpPower;
            jumpFig = false;
        }
    }
}
