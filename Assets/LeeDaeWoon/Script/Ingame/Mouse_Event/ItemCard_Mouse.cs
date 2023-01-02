using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]


public class ItemCard_Mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum EDirection
    {
        Left,
        Among,
        Right
    }
    public EDirection eDirection;
    float timer = 0;

    [Header("빛")]
    public Image leftLight;
    public Image amongLight;
    public Image rightLight;

    const float lightTimer = 0.5f;

    [Header("창")]
    [SerializeField] GameObject leftWindow;
    [SerializeField] GameObject amongWindow;
    [SerializeField] GameObject rightWindow;
    [SerializeField] RectTransform leftRect;
    [SerializeField] RectTransform amongRect;
    [SerializeField] RectTransform rightRect;

    const int windowWidth = 545;
    const int windowHeight = 890;
    const int windowClickPos = 1200;

    [Header("봉")]
    [SerializeField] GameObject leftBarUp;
    [SerializeField] GameObject leftBarDown;
    [SerializeField] GameObject amongBarUp;
    [SerializeField] GameObject amongBarDown;
    [SerializeField] GameObject rightBarUp;
    [SerializeField] GameObject rightBarDown;

    const int barClose = 40;
    const float barSpeed = 0.38f;

    void Start()
    {

    }

    void Update()
    {

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);

        if (CardManager.instance.isItemClick == true)
        {
            switch (eDirection)
            {
                case EDirection.Left:
                    leftLight.DOFade(1, lightTimer).SetUpdate(true);
                    break;
                case EDirection.Among:
                    amongLight.DOFade(1, lightTimer).SetUpdate(true);
                    break;
                case EDirection.Right:
                    rightLight.DOFade(1, lightTimer).SetUpdate(true);
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (eDirection)
        {
            case EDirection.Left:
                leftLight.DOFade(0, lightTimer).SetUpdate(true);
                break;
            case EDirection.Among:
                amongLight.DOFade(0, lightTimer).SetUpdate(true);
                break;
            case EDirection.Right:
                rightLight.DOFade(0, lightTimer).SetUpdate(true);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardManager.instance.isItemClick = false;

        switch (eDirection)
        {
            case EDirection.Left:
                StartCoroutine(LeftClickWindow());
                DaItemClick();

                switch (ItemCardList.instance.leftItem.eItem)
                {
                    case Item.EItem.WindEarRing:
                        Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                        break;

                    case Item.EItem.NeedleArmour:
                        Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                        break;

                    case Item.EItem.KnifeCape:
                        Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                        break;

                    case Item.EItem.CurseKnife:
                        Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                        break;

                    case Item.EItem.BloodGauntlet:
                        Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                        break;

                    case Item.EItem.CrystalOrb:
                        Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                        break;

                    case Item.EItem.TheOneRing:
                        Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                        break;
                }

                switch (ItemCardList.instance.leftItem.eItem)
                {
                    case Item.EItem.POWER:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.POWER];
                        break;

                    case Item.EItem.SPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.SPEED];
                        break;

                    case Item.EItem.ATTACKSPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED];
                        break;

                    case Item.EItem.HEALTH:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH];
                        break;

                    case Item.EItem.TIME:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.TIME];
                        break;

                    case Item.EItem.DEFFENCE:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE];
                        break;
                }

                break;

            case EDirection.Among:
                StartCoroutine(AmongClickWindow());
                DaItemClick();

                switch (ItemCardList.instance.amongItem.eItem)
                {
                    case Item.EItem.WindEarRing:
                        Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                        break;

                    case Item.EItem.NeedleArmour:
                        Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                        break;

                    case Item.EItem.KnifeCape:
                        Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                        break;

                    case Item.EItem.CurseKnife:
                        Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                        break;

                    case Item.EItem.BloodGauntlet:
                        Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                        break;

                    case Item.EItem.CrystalOrb:
                        Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                        break;

                    case Item.EItem.TheOneRing:
                        Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                        break;
                }

                switch (ItemCardList.instance.amongItem.eItem)
                {
                    case Item.EItem.POWER:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.POWER];
                        break;

                    case Item.EItem.SPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.SPEED];
                        break;

                    case Item.EItem.ATTACKSPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED];
                        break;

                    case Item.EItem.HEALTH:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH];
                        break;

                    case Item.EItem.TIME:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.TIME];
                        break;

                    case Item.EItem.DEFFENCE:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE];
                        break;
                }

                break;

            case EDirection.Right:
                StartCoroutine(RightClickWindow());
                DaItemClick();

                switch (ItemCardList.instance.rightItem.eItem)
                {
                    case Item.EItem.WindEarRing:
                        Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                        break;

                    case Item.EItem.NeedleArmour:
                        Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                        break;

                    case Item.EItem.KnifeCape:
                        Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                        break;

                    case Item.EItem.CurseKnife:
                        Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                        break;

                    case Item.EItem.BloodGauntlet:
                        Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                        break;

                    case Item.EItem.CrystalOrb:
                        Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                        break;

                    case Item.EItem.TheOneRing:
                        Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                        Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                        break;
                }

                switch (ItemCardList.instance.rightItem.eItem)
                {
                    case Item.EItem.POWER:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.POWER];
                        break;

                    case Item.EItem.SPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.SPEED];
                        break;

                    case Item.EItem.ATTACKSPEED:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED];
                        break;

                    case Item.EItem.HEALTH:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH];
                        break;

                    case Item.EItem.TIME:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.TIME];
                        break;

                    case Item.EItem.DEFFENCE:
                        ++Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE];
                        break;
                }

                break;
        }
    }

    #region 얻을 마정석과 장신구 선택
    void DaItemClick()
    {
        // 장신구
        for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        {
            if (CardManager.instance.daBuffer[i].name.Contains(ItemCardList.instance.leftItem.name))
            {
                StopManager.instnace.itemDaHave.Add(CardManager.instance.daBuffer[i]);
                CardManager.instance.daBuffer.RemoveAt(i);
            }
        }

        // 마정석
        for (int i = 0; i < CardManager.instance.itemBuffer.Count; i++)
        {
            if (CardManager.instance.itemBuffer[i].name.Contains(ItemCardList.instance.leftItem.name))
            {
                StopManager.instnace.itemDaHave.Add(CardManager.instance.itemBuffer[i]);
                CardManager.instance.itemBuffer.RemoveAt(i);
            }
        }
    }
    #endregion

    #region 클릭을 통한 창 움직임
    void LightDoKill()
    {
        leftLight.DOKill();
        amongLight.DOKill();
        rightLight.DOKill();
    }

    IEnumerator LeftClickWindow()
    {
        leftWindow.transform.DOLocalMoveY(windowClickPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            leftWindow.transform.DOKill();
            amongBarUp.transform.DOKill();
            amongBarDown.transform.DOKill();
            rightBarUp.transform.DOKill();
            rightBarDown.transform.DOKill();
            LightDoKill();

            Destroy(transform.parent.gameObject);
        });

        amongBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        amongBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        rightBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        rightBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            amongRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
            rightRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));

            timer += Time.unscaledDeltaTime * 2.8f;
            yield return null;
        }
    }

    IEnumerator AmongClickWindow()
    {
        amongWindow.transform.DOLocalMoveY(windowClickPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            amongWindow.transform.DOKill();
            leftBarUp.transform.DOKill();
            leftBarDown.transform.DOKill();
            rightBarUp.transform.DOKill();
            rightBarDown.transform.DOKill();
            LightDoKill();

            Destroy(transform.parent.gameObject);
        });

        leftBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        leftBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        rightBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        rightBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            leftRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
            rightRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));

            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }

    IEnumerator RightClickWindow()
    {
        rightWindow.transform.DOLocalMoveY(windowClickPos, barSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            rightWindow.transform.DOKill();
            leftBarUp.transform.DOKill();
            leftBarDown.transform.DOKill();
            amongBarUp.transform.DOKill();
            amongBarDown.transform.DOKill();
            LightDoKill();

            Destroy(transform.parent.gameObject);
        });

        leftBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        leftBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        amongBarUp.transform.DOLocalMoveY(barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        amongBarDown.transform.DOLocalMoveY(-barClose, barSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            leftRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
            amongRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));

            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }
    #endregion
}
