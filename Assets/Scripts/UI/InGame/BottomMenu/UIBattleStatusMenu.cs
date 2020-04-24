﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleStatusMenu : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button reloadButton = null;
   
    [SerializeField] private UICharacterPurchase characterPurchase = null;

    public Timer timer;

    void Start()
    {
        reloadButton.onClick.AddListener(() => {
            characterPurchase.Shuffle();
        });

        startButton.onClick.AddListener(() => {
            timer.TimeOut();
        });
    }
}