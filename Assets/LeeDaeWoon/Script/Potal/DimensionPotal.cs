using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DimensionPotal : MonoBehaviour
{
    [SerializeField] GameObject upgrade;
    [SerializeField] Image FBtn;
    [SerializeField] Text upgradeText;

    bool isCollisionCheck = false;
    const float fadeSpeed = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        upgrade.transform.localPosition = Camera.main.WorldToScreenPoint(transform.localPosition + new Vector3(-3f, -4.5f, 0));
        #endregion

        FClick();
    }

    void FClick()
    {
        if (isCollisionCheck == true && Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("test");
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;
            FBtn.DOFade(1, fadeSpeed).SetEase(Ease.Linear);
            upgradeText.DOFade(1, fadeSpeed).SetEase(Ease.Linear);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;
            FBtn.DOFade(0, fadeSpeed).SetEase(Ease.Linear);
            upgradeText.DOFade(0, fadeSpeed).SetEase(Ease.Linear);
        }
    }
}
