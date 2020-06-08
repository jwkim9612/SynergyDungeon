﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDiamond : MonoBehaviour
{
    [SerializeField] private Text diamondText = null;

    public void Initialize()
    {
        PlayerDataManager.Instance.OnDiamondChanged += UpdateDiamondText;
        UpdateDiamondText();
    }

    public void UpdateDiamondText()
    {
        diamondText.text = string.Format("{0}", PlayerDataManager.Instance.playerData.Diamond.ToString("#,##0"));
    }
}
