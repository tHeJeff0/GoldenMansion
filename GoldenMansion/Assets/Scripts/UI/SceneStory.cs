using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneStory : MonoBehaviour
{
    float fadeDuration = 1f; // 渐变时间
    float randomTime = 10.0f;
    float timer = 0.0f;
    private Material material;

    [SerializeField] GameObject groupOne;
    [SerializeField] GameObject groupTwo;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            originalColor = material.color;
        }


    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            StartCoroutine((ShowSceneStory()));
            timer = 0;
        }
        
    }

    public void FadeIn()
    {
        if (material != null)
        {
            material.DOFade(1.0f, fadeDuration);
        }
    }

    public void FadeOut()
    {
        if (material != null)
        {
            material.DOFade(0f, fadeDuration); // 渐出效果，透明度到 0
        }
    }

    IEnumerator ShowSceneStory()
    {
        int randomNum = Random.Range(1, 3);
        if (randomNum == 1)
        {
            GameObject storyBGPic = groupOne.transform.Find("StoryBGPic").gameObject;
            GameObject wordPic = groupOne.transform.Find("WordGroup").gameObject;
            GameObject text = groupOne.transform.Find("StoryText").gameObject;
            yield return storyBGPic.transform.DOScale(1.0f, 1.0f).WaitForCompletion();
            wordPic.GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f).WaitForCompletion();
            text.GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f).WaitForCompletion();
            yield return new WaitForSecondsRealtime(3.0f);
            yield return wordPic.GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f).WaitForCompletion();
            yield return text.GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f).WaitForCompletion();
            yield return storyBGPic.transform.DOScale(0.0f, 1.0f).WaitForCompletion();
        }
        else
        {
            GameObject storyBGPic = groupTwo.transform.Find("StoryBGPic").gameObject;
            GameObject wordPic = groupTwo.transform.Find("WordGroup").gameObject;
            GameObject text = groupTwo.transform.Find("StoryText").gameObject;
            yield return storyBGPic.transform.DOScale(1.0f, 1.0f).WaitForCompletion();
            wordPic.GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f).WaitForCompletion();
            text.GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f).WaitForCompletion();
            yield return new WaitForSecondsRealtime(3.0f);
            yield return wordPic.GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f).WaitForCompletion();
            yield return text.GetComponent<CanvasGroup>().DOFade(0.0f, 1.0f).WaitForCompletion();
            yield return storyBGPic.transform.DOScale(0.0f, 1.0f).WaitForCompletion();
        }
    }


}
