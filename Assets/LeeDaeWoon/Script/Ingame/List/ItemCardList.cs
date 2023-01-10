using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardList : MonoBehaviour
{
    public static ItemCardList instance;
    void Awake() => instance = this;

    [Header("왼쪽 카드")]
    public Image leftIcon;
    public Text leftName;
    public Text leftExplanation;

    [Header("가운데 카드")]
    public Image amongIcon;
    public Text amongName;
    public Text amongExplanation;

    [Header("오른쪽 카드")]
    public Image rightIcon;
    public Text rightName;
    public Text rightExplanation;

    public Item leftItem;
    public Item amongItem;
    public Item rightItem;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ItemCard(Item item, int itemIndex)
    {
        switch (itemIndex)
        {
            case 0:
                leftItem = item;

                leftName.text = leftItem.name;
                leftExplanation.text = leftItem.explanation;
                leftIcon.sprite = leftItem.icon;
                break;
            case 1:
                amongItem = item;

                amongName.text = amongItem.name;
                amongExplanation.text = amongItem.explanation;
                amongIcon.sprite = amongItem.icon;
                break;
            case 2:
                rightItem = item;

                rightName.text = rightItem.name;
                rightExplanation.text = rightItem.explanation;
                rightIcon.sprite = rightItem.icon;
                break;

        }
    }
}
