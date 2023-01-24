using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance { get; private set; }

    int randomMix = 0;

    [SerializeField] ItemSo itemSo;
    [SerializeField] GameObject cardPrefab;

    [Header("마정석 과 방어구 및 장신구")]
    public List<Item> itemBuffer = new List<Item>();
    public List<Item> daBuffer = new List<Item>();

    [Space(10)]
    public Image fade;
    public bool isItemClick = false;

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
        if (Input.GetKeyDown(KeyCode.Keypad1))
            AddCard();

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

    // 아이템 카드 소환하는 함수
    public void AddCard()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

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
            }
        }
    }
}
