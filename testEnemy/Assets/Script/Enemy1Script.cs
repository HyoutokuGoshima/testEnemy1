using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using Enemy1State;  // State関係

public class Enemy1Script : MonoBehaviour
{

    // State //--------------------------
    private string _beforeStateName;    // 変更前のステート名

    public StateProcessor StateProcessor = new StateProcessor();

    public Enemy1StateWait      StateWait       = new Enemy1StateWait();
    public Enemy1StateRandMove  StateRandMove   = new Enemy1StateRandMove();
    public Enemy1StateChace     StateChace      = new Enemy1StateChace();
    public Enemy1StateAttack    StateAttack     = new Enemy1StateAttack();
    //-----------------------------------


    private CharacterController enemyController;
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    private bool arrived;           // 到着フラグ
    private float walkSpeed = 1.0f; // 歩行速度

    private Vector3 velocity;       // 速度
    private Vector3 direction;      // 移動方向

    public Vector3 destination;
    public float randomMoveDistance = 2.0f; // ランダムに移動する際の距離

    public GameObject targetPlayer;


    // ステート変数 //
    private float chaceTime = 2f;   // 追跡時間
    private float attackTime = 2f;  // 攻撃時間
    private float time = 0f;        // 経過時間




    //===========================
    //
    //      関数
    //
    //===========================


    // Start is called before the first frame update
    void Start()
    {
        // 領域内にそれぞれスクリプトを格納する
        enemyController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // State初期化
        StateProcessor.State = StateWait;

        StateWait.execDelegate = Wait;
        StateRandMove.execDelegate = RandMove;
        StateChace.execDelegate = Chace;
        StateAttack.execDelegate = Attack;


        arrived = false;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {


        if (StateProcessor.State != null)
        {
            //ステートの値が変更されたら実行処理を行う
            if (StateProcessor.State.getStateName() != _beforeStateName)
            {
                //Debug.Log(" Now State:" + StateProcessor.State.getStateName());

                _beforeStateName = StateProcessor.State.getStateName();
                time = 0f;
            }

            StateProcessor.Execute();
        }


        // 重力を加味すると面倒くさい！
        //velocity.y += Physics.gravity.y * Time.deltaTime;
        //enemyController.Move(velocity * Time.deltaTime);


    }


    public void Wait()
    {

        if (navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            destination = RandomPointOnNavMesh();
            navMeshAgent.SetDestination(destination);
            navMeshAgent.isStopped = false; // 一応

            //Debug.Log("座標を設定 State:" + StateProcessor.State.getStateName());
        }

        // 目的地を設定して移動処理に遷移
        StateProcessor.State = StateRandMove;
    }

    public void RandMove()
    {

        if (navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            if (Vector3.Distance(transform.position, destination) < 0.2f)
            {
                navMeshAgent.isStopped = true;
            }

            // 目的地について停止していたら
            if (navMeshAgent.isStopped)
            {

                StateProcessor.State = StateWait;
            }
        }
    }

    public void Chace()
    {
        time += Time.deltaTime;

        if(time >= chaceTime)
        {
            StateProcessor.State = StateWait;
        }

        // プレイヤーの座標を取得し続ける
        if (navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            destination = targetPlayer.transform.position;
            navMeshAgent.SetDestination(destination);
        }

        // プレイヤーにある程度近づいたら
        if (Vector3.Distance(transform.position, destination) < 2.0f)
        {
            // 攻撃体制に入る
            StateProcessor.State = StateAttack;
        }
    }

    public void Attack()
    {
        if(time <= 0f)
        {
            //Debug.Log("攻撃開始！！");
        }

        time += Time.deltaTime;

        if(time >= attackTime)
        {
            // 攻撃し終わったら待機モードへ
            StateProcessor.State = StateWait;
        }

    }



    // NavMesh内のランダムな座標を取得
    private Vector3 RandomPointOnNavMesh()
    {
        // ランダムに設定された座標
        Vector3 randomPos = transform.position;

        // ランダム座標
        float randomPosX = Random.Range(-randomMoveDistance, randomMoveDistance);
        float randomPosY = Random.Range(0, randomMoveDistance);
        float randomPosZ = Random.Range(-randomMoveDistance, randomMoveDistance);

        randomPos += new Vector3(randomPosX, randomPosY, randomPosZ);

        // ランダムな座標から一番近いNavMesh内の点を検索
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPos,out hit,20.0f,NavMesh.AllAreas))
        {
            Debug.Log("NavMesh発見！：" + hit.position);
        }
        else
        {
            Debug.Log("NavMesh未発見…" + hit.position);

            // 原点に再設定
            //hit.position = new Vector3(0f, 0f, 0f);
        }

        return hit.position;
    }


    // プレイヤー発見時に呼ばれる
    public void Discovery_Player(GameObject player)
    {
        targetPlayer = player;
        StateProcessor.State = StateChace;
    }
}




// NavMesh上の座標を取得
//int layerMask = (1 << LayerMask.NameToLayer("Stage"));    // Stageレイヤー以外無視
//var hits = Physics.RaycastAll(
//    (transform.position + new Vector3(randomPosX, randomPosY, randomPosZ)),
//    Vector3.down,
//    1000,
//    layerMask
//    ); 
//        // NavMeshに当たったか判断
//        foreach(var hit in hits)
//        {
//            if(hit.collider.CompareTag("Stage"))
//            {
//                Debug.Log("IN!!!" + hit.point);
//                randomPos = hit.point;
//                break;
//            }
//        }
