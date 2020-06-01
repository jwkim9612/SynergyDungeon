﻿using GameSparks.Api.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoSingleton<GoodsManager>
{
    [SerializeField] private GameObject beingBuy;
    [SerializeField] private UIAskGoToStore uiAskGoToStore;
    [SerializeField] private UIFloatingText buyCompletedFloatingText;
    public UIStore uiStore;

    private int rewardAmount;
    private int rewardId;

    private void Start()
    {
        buyCompletedFloatingText.Initialize();
    }

    public void BuyingGoods(int id)
    {
        ShowBeingBuy(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("BuyingGoods")
           .SetEventAttribute("ItemId", id)
           .Send((response) =>
           {
               HideBeginBuy(); // 구매중 팝업 없애기.

               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrencyType"));
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));
                       rewardId = (int)(response.ScriptData.GetInt("RewardId"));

                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       Debug.Log("test = " + rewardCurrency);
                       Debug.Log("타입 = " + strRewardCurrency);
                       Debug.Log("수량 = " + rewardAmount);
                       Debug.Log("id = " + rewardId);

                       Debug.Log("구매 완료");

                       GetItems(rewardCurrency);
                       buyCompletedFloatingText.Play(); // 구매 완료! 띄우기
                   }
                   else
                   {
                       string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                       PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);


                       uiAskGoToStore.SetText(purchaseCurrency);
                       UIManager.Instance.ShowNew(uiAskGoToStore);
                       // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                   }
               }
               else
               {
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    // 구매한 아이템을 플레이어 인벤토리에 넣어주는 함수
    private void GetItems(RewardCurrency rewardCurrency)
    {
        switch(rewardCurrency)
        {
            case RewardCurrency.Gold:
                PlayerDataManager.Instance.LoadPlayerData();
                break;
            case RewardCurrency.Rune:
                Debug.Log("룬 구입!!");
                RuneManager.Instance.AddRune(rewardId);
                break;

        }
    }

    private void ShowBeingBuy()
    {
        beingBuy.SetActive(true);
    }

    private void HideBeginBuy()
    {
        beingBuy.SetActive(false);
    }
}
