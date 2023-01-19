using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    Image fadeInOut;
    float speed = 0.2f;

    void Start() => fadeInOut = GetComponent<Image>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void FadeIn() => fadeInOut.DOFade(0.5f, speed).SetEase(Ease.Linear).SetUpdate(true);

    public void FadeOut() => fadeInOut.DOFade(0, speed).SetEase(Ease.Linear).SetUpdate(true);
}
