using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EScene : short
{
    Main,
    Ingame,
    Dimension
}

public class CurrentScene : MonoBehaviour
{
    public static CurrentScene instance;
    void Awake() => instance = this;

    public EScene eScene;

    void Start()
    {
        PlayerVector();
    }

    void Update()
    {

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
