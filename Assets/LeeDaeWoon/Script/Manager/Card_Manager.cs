using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Manager : MonoBehaviour
{
    public static Card_Manager instance { get; private set; }

    public int randomMix = 0;

    public bool isLeftPick = true;
    public bool isAmongPick = true;
    public bool isRightPick = true;

    [SerializeField] ItemSo itemSo;
    [SerializeField] GameObject cardPrefab;

    [Header("마정석 과 방어구 및 장신구")]
    public List<Item> itemBuffer = new List<Item>();
    public List<Item> daBuffer = new List<Item>();

    [Header("왼쪽카드 / 가운데카드 / 오른쪽 카드")]
    public List<Item> itemDALeftCheck = new List<Item>();
    public List<Item> itemDAAmongCheck = new List<Item>();
    public List<Item> itemDARightCheck = new List<Item>();

    [Space(10)]
    public int itemCheck = 0;

    [Space(10)]
    public bool isDaLeft = true;
    public bool isDaAmong = true;
    public bool isDaRight = true;

    [Space(10)]
    public bool isItemLeft = true;
    public bool isItemAmong = true;
    public bool isItemRight = true;

    public Image fade;

    [Header("시간의 마정석 제한")]
    public List<Item> timeItemLimit = new List<Item>();
    public int timeItemCount = 0;

    public bool isItemBool = true;
    public bool isItemCardOpenCheck = true;
    int itemRandomTest;
    int dARandomTest;


    void Start()
    {
        AddList();
        isItemBool = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.O))
            AddCard();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            AddList();

        if (timeItemCount == 3)
        {
            timeItemCount++;
            itemBuffer.RemoveAt(4);
        }
    }

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

    public void AddList()
    {
        for (int i = 0; i < itemSo.Items.Count; i++)
            itemBuffer.Add(itemSo.Items[i]);
        
        for (int i = 0; i < itemSo.DA.Count; i++)
            daBuffer.Add(itemSo.DA[i]);
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

        UI_Manager.instance.timerCheck = false;
        int itemIndex = 0;
        List<Item> item = new List<Item>();
        var cardObject = Instantiate(cardPrefab, this.transform.position, Quaternion.identity, GameObject.Find("Item_Canvas").transform);
        var card = cardObject.GetComponent<Item_CardList>();
        itemDALeftCheck.Clear();
        itemDAAmongCheck.Clear();
        itemDARightCheck.Clear();

        isDaLeft = true;
        isDaAmong = true;
        isDaRight = true;

        isItemLeft = true;
        isItemAmong = true;
        isItemRight = true;


        for (int i = 0; i < 3; i++)
        {
            randomMix = Random.Range(0, 101);

            // 마정석
            if (randomMix <= 70 || daBuffer.Count == 0)
            {
                itemRandomTest = Card_Percent(itemBuffer);
                for (int j = 0; j < item.Count; j++)
                {
                    while (item[j] == itemBuffer[itemRandomTest])
                    {
                        itemRandomTest = Card_Percent(itemBuffer);
                    }
                }
                item.Add(itemBuffer[itemRandomTest]);
                card.ItemCard(itemBuffer[itemRandomTest], itemIndex++);

                if (i == 0)
                {
                    isItemLeft = false;
                    itemDALeftCheck.Add(itemBuffer[itemRandomTest]);
                }
                else if (i == 1)
                {
                    isItemAmong = false;
                    itemDAAmongCheck.Add(itemBuffer[itemRandomTest]);
                }
                else if (i == 2)
                {
                    isItemRight = false;
                    itemDARightCheck.Add(itemBuffer[itemRandomTest]);
                }
            }

            // 방어구 및 장신구
            else
            {
                dARandomTest = Card_Percent(daBuffer);
                for (int j = 0; j < item.Count; j++)
                {
                    while (item[j] == daBuffer[dARandomTest])
                    {
                        dARandomTest = Card_Percent(daBuffer);
                    }
                }
                item.Add(daBuffer[dARandomTest]);
                card.ItemCard(daBuffer[dARandomTest], itemIndex++);

                if (i == 0)
                {
                    isDaLeft = false;
                    itemDALeftCheck.Add(daBuffer[dARandomTest]);
                }
                else if (i == 1)
                {
                    isDaAmong = false;
                    itemDAAmongCheck.Add(daBuffer[dARandomTest]);
                }
                else if (i == 2)
                {
                    isDaRight = false;
                    itemDARightCheck.Add(daBuffer[dARandomTest]);
                }
            }
        }
    }

    public void Item_Reset()
    {
        randomMix = 0;
        itemCheck = 0;
        timeItemCount = 0;

        daBuffer.Clear();
        timeItemLimit.Clear();

        isLeftPick = true;
        isAmongPick = true;
        isRightPick = true;

        isDaLeft = true;
        isDaAmong = true;
        isDaRight = true;

        isItemLeft = true;
        isItemAmong = true;
        isItemRight = true;

        isItemBool = true;
        isItemCardOpenCheck = true;
    }
}
