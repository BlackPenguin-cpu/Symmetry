using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    private void Awake() => instance = this;

    public Image fadeInOut;

    void Start()
    {
        fadeInOut = GetComponent<Image>();

        fadeInOut.DOFade(1, 0);
        fadeInOut.DOFade(0, 1).SetEase(Ease.Linear).SetUpdate(true);
    }
}
