using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public enum State
    {
        Walk,
        Wait,
        Chace
    };

    private State state;                // キャラクターの状態
    private Transform targetTransform;  // 目標

    private Vector3 destination;        // 目的地

    // 移動関係 //

    private float walkSpeed = 1.0f; // 歩くスピード
    private Vector3 velocity;       // 速度
    private Vector3 direction;      // 移動方向

    private bool arrived;           // 到着フラグ
                                    // SetPositionスクリプト
    private float waitTime = 5f;    // 待ち時間
    private float elapsedTime;      // 経過時間




    public void SetState(State tempState,Transform targetObj = null)
    {
        switch(state)
        {
            case State.Walk:
                arrived = false;
                elapsedTime = 0f;
                break;

            case State.Chace:
                arrived = false;
                targetTransform = targetObj;
                break;

            case State.Wait:
                arrived = true;
                velocity = Vector3.zero;
                break;
        }

        if(arrived)
        {

        }
    }

    public State GetState() { return state; }　// 状態を取得


    // Start is called before the first frame update
    void Start()
    {
        SetState(State.Walk);   // 状態設定
    }

    // Update is called once per frame
    void Update()
    {

        switch(state)
        {
            case State.Walk:
                Walk();
                break;

            case State.Chace:
                Chace();
                break;

            case State.Wait:
                Wait();
                break;
        }
    }

    
    private void Walk()
    {

    }

    private void Chace()
    {

    }

    private void Wait()
    {

    }
}
