using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Fade : MonoBehaviour
{
    public Player player;
    public Scene targetScene;
    public float fadeDuration = 2.0f;

    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    void Update ()
    {
        if (Input.GetButtonDown("Jump") && player.scanObject != null)
        {

            if (player.scanObject.name == "gate")
            {
                StartCoroutine(FadeAndLoadScene(targetScene));
            }

        }
    }
    IEnumerator FadeAndLoadScene(Scene scene)
    {
        // ���̵� �ƿ�
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // ������ �������ϰ� ����

        // �� ��ȯ
        SceneManager.LoadScene("ax");

        // ���̵� ��
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // ������ �����ϰ� ����
    }
}
