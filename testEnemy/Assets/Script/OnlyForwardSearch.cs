using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OnlyForwardSearch : MonoBehaviour
{
    [SerializeField]
    private Enemy1Script m_enemy1Script;     // 情報を伝えるスクリプト
    [SerializeField]
    private SphereCollider  searchArea;         // 視野
    [SerializeField]
    private float           searchAngle = 130f; // エネミーの視角


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // 主人公の方向
            var playerDirection = other.transform.position - transform.position;
            // エネミーの前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);

            // サーチする角度内なら発見
            if(angle <= searchAngle)
            {
                Debug.Log("Player発見！！");
                m_enemy1Script.Discovery_Player(other.gameObject);
            }
        }
    }


    // サーチする角表示
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }


#if UNIT_EDITOR


#endif

}
