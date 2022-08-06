using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Manager : MonoBehaviour
{
    public static Card_Manager Inst { get; private set; }

    public int RandomMix;

    public bool Left_Pick = true;
    public bool Among_Pick = true;
    public bool Right_Pick = true;

    [SerializeField] ItemSo itemSo;
    [SerializeField] GameObject CardPrefab;

    [Header("마정석 과 방어구 및 장신구")]
    public List<Item> ItemBuffer = new List<Item>();
    public List<Item> DABuffer = new List<Item>();

    [Header("왼쪽카드 / 가운데카드 / 오른쪽 카드")]
    public List<Item> ItemDA_LeftCheck = new List<Item>();
    public List<Item> ItemDA_AmongCheck = new List<Item>();
    public List<Item> ItemDA_RightCheck = new List<Item>();

    [Space(10)]
    public int Item_Check;
    public bool DAClick_Check = true;

    [Space(10)]
    public bool DA_Left = true;
    public bool DA_Among = true;
    public bool DA_Right = true;

    [Space(10)]
    public bool Item_Left = true;
    public bool Item_Among = true;
    public bool Item_Right = true;

    public Image Fade;

    [Header("시간의 마정석 제한")]
    public List<Item> Time_Item_Limit = new List<Item>();
    public int TimeItem_Count;


    public bool Item_bool = true;
    public bool ItemCard_OpenCheck = true;
    private int Item_RandomTest;
    private int DA_RandomTest;


    void Start()
    {
        AddList();
        Item_bool = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.O))
            AddCard();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            AddList();

        if (TimeItem_Count == 3)
        {
            TimeItem_Count++;
            ItemBuffer.RemoveAt(4);
        }
    }

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddList()
    {
        for (int i = 0; i < itemSo.Items.Count; i++)
            ItemBuffer.Add(itemSo.Items[i]);
        
        for (int i = 0; i < itemSo.DA.Count; i++)
            DABuffer.Add(itemSo.DA[i]);
    }

    public int Card_Percent(List<Item> Percent_Item)
    {
        int percent = Random.Range(0, 101);
        for (int i = 0; i < Percent_Item.Count; i++)
        {
            if (percent < Percent_Item[i].Item_Percent)
            {
                return i;
            }
            percent -= Percent_Item[i].Item_Percent;
        }
        return 0;
    }

    public void AddCard()
    {
        // 아이템 카드 소환
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

        UI_Manager.Inst.Timer_Check = false;
        int itemIndex = 0;
        List<Item> item = new List<Item>();
        var cardObject = Instantiate(CardPrefab, this.transform.position, Quaternion.identity, GameObject.Find("Item_Canvas").transform);
        var card = cardObject.GetComponent<Item_CardList>();
        ItemDA_LeftCheck.Clear();
        ItemDA_AmongCheck.Clear();
        ItemDA_RightCheck.Clear();

        DA_Left = true;
        DA_Among = true;
        DA_Right = true;

        Item_Left = true;
        Item_Among = true;
        Item_Right = true;


        for (int i = 0; i < 3; i++)
        {
            RandomMix = Random.Range(0, 101);

            // 마정석
            if (RandomMix <= 70 || DABuffer.Count == 0)
            {
                Item_RandomTest = Card_Percent(ItemBuffer);
                for (int j = 0; j < item.Count; j++)
                {
                    while (item[j] == ItemBuffer[Item_RandomTest])
                    {
                        Item_RandomTest = Card_Percent(ItemBuffer);
                    }
                }
                item.Add(ItemBuffer[Item_RandomTest]);
                card.ItemCard(ItemBuffer[Item_RandomTest], itemIndex++);

                if (i == 0)
                {
                    Item_Left = false;
                    ItemDA_LeftCheck.Add(ItemBuffer[Item_RandomTest]);
                }
                else if (i == 1)
                {
                    Item_Among = false;
                    ItemDA_AmongCheck.Add(ItemBuffer[Item_RandomTest]);
                }
                else if (i == 2)
                {
                    Item_Right = false;
                    ItemDA_RightCheck.Add(ItemBuffer[Item_RandomTest]);
                }
            }

            // 방어구 및 장신구
            else
            {
                DA_RandomTest = Card_Percent(DABuffer);
                for (int j = 0; j < item.Count; j++)
                {
                    while (item[j] == DABuffer[DA_RandomTest])
                    {
                        DA_RandomTest = Card_Percent(DABuffer);
                    }
                }
                item.Add(DABuffer[DA_RandomTest]);
                card.ItemCard(DABuffer[DA_RandomTest], itemIndex++);

                if (i == 0)
                {
                    DA_Left = false;
                    ItemDA_LeftCheck.Add(DABuffer[DA_RandomTest]);
                }
                else if (i == 1)
                {
                    DA_Among = false;
                    ItemDA_AmongCheck.Add(DABuffer[DA_RandomTest]);
                }
                else if (i == 2)
                {
                    DA_Right = false;
                    ItemDA_RightCheck.Add(DABuffer[DA_RandomTest]);
                }
            }
        }
    }

    public void Item_Reset()
    {
        RandomMix = 0;
        Item_Check = 0;
        TimeItem_Count = 0;

        DABuffer.Clear();
        Time_Item_Limit.Clear();

        Left_Pick = true;
        Among_Pick = true;
        Right_Pick = true;

        DA_Left = true;
        DA_Among = true;
        DA_Right = true;

        Item_Left = true;
        Item_Among = true;
        Item_Right = true;

        Item_bool = true;
        ItemCard_OpenCheck = true;
    }
}
