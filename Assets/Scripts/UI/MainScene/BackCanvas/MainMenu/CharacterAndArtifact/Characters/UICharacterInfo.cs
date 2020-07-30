﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : UIControl
{ 
    [SerializeField] private Text textName = null;
    [SerializeField] private Image image = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Text tribeText = null;
    [SerializeField] private Image originImage = null;
    [SerializeField] private Text originText = null;
    [SerializeField] private Text textAttack = null;
    [SerializeField] private Text textMagicalAttack = null;
    [SerializeField] private Text textHealth = null;
    [SerializeField] private Text textDefence = null;
    [SerializeField] private Text textMagicDefence = null;
    [SerializeField] private Text textShield = null;
    [SerializeField] private Text textAccuracy = null;
    [SerializeField] private Text textEvasion = null;
    [SerializeField] private Text textCritical = null;
    [SerializeField] private Text textAttackSpeed = null;
    [SerializeField] private List<Text> textPlusValueList = null;

    private CharacterData characterData;

    public void SetCharacterData(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetName(characterData.Name);
        SetImage(characterData.Image);

        SetCharacterAbilityText(DataBase.Instance.characterAbilityDataSheet.OneStarDatas[characterData.Id]);
        SetPlusValue();
        SetSynergyData();
    }

    public void SetName(string name)
    {
        if (name != null)
        {
            textName.text = name;
        }
        else
        {
            Debug.Log("No Character Name");
        }
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("No Character Image");
        }
    }

    public void OnOneStarClick()
    {
        SetCharacterAbilityText(DataBase.Instance.characterAbilityDataSheet.OneStarDatas[characterData.Id]);
    }

    public void OnTwoStarClick()
    {
        SetCharacterAbilityText(DataBase.Instance.characterAbilityDataSheet.TwoStarDatas[characterData.Id]);
    }

    public void OnThreeStarClick()
    {
        SetCharacterAbilityText(DataBase.Instance.characterAbilityDataSheet.ThreeStarDatas[characterData.Id]);
    }

    private void SetCharacterAbilityText(CharacterAbilityData characterAbilityData)
    {
        textAttack.text = characterAbilityData.Attack.ToString();
        textMagicalAttack.text = characterAbilityData.MagicalAttack.ToString();
        textHealth.text = characterAbilityData.Health.ToString();
        textDefence.text = characterAbilityData.Defence.ToString();
        textMagicDefence.text = characterAbilityData.MagicDefence.ToString();
        textShield.text = characterAbilityData.Shield.ToString();
        textAccuracy.text = characterAbilityData.Accuracy.ToString();
        textEvasion.text = characterAbilityData.Evasion.ToString();
        textCritical.text = characterAbilityData.Critical.ToString();
        textAttackSpeed.text = characterAbilityData.AttackSpeed.ToString();
    }

    private void SetPlusValue()
    {
        Rune rune = RuneManager.Instance.GetEquippedRuneOfOrigin(characterData.Origin);
        if (rune != null)
        {
            var abilityList = rune.runeData.AbilityData.GetAbilityDataList();

            for (int i = 0; i < abilityList.Count; ++i)
            {
                if (abilityList[i] == 0)
                    textPlusValueList[i].text = "";
                else
                    textPlusValueList[i].text = "+ " + abilityList[i];
            }
        }
        else
        {
            for (int i = 0; i < textPlusValueList.Count; ++i)
            {
                textPlusValueList[i].text = "";
            }
        }
    }

    private void SetSynergyData()
    {
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;
        if(tribeDataSheet == null)
        {
            Debug.Log("tribeDataSheet is null!");
            return;
        }

        var originDataSheet = DataBase.Instance.originDataSheet;
        if (originDataSheet == null)
        {
            Debug.Log("originDataSheet is null!");
            return;
        }

        if(tribeDataSheet.TryGetTribeData(characterData.Tribe, out var tribeData))
        {
            tribeImage.sprite = tribeData.Image;
            tribeText.text = SynergyService.GetNameByTribe(tribeData.Tribe);
        }

        if (originDataSheet.TryGetOriginData(characterData.Origin, out var originData))
        {
            originImage.sprite = originData.Image;
            originText.text = SynergyService.GetNameByOrigin(originData.Origin);
        }
    }
}