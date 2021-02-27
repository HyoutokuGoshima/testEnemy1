using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using UnityEditor;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject  EnemyPrefab;    // 生成するエネミーのプレファブ

    [SerializeField]
    private float       CreateDist = 20f; // 生成される距離

    [SerializeField]
    private float       SearchDist_NavMesh = 30f; // 生成地点から検索できるNavMeshの距離

    [SerializeField]
  //  private float       CreateTime = 3f; // 敵が生成される時間

    //[SerializeField]
    private float createAngle = 180f;


   // private float time = 0f;   // 経過時間



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;

        //if(time >= CreateTime)
        //{
        //    SummonEnemy_front();
        //    time = 0f;
        //}

        if(Input.GetKeyDown(KeyCode.C))
        {
            SummonEnemy_front();
        }
    }


    private void SummonEnemy_front()
    {

        float r = Random.Range(0f, CreateDist);
        float a = Random.Range(0f, createAngle);

        Vector3 randVec = new Vector3(
            Mathf.Cos(a),
            0f,
            Mathf.Sin(a)
            );

        // 実際に発射する角度
        var angle = Vector3.Angle(transform.forward, randVec);

        Vector3 summonPos = transform.position + new Vector3(
            r * Mathf.Cos(angle),
            0f,
            r * Mathf.Sin(angle)
            );


        // その座標から一番近いNavMeshを検索
        NavMeshHit hit;
        if(NavMesh.SamplePosition(summonPos,out hit,SearchDist_NavMesh,NavMesh.AllAreas))
        {
            summonPos = hit.position;

            // エネミープレファブからオブジェクトを生成
            GameObject obj = Instantiate(EnemyPrefab, summonPos, Quaternion.identity);

            // 自身の子オブジェクトとして登録
            obj.transform.parent = transform;
        }
        else
        {
            Debug.Log("召喚失敗：NavMeshが範囲内になかった");
        }
    }



    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -createAngle, 0f) * transform.forward, createAngle * 2f, 2f);
    }

}
