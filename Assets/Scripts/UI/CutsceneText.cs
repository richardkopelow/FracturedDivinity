using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneText : MonoBehaviour
{
    public Text Label;
    public float DelayTime;
    public float FadeDelay;
    public float HiddenDelay;
    public string[] Texts;

    void Start()
    {
        StartCoroutine(RunScene());
    }

    IEnumerator RunScene()
    {
        yield return new WaitForSeconds(DelayTime);
        for (int i = 0; i < Texts.Length; i++)
        {
            Label.text = Texts[i];
            yield return Fade(1,FadeDelay);
            yield return new WaitForSeconds(DelayTime);
            yield return Fade(0, FadeDelay);
            yield return new WaitForSeconds(HiddenDelay);
        }
        PlayerPrefs.SetInt("SeenCutscene", 1);
        SceneManager.LoadScene("Temple");
    }

    IEnumerator Fade(float alphaTarget, float time)
    {
        float timeElapsed = 0;
        Color originalColor = Label.color;
        Color targetColor = originalColor;
        targetColor.a = alphaTarget;
        while (timeElapsed<time)
        {
            timeElapsed += Time.deltaTime;
            Label.color = Color.Lerp(originalColor, targetColor,timeElapsed/time);
            yield return null;
        }
        Label.color = targetColor;
    }
}
