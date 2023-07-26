using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMap : MonoBehaviour
{
    GameObject MapGenerator;

    private void Start()
    {
        MapGenerator = GameObject.Find("MapGenerator");
    }
    private void OnTriggerExit(Collider collider)
    {
        MapGenerator.GetComponent<MapGenerator>().OnTriggerMap();
    }
}
