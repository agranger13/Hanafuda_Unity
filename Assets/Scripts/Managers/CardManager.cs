using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> cards = new List<Card>(), player1Deck = new List<Card>(), player2Deck = new List<Card>();
    public Transform player1Hand, player2Hand, playersMiddleArea, drawZone;
    public CardController cardControllerPrefab;
    public List<CardController> player1Cards = new List<CardController>(),
        player2Cards = new List<CardController>(),
        playersMiddleAreaCards = new List<CardController>(),
        player1HandCards = new List<CardController>(),
        player2HandCards = new List<CardController>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetAllCards();
        GenerateCards();
        //FillDecks();
    }

    /* private void FillDecks()
    {
        foreach (Card card in cards)
        {
            player1Deck.Add(new Card(card));
        }

        foreach (Card card in cards)
        {
            player2Deck.Add(new Card(card));
        }
    } */

    private void GetAllCards()
    {
        var tilesImg = Resources.LoadAll("Art/tiles", typeof(Sprite)).Cast<Sprite>();
        foreach (var t in tilesImg)
        {
            List<String> nameSplit = t.name.Split('_').ToList();
            Card newCard;
            if (nameSplit.Count >= 3)
            {
                newCard = new(nameSplit[0], nameSplit[1], nameSplit[2], t);
            }
            else
            {
                newCard = new(nameSplit[0], nameSplit[1], "", t);
            }

            cards.Add(newCard);
        }
    }

    public void GenerateCards()
    {

        for(var i=0; i < 8*3; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cards.Count);
            CardController newCard = Instantiate(cardControllerPrefab, drawZone);
            newCard.transform.localPosition = Vector3.zero;
            if (i % 3 == 0)
            {
                newCard.Initialize(cards[randomCard], 0, player1Hand);
                player1HandCards.Add(newCard);
            }else if (i % 3 == 1)
            {
                newCard.Initialize(cards[randomCard], 1, player2Hand);
                player2HandCards.Add(newCard);
            }
            else
            {
                newCard.Initialize(cards[randomCard], 0, playersMiddleArea);
                playersMiddleAreaCards.Add(newCard);
            }
            cards.RemoveAt(randomCard);

        }

        /*List<Card> oldcardsList = cards;
        foreach(Card card in oldcardsList)
        {
            CardController newCard = Instantiate(cardControllerPrefab, drawZone);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 0, player1Hand);
            player1HandCards.Add(newCard);
        }
        foreach (Card card in oldcardsList)
        {
            CardController newCard = Instantiate(cardControllerPrefab, drawZone);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 1, player2Hand);
            player2HandCards.Add(newCard);
        }*/
        }

    public void PlayCard(CardController card, int ID)
    {
        if(ID == 0)
        {
            player1Cards.Add(card);
            player1HandCards.Remove(card);
        }
        else
        {
            player2Cards.Add(card);
            player2HandCards.Remove(card);
        }
        StartCoroutine(waiter());
        
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        TurnManager.instance.EndTurn();
    }

    public void ProcessStartTurn(int ID)
    {

    }

    public void ProcessEndTurn(int ID)
    {
        // Draw Card
        if ((ID == 0 ? player1HandCards : player2HandCards).Count < 8)
        {
            int randomCard = UnityEngine.Random.Range(0, this.cards.Count);
            CardController newCard = Instantiate(cardControllerPrefab, drawZone);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(cards[randomCard], ID, ID == 0 ? player1Hand : player2Hand);
            if (ID == 0)
                player1HandCards.Add(newCard);
            else
                player2HandCards.Add(newCard);
            cards.RemoveAt(randomCard);
        }

    }
}
