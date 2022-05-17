using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsPicker : MonoBehaviour
{
    public List<GameObject> buffItemSpots;//3 œrodkowe
    public Button slotButton1;
    public List<Sprite> buffSprites;
    
    public ShopBuffs shopBuffs;
    private void Awake()
    {
        //shopBuffs = new ShopBuffs();
        PickBuffs();
    }

    public void PickBuffs()
    {
        int index=Random.Range(0, buffSprites.Count);
        AssignButton(index, buffItemSpots[0]);
        index = Random.Range(0, buffSprites.Count);
        AssignButton(index, buffItemSpots[1]);
        index = Random.Range(0, buffSprites.Count);
        AssignButton(index, buffItemSpots[2]);

    }
    public void AssignButton(int index,GameObject go)
    {
        index = 1;
        go.GetComponent<ItemHolder>().logo.sprite = buffSprites[index];
        go.GetComponent<ItemHolder>().item.price = 100;
        slotButton1.onClick.AddListener(() => shopBuffs.BuffAllSuitPower(Suit.HEARTS));
        Button button = go.GetComponentInChildren<Button>();
        if (index == 0)
        {
            button.onClick.AddListener(() => shopBuffs.BuffAllCardDmg(1));
           
        }
        if (index == 1)
        {
            button.onClick.AddListener(() => shopBuffs.BuffAllSuitPower(Suit.HEARTS));
            
        }
        if (index == 2)
        {
            button.onClick.AddListener(() => shopBuffs.BuffAllSuitPower(Suit.DIAMONDS));
        }
        if (index == 3)
        {
            button.onClick.AddListener(() => shopBuffs.BuffAllSuitPower(Suit.SPADES));
        }
        if (index == 4)
        {
            button.onClick.AddListener(() => shopBuffs.BuffAllSuitPower(Suit.CLUBS));
        }
        if (index == 5)
        {
            button.onClick.AddListener(() => shopBuffs.BuffKings(1));
        }
        if (index == 6)
        {
            button.onClick.AddListener(() => shopBuffs.QueensGivesBarrier());
        }
        if (index == 7)
        {
            button.onClick.AddListener(() => shopBuffs.BuffJacks(1));
        }
        if (index == 8)
        {
            button.onClick.AddListener(() => shopBuffs.BuffJokers(1));
            
            
        }
    }
}
