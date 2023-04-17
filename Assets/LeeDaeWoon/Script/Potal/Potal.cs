using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Potal : MonoBehaviour
{
    public static Potal Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject upgrade;

    bool isCollisionCheck = false;
    const float fadeSpeed = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Dimension:
                ScreenVector(new Vector3(-3, -4.5f, 0));
                FClick();
                break;
        }
    }

    void ScreenVector(Vector3 vec) => upgrade.transform.localPosition = Camera.main.WorldToScreenPoint(transform.localPosition + vec);

    void FClick()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck)
        {
            SoundManager.instance.PlaySoundClip("SFX_Potal", SoundType.SFX);

            Fade.instance.fadeInOut.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                DOTween.KillAll();
                SceneManager.LoadScene("Dimension");
            });
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            switch (CurrentScene.instance.eScene)
            {
                case EScene.Main:
                    Fade.instance.fadeInOut.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Potal", SoundType.SFX);

                        DOTween.KillAll();
                        SceneManager.LoadScene("Test");
                    });
                    break;

                case EScene.Dimension:
                    isCollisionCheck = true;
                    upgrade.transform.GetChild(0).GetComponent<Image>().DOFade(1, fadeSpeed).SetEase(Ease.Linear);
                    upgrade.transform.GetChild(1).GetComponent<Text>().DOFade(1, fadeSpeed).SetEase(Ease.Linear);
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            switch (CurrentScene.instance.eScene)
            {
                case EScene.Dimension:
                    isCollisionCheck = false;
                    upgrade.transform.GetChild(0).GetComponent<Image>().DOFade(0, fadeSpeed).SetEase(Ease.Linear);
                    upgrade.transform.GetChild(1).GetComponent<Text>().DOFade(0, fadeSpeed).SetEase(Ease.Linear);
                    break;
            }
        }
    }
}
