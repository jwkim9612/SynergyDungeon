﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using geniikw.DataSheetLab;

public class UICharacterCard : MonoBehaviour
{
    [SerializeField] public Image characterImage = null;
    [SerializeField] private Text priceText = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Text tribeText = null;
    [SerializeField] private Image originImage = null;
    [SerializeField] private Text originText = null;
    [SerializeField] private Text characterNameText = null;
    [SerializeField] private Image tierBorderImage = null;
    [SerializeField] private Button buyButton = null;

    public CharacterData characterData;
    public bool isBoughtCard = false;

    void Start()
    {
        buyButton.onClick.AddListener(() => {
            if(InGameManager.instance.uiPrepareArea.BuyCharacter(characterData.id))
            {
                isBoughtCard = true;
                OnHide();
            }
            else
            {
                Debug.Log("꽉참");
            }
        });
    }

    public void SetCard(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetCharacterImage(characterData.image);
        SetPriceText(((int)(characterData.tier)).ToString());
        SetTribeImage(characterData.tribeData.Sheet[characterData.tribeData.idxList[0]].image);
        SetTribeText(characterData.tribeData.Sheet[characterData.tribeData.idxList[0]].strTribe);
        SetOriginImage(characterData.originData.Sheet[characterData.originData.idxList[0]].image);
        SetOriginText(characterData.originData.Sheet[characterData.originData.idxList[0]].strOrigin);
        SetCharacterNameText(characterData.name);
        SetTierBorderImage(CardService.GetColorByTier(characterData.tier));

        OnShow();
    }

    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void SetPriceText(string text)
    {
        priceText.text = text;
    }

    public void SetTribeImage(Sprite sprite)
    {
        tribeImage.sprite = sprite;
    }

    public void SetTribeText(string text)
    {
        tribeText.text = text;
    }

    public void SetOriginImage(Sprite sprite)
    {
        originImage.sprite = sprite;
    }

    public void SetOriginText(string text)
    {
        originText.text = text;
    }

    public void SetCharacterNameText(string text)
    {
        characterNameText.text = text;
    }


    public void SetTierBorderImage(Color color)
    {
        tierBorderImage.color = color;
    }

    void OnHide()
    {
        buyButton.interactable = false;
        this.gameObject.SetActive(false);
    }

    void OnShow()
    {
        buyButton.interactable = true;
        this.gameObject.SetActive(true);
    }
}
