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
                this.leftItem = item;

                leftName.text = this.leftItem.name;
                leftExplanation.text = this.leftItem.explanation;
                leftIcon.sprite = this.leftItem.icon;
                break;
            case 1:
                this.amongItem = item;

                amongName.text = this.amongItem.name;
                amongExplanation.text = this.amongItem.explanation;
                amongIcon.sprite = this.amongItem.icon;
                break;
            case 2:
                this.rightItem = item;

                rightName.text = this.rightItem.name;
                rightExplanation.text = this.rightItem.explanation;
                rightIcon.sprite = this.rightItem.icon;
                break;

        }
    }
}
