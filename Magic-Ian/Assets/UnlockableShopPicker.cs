using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableShopPicker : MonoBehaviour//chyba brzydszej klasy to jeszcze nie zrobi³em...
{
    public GameObject unlockableSpot;
    DeckBuilder deckBuilder;
    PlayerManager playerManager;
    int safeGuard;
    int unlockablesCount;
    private void Awake()
    {
        safeGuard = 0;
        deckBuilder = DeckBuilder.Instance;
        playerManager = PlayerManager.Instance;
        unlockablesCount = 10;
        int index = Random.Range(0, unlockablesCount);
        AssignButton(index);
    }
    public void AssignButton(int index)
    {
        safeGuard++;
        if (safeGuard > unlockablesCount)
            return;
        unlockableSpot.GetComponent<ItemHolder>().item.price = 200;
        Button button = unlockableSpot.GetComponentInChildren<Button>();
        if (index == 0)
        {
            if (!playerManager.GetUnlockedItems().Exists(x => x.itemName == ItemName.CAPE))
            {
                button.onClick.AddListener(() => playerManager.UnlockItem(deckBuilder.items[2]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.items[2].artwork;
            }
            else
                AssignButton(1);

        }
        if (index == 1)
        {
            if (!playerManager.GetUnlockedItems().Exists(x => x.itemName == ItemName.MONOCLE))
            {
                button.onClick.AddListener(() => playerManager.UnlockItem(deckBuilder.items[3]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.items[3].artwork;
            }
            else
                AssignButton(2);
        }
        if (index == 2)
        {
            if (!playerManager.GetUnlockedCards().Exists(x=>x.suit ==deckBuilder.clubs[10].suit && x.rank == deckBuilder.clubs[10].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.clubs[10]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.clubs[10].artwork;
            }
            else
                AssignButton(3);
        }
        if (index == 3)
        {
            if (!playerManager.GetUnlockedCards().Exists(x=>x.suit == deckBuilder.clubs[11].suit && x.rank == deckBuilder.clubs[11].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.clubs[11]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.clubs[11].artwork;
            }
            else
                AssignButton(4);
        }
        if (index == 4)
        {
            if (!playerManager.GetUnlockedCards().Exists(x=>x.suit == deckBuilder.clubs[12].suit && x.rank == deckBuilder.clubs[12].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.clubs[12]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.clubs[12].artwork;
            }
            else
                AssignButton(5);
        }
        if (index == 5)
        {
            if (!playerManager.GetUnlockedCards().Exists(x => x.suit == deckBuilder.diamonds[10].suit && x.rank == deckBuilder.diamonds[10].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.diamonds[10]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.diamonds[10].artwork;
            }
            else
                AssignButton(6);
        }
        if (index == 6)
        {
            if (!playerManager.GetUnlockedCards().Exists(x => x.suit == deckBuilder.diamonds[11].suit && x.rank == deckBuilder.diamonds[11].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.diamonds[11]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.diamonds[11].artwork;
            }
            else
                AssignButton(7);
        }
        if (index == 7)
        {
            safeGuard++;
            if (!playerManager.GetUnlockedCards().Exists(x => x.suit == deckBuilder.diamonds[12].suit && x.rank == deckBuilder.diamonds[12].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.diamonds[12]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.diamonds[12].artwork;
            }
            else
                AssignButton(8);
        }
        if (index == 8)
        {
            safeGuard++;
            if (!playerManager.GetUnlockedCards().Exists(x => x.suit == deckBuilder.diamonds[0].suit && x.rank == deckBuilder.diamonds[0].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockSuit(Suit.DIAMONDS));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.diamonds[0].artwork;
                unlockableSpot.GetComponent<ItemHolder>().item.price = 300;
            }
            else
                AssignButton(9);
        }
        if (index == 9)
        {
            safeGuard++;
            if (!playerManager.GetUnlockedCards().Exists(x => x.suit == deckBuilder.special[0].suit && x.rank == deckBuilder.special[0].rank))
            {
                button.onClick.AddListener(() => playerManager.UnlockCard(deckBuilder.special[0]));
                unlockableSpot.GetComponent<ItemHolder>().logo.sprite = deckBuilder.special[0].artwork;
                unlockableSpot.GetComponent<ItemHolder>().item.price = 300;
            }
            else
                AssignButton(0);
        }
    }
}
