using UnityEngine;
using UnityEngine.UI;

public enum ItemName
{ CYLINDER, SLEEVE, CAPE, MONOCLE }

[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Text descriptionText;
    public ItemName itemName;
    public string buttonText;
    public Sprite artwork;
}