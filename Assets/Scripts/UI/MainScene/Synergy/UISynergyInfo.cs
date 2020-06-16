﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using geniikw.DataSheetLab;

public class UISynergyInfo : UIControl
{
    [SerializeField] private Text synergyName = null;
    [SerializeField] private Image synergyImage = null;
    [SerializeField] private Text synergyDescription = null;
    [SerializeField] private GridLayoutGroup girdLayoutGroup = null;
    [SerializeField] private UICharacterSlotToShow characterSlotToShow = null;

    //private List<UICharacterSlotToShow> characterSlotsToShow = new List<UICharacterSlotToShow>();
    private List<UICharacterSlotToShow> characterSlotsToShow { get; set; }

    private TribeData tribeData = null;
    private OriginData originData = null;

    bool isTribe { get; set; } = false;
    bool isOrigin = false;

    public void Initialize()
    {
        CreateCharacterList();
        Destroy(characterSlotToShow.gameObject);
    }

    public void SetSynergyData(TribeData newTribeData, string newSynegyName, string newDescription, Sprite newSprite)
    {
        isTribe = true;
        isOrigin = false;

        tribeData = newTribeData;
        SetName(newSynegyName);
        SetImage(newSprite);
        SetDescription(newDescription);
        Sort();
    }

    public void SetSynergyData(OriginData newOriginData, string newSynegyName, string newDescription, Sprite newSprite)
    {
        isTribe = false;
        isOrigin = true;

        originData = newOriginData;
        SetName(newSynegyName);
        SetImage(newSprite);
        SetDescription(newDescription);
        Sort();
    }

    public void SetName(string name)
    {
        if(name != null)
        {
            synergyName.text = name;
        }
        else
        {
            Debug.Log("No SynergyName");
        }
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            synergyImage.sprite = sprite;
        }
        else
        {
            Debug.Log("No Synergy Image");
        }
    }

    public void SetDescription(string description)
    {
        if (description != null)
        {
            synergyDescription.text = description;
        }
        else
        {
            Debug.Log("No Description");
        }
    }

    public void CreateCharacterList()
    {
        characterSlotsToShow = new List<UICharacterSlotToShow>();

        var characterDatas = GameManager.instance.dataSheet.characterDataSheet.characterDatas;
        foreach (var characterData in characterDatas)
        {
            var slot = Instantiate(characterSlotToShow, girdLayoutGroup.transform);
            slot.SetCharacterData(characterData.Value);
            characterSlotsToShow.Add(slot);
        }
    }


    public void Sort()
    {
        if (isTribe)
        {
            foreach (var characterSlotToShow in characterSlotsToShow)
            {
                if(tribeData.Tribe == characterSlotToShow.characterData.Tribe)
                {
                    characterSlotToShow.gameObject.SetActive(true);
                }
                else
                {
                    characterSlotToShow.gameObject.SetActive(false);
                }
            }
        }
        else if (isOrigin)
        {
            foreach (var characterSlotToShow in characterSlotsToShow)
            {
                if (originData.Origin == characterSlotToShow.characterData.Origin)
                {
                    characterSlotToShow.gameObject.SetActive(true);
                }
                else
                {
                    characterSlotToShow.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("Sort Error");
        }
    }
}
