using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//Скрипт который отвечает за переход между сценами и затемнение экрана
public class Buttons : MonoBehaviour
{
    [SerializeField] private Image fade;

    private Animation fader;

    private void Awake()
    {
        fader = fade.GetComponent<Animation>();
    }

    public void LoadScene(int sceneIndex)
    {
        fader.Play("FadeOut");
        StartCoroutine(LoadingSceneDelayed(sceneIndex));
    }

    private IEnumerator LoadingSceneDelayed(int sceneIndex)
    {
        yield return new WaitUntil(() => fade.color.a == 1);
        SceneManager.LoadScene(sceneIndex);
    }
}
