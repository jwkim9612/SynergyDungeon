﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterList : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup girdLayoutGroup = null;
    [SerializeField] private UICharacterSlot characterSlot = null;

    //private List<UICharacterSlot> characterSlots = new List<UICharacterSlot>();
    private List<UICharacterSlot> characterSlots { get; set; }

    public Tier currentTier { get; set; } = Tier.None;
    public Tribe currentTribe { get; set; } = Tribe.None;
    public Origin currentOrigin { get; set; } = Origin.None;

    void Start()
    {
        CreateCharacterList();
        Destroy(characterSlot.gameObject);
    }

    private void CreateCharacterList()
    {
        characterSlots = new List<UICharacterSlot>();

        var characterDatas = GameManager.instance.dataSheet.characterDataSheet.characterDatas;
        foreach (var characterData in characterDatas)
        {
            var slot = Instantiate(characterSlot, girdLayoutGroup.transform);
            slot.SetCharacterData(characterData);
            characterSlots.Add(slot);
        }
    }

    public void Sort()
    {
        foreach (var characterSlot in characterSlots)
        {
            // 현재 정렬값이 캐릭터의 정렬값과 같지 않거나, 모든(None)값이 아니면
            if (!(characterSlot.characterData.Tier == currentTier || Tier.None == currentTier))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            if (!(characterSlot.characterData.Tribe == currentTribe || Tribe.None == currentTribe))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            if (!(characterSlot.characterData.Origin == currentOrigin || Origin.None == currentOrigin))
            {
                characterSlot.gameObject.SetActive(false);
                continue;
            }

            characterSlot.gameObject.SetActive(true);
        }
    }
}
