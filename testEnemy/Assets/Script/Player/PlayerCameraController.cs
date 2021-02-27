using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    
    public GameObject   Player;             // 追従するプレイヤー
    public float        rotateSpeed = 3.0f; // 回転させるスピード

    Vector3 targetPos;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの移動量分、自身も移動
        transform.position += Player.transform.position - targetPos;
        targetPos = Player.transform.position;

        // 回転させる角度
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotateSpeed,
            Input.GetAxis("Mouse Y") * rotateSpeed,
            0
            );

        // プレイヤー位置情報
        Vector3 playerPos = Player.transform.position;

        // カメラを回転させる
        transform.RotateAround(playerPos, Vector3.up, angle.x);
        transform.RotateAround(playerPos, transform.right, -angle.y);
    }
}
