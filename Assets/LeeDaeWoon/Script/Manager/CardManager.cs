using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance { get; private set; }

    public int randomMix = 0;

    [SerializeField] ItemSo itemSo;
    [SerializeField] GameObject cardPrefab;

    [Header("마정석 과 방어구 및 장신구")]
    public List<Item> itemBuffer = new List<Item>();
    public List<Item> daBuffer = new List<Item>();

    [Space(10)]
    public int itemCheck = 0;

    [Space(10)]
    public bool isDaLeft = false;
    public bool isDaAmong = false;
    public bool isDaRight = false;

    [Space(10)]
    public bool isItemLeft = false;
    public bool isItemAmong = false;
    public bool isItemRight = false;

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
            if (percent < Percent_Item[i].percent)
                return i;

            percent -= Percent_Item[i].percent;
        }
        return 0;
    }

    public void AddCard()
    {
        // 아이템 카드 소환
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        Time.timeScale = 0;

        int itemIndex = 0;
        List<Item> item = new List<Item>();
        var cardObject = Instantiate(cardPrefab, transform.position, Quaternion.identity,transform.GetChild(0));
        var card = cardObject.GetComponent<ItemCardList>();

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
                        itemRandomTest = Card_Percent(itemBuffer);
                }

                item.Add(itemBuffer[itemRandomTest]);
                card.ItemCard(itemBuffer[itemRandomTest], itemIndex++);

                //switch(i)
                //{
                //    case 0:
                //        isItemLeft = true;
                //        itemDALeftCheck.Add(itemBuffer[itemRandomTest]);
                //        break;
                //    case 1:
                //        isItemAmong = true;
                //        itemDAAmongCheck.Add(itemBuffer[itemRandomTest]);
                //        break;
                //    case 2:
                //        isItemRight = true;
                //        itemDARightCheck.Add(itemBuffer[itemRandomTest]);
                //        break;
                //}
            }

            // 방어구 및 장신구
            else
            {
                dARandomTest = Card_Percent(daBuffer);
                for (int j = 0; j < item.Count; j++)
                {
                    while (item[j] == daBuffer[dARandomTest])
                        dARandomTest = Card_Percent(daBuffer);
                }

                item.Add(daBuffer[dARandomTest]);
                card.ItemCard(daBuffer[dARandomTest], itemIndex++);

                //switch (i)
                //{
                //    case 0:
                //        isDaLeft = true;
                //        itemDALeftCheck.Add(daBuffer[dARandomTest]);
                //        break;
                //    case 1:
                //        isDaAmong = true;
                //        itemDAAmongCheck.Add(daBuffer[dARandomTest]);
                //        break;
                //    case 2:
                //        isDaRight = true;
                //        itemDARightCheck.Add(daBuffer[dARandomTest]);
                //        break;
                //}
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

        isDaLeft = false;
        isDaAmong = false;
        isDaRight = false;

        isItemLeft = false;
        isItemAmong = false;
        isItemRight = false;

        isItemBool = true;
        isItemCardOpenCheck = true;
    }
}
