using UnityEngine;
[System.Serializable]
public class Card
{
    public string flowerFamily;
    public string category;
    public string extraCategory = "";
    public Sprite illustration;
    public int ownerID;

    public Card()
    {

    }

    public Card(Card card)
    {
        flowerFamily = card.flowerFamily;
        category = card.category;
        extraCategory = card.extraCategory;
        illustration = card.illustration;
        ownerID = card.ownerID;
    }

    public Card(string flowerFamily, string category, string extraCategory, Sprite illustration)
    {
        this.flowerFamily = flowerFamily;
        this.category = category;
        this.extraCategory = extraCategory;
        this.illustration = illustration;
    }
}
