using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public static int tankSize = 40;

    static int numFish = 60;
    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;


    public static Vector3 gPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < numFish; i++)
        {
            Vector3 pos = GetRandTankSize();

            allFish[i] = (GameObject)Instantiate(fishPrefab, transform.position + pos, Quaternion.identity);
        }

        // ゴール地点を設定
        goalPos = transform.position + GetRandTankSize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,10000) < 50)
        {
            goalPos = transform.position + GetRandTankSize();
        }

        gPos = transform.position;
    }


    private Vector3 GetRandTankSize()
    {
        return new Vector3(Random.Range(-tankSize, tankSize),
                           Random.Range(-tankSize, tankSize),
                           Random.Range(-tankSize, tankSize));
    }

}
