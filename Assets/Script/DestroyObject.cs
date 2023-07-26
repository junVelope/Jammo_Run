using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    // ������Ʈ�� ����� ����Ű�� �� ������Ʈ���� �������ݴϴ�.
    public KeyCode myKey;
    GameObject gameManager;
    public ParticleSystem effect;

    private bool isControlledByPlayer = false; // �ش� ������Ʈ�� �÷��̾ ���� �����Ǵ��� ���θ� Ȯ���ϴ� ����

    int flag = 0;

    // �÷��̾ �ش� ������Ʈ�� ������ �� �ֵ��� �����ϴ� �Լ�
    public void SetControlledByPlayer(bool value)
    {
        isControlledByPlayer = value;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        // �÷��̾ ���� �����Ǵ� ������Ʈ�� ����Ű �Է��� �޾Ƽ� ��������� �մϴ�.
        if (isControlledByPlayer && flag == 0)
        {
            if (Input.GetKeyDown(myKey))
            {
                Destroy(gameObject);
                gameManager.GetComponent<GameManager>().GetFirstObject();   // �� �� ȭ��ǥ ȣ��
                gameManager.GetComponent<GameManager>().GenerateObjects();  // �� �ڿ� ������ ȭ��ǥ ����
                gameManager.GetComponent<GameManager>().GetCombo();         // �޺� ȹ��
                gameManager.GetComponent<GameManager>().GetScore();         // ����ȹ��
                EffectPlay();
            }

            else if (!(Input.GetKeyDown(myKey)) &&
                    (Input.GetKeyDown(KeyCode.UpArrow) ||
                     Input.GetKeyDown(KeyCode.DownArrow) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                
                gameManager.GetComponent<GameManager>().combo = 0;
                gameManager.GetComponent<GameManager>().SetComboText();
                StartCoroutine(InputDelay());
            }
        }
    }

    private IEnumerator InputDelay()
    {
        flag = 1;
        yield return new WaitForSeconds(0.1f);
        flag = 0;

    }


    private void EffectPlay()
    {
        GameObject firstEffect = gameManager.GetComponent<GameManager>().effectQueue.Dequeue();
        AudioSource audioSource = firstEffect.GetComponent<AudioSource>();
        audioSource.Play();
        effect.transform.position = firstEffect.transform.position;
        effect.transform.rotation = Quaternion.Euler(0, 0, 0);
        effect.Play();
    }

    public void OnTriggerEnter(Collider collision)
    {
        SceneManager.LoadScene("GameOver");
    }

}
