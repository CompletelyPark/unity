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
        // 페이드 아웃
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // 완전히 불투명하게 설정

        // 씬 전환
        SceneManager.LoadScene("ax");

        // 페이드 인
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // 완전히 투명하게 설정
    }
}
