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

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);

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
        switch (eDirection)
        {
            case EDirection.Left:
                StartCoroutine(LeftClickWindow());

                //for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
                //{
                //    if (CardManager.instance.daBuffer[i].name == ItemCardList.instance.leftItem.name)
                //    {
                //        StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
                //        CardManager.instance.daBuffer.RemoveAt(i);
                //    }
                //}

                for (int i = 0; i < CardManager.instance.itemBuffer.Count; i++)
                {
                    if (CardManager.instance.itemBuffer[i].name.Contains(ItemCardList.instance.leftItem.name))
                    {
                        StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemBuffer[i]);
                        CardManager.instance.itemBuffer.RemoveAt(i);

                        //CardManager.instance.timeItemCount++;
                        //CardManager.instance.timeItemLimit.Clear();
                    }
                }

                break;

            case EDirection.Among:


                StartCoroutine(AmongClickWindow());

                break;

            case EDirection.Right:



                StartCoroutine(RightClickWindow());

                break;
        }

        //// 왼쪽 카드를 선택 했을 때
        //if (eDirection == EDirection.Left)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isLeftPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isRightPick = false;
        //        CardManager.instance.isAmongPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaLeft == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }

        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaLeft == true)
        //        {
        //            switch (CardManager.instance.itemDALeftCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }


        //        if (CardManager.instance.isItemLeft == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDALeftCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += LeftClick_Check;


        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDALeftCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        leftLight.DOFade(1f, 0.1f);
        //        leftWindow.transform.DOLocalMoveY(1100, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }
        //}

        //// 가운데 카드를 선택 했을 때
        //if (eDirection == EDirection.Among)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isAmongPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isAmongPick = false;
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isRightPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaAmong == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }
        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaAmong == true)
        //        {
        //            switch (CardManager.instance.itemDAAmongCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }


        //        if (CardManager.instance.isItemAmong == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDAAmongCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += AmongClick_Check;

        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDAAmongCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        amongLight.DOFade(1f, 0.1f);
        //        amongWindow.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }

        //}

        //// 오른쪽 카드를 선택 했을 때
        //if (eDirection == EDirection.Right)
        //{
        //    CardManager.instance.fade.DOFade(0f, 0.5f);
        //    UI_Manager.instance.isCursorFade = false;

        //    if (CardManager.instance.isRightPick == true && CardManager.instance.isItemCardOpenCheck == false)
        //    {
        //        CardManager.instance.isRightPick = false;
        //        CardManager.instance.isLeftPick = false;
        //        CardManager.instance.isAmongPick = false;

        //        // 방어구 및 장신구를 선택했을 때
        //        if (CardManager.instance.isDaRight == false)
        //        {
        //            for (int i = 0; i < CardManager.instance.daBuffer.Count; i++)
        //            {
        //                if (CardManager.instance.daBuffer[i].Itme_Name == CardManager.instance.itemDARightCheck[0].Itme_Name)
        //                {
        //                    StopManager.instnace.ItemDA_Have.Add(CardManager.instance.daBuffer[i]);
        //                    CardManager.instance.daBuffer.RemoveAt(i);
        //                }
        //            }

        //            switch (CardManager.instance.itemDARightCheck[0].Itme_Name)
        //            {
        //                case "바람의 귀걸이":
        //                    Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
        //                    break;

        //                case "가시견갑":
        //                    Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
        //                    break;

        //                case "칼날망토":
        //                    Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
        //                    break;

        //                case "저주받은 단검":
        //                    Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
        //                    break;

        //                case "피의 장갑":
        //                    Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
        //                    break;

        //                case "수정구":
        //                    Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
        //                    break;

        //                case "절대반지":
        //                    Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
        //                    Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
        //                    break;

        //            }
        //        }

        //        // 마정석을 선택했을 때
        //        if (CardManager.instance.isDaRight == true)
        //        {
        //            switch (CardManager.instance.itemDARightCheck[0].Itme_Name)
        //            {
        //                case "힘의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
        //                    break;

        //                case "신속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
        //                    break;

        //                case "연속의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
        //                    break;

        //                case "체력의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
        //                    break;

        //                case "시간의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
        //                    break;

        //                case "방어의 마정석":
        //                    Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
        //                    break;
        //            }
        //        }

        //        if (CardManager.instance.isItemRight == false)
        //            StopManager.instnace.ItemDA_Have.Add(CardManager.instance.itemDARightCheck[0]);

        //        if (CardManager.instance.isItemBool == true)
        //            CardManager.instance.isItemBool = false;

        //        else if (CardManager.instance.isItemBool == false)
        //            CardManager.instance.itemCheck += RightClick_Check;

        //        if (CardManager.instance.timeItemCount <= 2)
        //        {
        //            CardManager.instance.timeItemLimit.Add(CardManager.instance.itemDARightCheck[0]);
        //            for (int i = 0; i < CardManager.instance.timeItemLimit.Count; i++)
        //            {
        //                if (CardManager.instance.timeItemLimit[i].Itme_Name.Contains(CardManager.instance.itemBuffer[4].Itme_Name))
        //                {
        //                    CardManager.instance.timeItemCount++;
        //                    CardManager.instance.timeItemLimit.Clear();
        //                }
        //            }
        //        }

        //        rightLight.DOFade(1f, 0.1f);
        //        rightWindow.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
        //        StartCoroutine(Close_Dot());
        //    }
        //}
    }
}
