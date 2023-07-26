using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject effectPrefab;

    private Queue<GameObject> objectQueue;
    [HideInInspector] public Queue<GameObject> effectQueue;
    private Vector3 currentPosition;
    private Vector3 effectPosition;

    public TextMeshProUGUI scoreText; //����â
    public TextMeshProUGUI comboText; //�޺�â

    int score = 0;
    [HideInInspector] public int combo = 0;

    private void Start()
    {
        SetScoreText(); //���� ǥ��        
        SetComboText(); //�޺� ǥ��

        objectQueue = new Queue<GameObject>();
        effectQueue = new Queue<GameObject>();
        currentPosition = transform.position;
        effectPosition = transform.position + Vector3.up * 1.5f;

        for (int i = 0; i < 10; i++)
        {
            GenerateObjects();
        }

        //GenerateObjects();
        GetFirstObject();
    }

    private void Update()
    {
        

    }

    public void GenerateObjects()
    {
        //ȭ��ǥ ����
        GameObject objectPrefab = GetRandomObjectPrefab();
        GameObject objectInstance = Instantiate(objectPrefab, currentPosition, Quaternion.identity);

        currentPosition += new Vector3(0f, 0f, 5f); // �� ������Ʈ ������ ���� ���� ����
        objectQueue.Enqueue(objectInstance);

        //����Ʈ ����
        GameObject effectInstanceObj = Instantiate(effectPrefab, effectPosition, Quaternion.identity);
        ParticleSystem effectInstance = effectInstanceObj.GetComponent<ParticleSystem>();

        effectPosition += new Vector3(0f, 0f, 5f);
        effectQueue.Enqueue(effectInstanceObj);


        objectInstance.GetComponent<DestroyObject>().effect = effectInstance;
    }

    private GameObject GetRandomObjectPrefab()
    {
        int randomIndex = Random.Range(0, objectPrefabs.Length);
        return objectPrefabs[randomIndex];
    }

    public void GetFirstObject()
    {
        GameObject firstObject = objectQueue.Dequeue();
        DestroyObject destroyObject = firstObject.GetComponent<DestroyObject>();
        destroyObject.enabled = true;
        destroyObject.SetControlledByPlayer(true);
    }

    public void GetScore()
    {
        if (combo >= 100) { score += 100; }
        else if (combo >= 50) { score += 50; }
        else if(combo >= 30) { score += 30; }
        else if(combo >= 10) { score += 20; }
        else if(combo >= 0) { score += 10; }

        SetScoreText();
    }

    public void SetScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }


    public void GetCombo()
    {
        combo += 1;
        SetComboText();
    }

    public void SetComboText()
    {
        if (combo >= 100)
        {
            comboText.text = combo.ToString() + "Combo!!!!";
        }

        else if (combo >= 50)
        {
            comboText.text = combo.ToString() + "Combo!!!";
        }

        else if (combo >= 30)
        {
            comboText.text = combo.ToString() + "Combo!!";
        }

        else if (combo >= 10)
        {
            comboText.text = combo.ToString() + "Combo!";
        }

        else if (combo > 0)
        {
            comboText.text = combo.ToString() + "Combo";
        }

        if (combo == 0)
        {
            comboText.text = null;
        }
        
    }
}
