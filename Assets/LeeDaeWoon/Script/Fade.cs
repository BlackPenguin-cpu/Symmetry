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
    float speed = 0.2f;

    void Start() => fadeInOut = GetComponent<Image>();
}
