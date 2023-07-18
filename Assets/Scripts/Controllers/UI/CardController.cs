using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Card card;
    public Image illustration, image;
    private Transform originalParent;

    private void Awake()
    {
        //Initialize(card);
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    public void Initialize(Card card, int ownerID, Transform intendedParent)
    {
        this.card = new Card(card);
        this.card.ownerID = ownerID;
        originalParent = intendedParent;
        Tweener tween = transform.DOMove(intendedParent.transform.position, 1, true);
        tween.onComplete +=
            () =>
            {
                transform.SetParent(intendedParent);
            };

        illustration.sprite = card.illustration;

    }


    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(originalParent.name == $"PlayersMiddleArea" || TurnManager.instance.currentPlayerTurn != card.ownerID)
        {
            transform.DOShakeScale(0.35f,0.5f,5);
        } 
        else
        {
            transform.SetParent(transform.root);
            image.raycastTarget = false;
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (originalParent.name == $"PlayersMiddleArea" || TurnManager.instance.currentPlayerTurn != card.ownerID)
        {

        }
        else
        {
            image.raycastTarget = true;
            AnalyzePointerUp(eventData);
        }
    }

    private void AnalyzePointerUp(PointerEventData eventData)
    {
        if(eventData.pointerEnter != null && eventData.pointerEnter.name == $"PlayersMiddleArea")
        {
            PlayCard(eventData.pointerEnter.transform);
        }
        else
        {
            ReturnToHand();
            transform.DOShakeScale(0.25f, 0.25f, 3);
        }

    }

    private void PlayCard(Transform playArea)
    {
        transform.SetParent(playArea);
        transform.localPosition = Vector3.zero;
        originalParent = playArea;
        CardManager.instance.PlayCard(this, card.ownerID);
    }

    private void ReturnToHand()
    {
/*        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;*/
        Tweener tween = transform.DOMove(originalParent.transform.position, 0.35f, true);
        tween.onComplete +=
            () =>
            {
                transform.SetParent(originalParent);
            };

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent == originalParent) return;
        transform.position = eventData.position;
    }
}
