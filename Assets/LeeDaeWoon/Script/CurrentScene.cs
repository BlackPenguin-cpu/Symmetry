using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EScene
{
    Main,
    Ingame,
    Dimension
}

public class CurrentScene : MonoBehaviour
{
    public static CurrentScene instance;

    public EScene eScene;

    void Start()
    {
        PlayerVector();
    }

    void Update()
    {

    }

    void Awake()
    {
        instance = this;
    }

    private void OnLevelWasLoaded(int level)
    {
        //DOTween.PauseAll();
        //switch(SceneManager.GetActiveScene().buildIndex)
        //{
        //    case 0: // Title
        //        UIManager.instance.fadeInOut.DOFade(0, 0);
        //        break;

        //    case 3: // Dimension
        //        Potal.Inst.Player.DOFade(1, 0);
        //        Potal.Inst.Dark_Player.DOFade(1, 0);

        //        Skill_Manager.instance.isPotalMove = false;
        //        UIManager.instance.isPlayerControl = false;

        //        UIManager.instance.fadeInOut.DOFade(0, 0);
        //        break;
        //}
    }

    public void PlayerVector()
    {
        switch (eScene)
        {
            case EScene.Main:
                Vector2 mainPos = new Vector2(-10, -1.8f);
                Player.Instance.transform.DOMove(mainPos, 0);
                break;

            case EScene.Ingame:
                Vector2 ingamePos = new Vector2(1, 0);
                Player.Instance.transform.DOLocalMove(ingamePos, 0);
                break;

            case EScene.Dimension:
                Vector2 dimensionPos = new Vector2(1, 0);
                Player.Instance.transform.DOLocalMove(dimensionPos, 0);
                break;
        }
    }
}
