using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    // 오브젝트가 사라질 방향키를 각 오브젝트마다 지정해줍니다.
    public KeyCode myKey;
    GameObject gameManager;
    public ParticleSystem effect;

    private bool isControlledByPlayer = false; // 해당 오브젝트가 플레이어에 의해 조종되는지 여부를 확인하는 변수

    int flag = 0;

    // 플레이어가 해당 오브젝트를 조종할 수 있도록 설정하는 함수
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
        // 플레이어에 의해 조종되는 오브젝트만 방향키 입력을 받아서 사라지도록 합니다.
        if (isControlledByPlayer && flag == 0)
        {
            if (Input.GetKeyDown(myKey))
            {
                Destroy(gameObject);
                gameManager.GetComponent<GameManager>().GetFirstObject();   // 맨 앞 화살표 호출
                gameManager.GetComponent<GameManager>().GenerateObjects();  // 맨 뒤에 새오운 화살표 생성
                gameManager.GetComponent<GameManager>().GetCombo();         // 콤보 획득
                gameManager.GetComponent<GameManager>().GetScore();         // 점수획득
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
