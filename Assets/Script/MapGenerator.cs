using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapPrefab;
    private Queue<GameObject> mapQueue;
    private Vector3 currentPosition;

    private void Start()
    {
        mapQueue = new Queue<GameObject>();
        currentPosition = transform.position;

        GenerateMap();
        GenerateMap();
    }


    public void GenerateMap()
    {
        GameObject newInstance = Instantiate(mapPrefab, currentPosition, Quaternion.Euler(0, 180, 0));
        currentPosition += new Vector3(0f, 0f, 96f); // 각 오브젝트 사이의 간격 조정 가능
        mapQueue.Enqueue(newInstance);

    }

    public void OnTriggerMap()
    {
        GameObject tempMap = mapQueue.Dequeue();
        Destroy(tempMap);
        GenerateMap();
    }
}
