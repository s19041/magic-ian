using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{

    public Card card;

    public Image artworkImage;

    public Text descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        updateDisplay();
    }
    public void updateDisplay()
    {
        artworkImage.sprite = card.artwork;

        descriptionText.text = card.description;
    }
}

    
