﻿using GameSparks.Api.Requests;
using GameSparks.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private AskInGameContinue askInGameContinue = null;

   // private string playDataPath;
    private InGameSaveData _inGameSaveData;
    public InGameSaveData inGameSaveData { get { return _inGameSaveData; } }
    public bool IsLoadedData { get; set; }
    public List<int> equippedRuneIdsSaveData;
    string equippedRuneIdsSaveDataPath;

    public void Initialize()
    {
        _inGameSaveData = new InGameSaveData();

        equippedRuneIdsSaveDataPath = Path.Combine(Application.dataPath, "EquippedRuneIds.json");
        IntializeEquippedRuneIdsSaveData();

        //if (!File.Exists(playDataPath))
        //{
        //    //SaveSPSData();
        //}
        //else
        //{
        //    //LoadSPSData();
        //}
    }

    private void IntializeEquippedRuneIdsSaveData()
    {
        equippedRuneIdsSaveData = new List<int>(RuneService.NUMBER_OF_RUNE_SOCKETS);
        
        for(int i = 0; i < RuneService.NUMBER_OF_RUNE_SOCKETS; ++i)
        {
            equippedRuneIdsSaveData.Add(-1);
        }

        bool result = LoadEquippedRuneIdsSaveData();
        if (!result)
            SaveEquippedRuneIds();
    }

    public void SaveEquippedRuneIds()
    {
        JsonDataManager.Instance.CreateJsonFile(Application.dataPath, "EquippedRuneIds", JsonDataManager.Instance.ObjectToJson(equippedRuneIdsSaveData));
        Debug.Log("EquippedRuneIds Save Done !");
    }

    public void SetEquippedRuneIdsSaveData(int socketId, int runeId)
    {
        equippedRuneIdsSaveData[socketId] = runeId;
    }

    public void SetEquippedRuneIdsSaveDataByRelease(int socketId)
    {
        equippedRuneIdsSaveData[socketId] = -1;
    }

    public void SetInGameData()
    {
        _inGameSaveData.Coin = InGameManager.instance.playerState.coin;
        _inGameSaveData.Level = InGameManager.instance.playerState.level ;
        _inGameSaveData.Exp = InGameManager.instance.playerState.exp;
        _inGameSaveData.Chapter = StageManager.Instance.currentChapter;
        _inGameSaveData.Wave = StageManager.Instance.currentWave + 1;
        _inGameSaveData.CharacterAreaInfoList = InGameManager.instance.draggableCentral.uiCharacterArea.GetAllCharacterInfo();
        _inGameSaveData.PrepareAreaInfoList = InGameManager.instance.draggableCentral.uiPrepareArea.GetAllCharacterInfo();
    }

    public bool LoadEquippedRuneIdsSaveData()
    {
        //파일이 없으면
        if (!File.Exists(equippedRuneIdsSaveDataPath))
        {
            Debug.Log(equippedRuneIdsSaveDataPath);
            return false;
        }

        FileStream fileStream = new FileStream(string.Format(equippedRuneIdsSaveDataPath), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        Debug.Log("!! Load");
        string jsonData = Encoding.UTF8.GetString(data);
        equippedRuneIdsSaveData = JsonConvert.DeserializeObject<List<int>>(jsonData);
        return true;
    }

    

    /// <summary>
    /// 인게임 데이터 삭제
    /// </summary>
    /// <returns> 성공 여부 </returns>
    //public bool DeleteInGameData()
    //{
    //    if (HasInGameData())
    //    {
    //        File.Delete(playDataPath);
    //        return true;
    //    }

    //    return false;
    //}

    // 최초 게임 진입 시에 초기 값으로 해서 저장
    // 이후에는 관련된 프로퍼티 변경할 때마다 저장
    public void SaveInGameData()
    {
        new LogEventRequest()
            .SetEventKey("SaveInGameData")
            .SetEventAttribute("Chapter", _inGameSaveData.Chapter)
            .SetEventAttribute("Wave", _inGameSaveData.Wave)
            .SetEventAttribute("Coin", _inGameSaveData.Coin)
            .SetEventAttribute("Level", _inGameSaveData.Level)
            .SetEventAttribute("Exp", _inGameSaveData.Exp)
            .SetEventAttribute("CharacterAreaInfoList", JsonDataManager.Instance.ObjectToJson(_inGameSaveData.CharacterAreaInfoList))
            .SetEventAttribute("PrepareAreaInfoList", JsonDataManager.Instance.ObjectToJson(_inGameSaveData.PrepareAreaInfoList))
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success InGame Data Save !");
                }
                else
                {
                    Debug.Log("Error Data Save !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void RemoveInGameData()
    {
        new LogEventRequest()
            .SetEventKey("RemoveInGameData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success InGame Data Remove !");
                }
                else
                {
                    Debug.Log("Error Data Remove !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void LoadInGameData()
    {
        new LogEventRequest()
            .SetEventKey("LoadInGameData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    GSData scriptData = response.ScriptData.GetGSData("InGameData");

                    var data = new InGameSaveData
                    {

                        Chapter = (int)scriptData.GetInt("InGameChapter"),
                        Wave = (int)scriptData.GetInt("InGameWave"),
                        Coin = (int)scriptData.GetInt("InGameCoin"),
                        Level = (int)scriptData.GetInt("InGameLevel"),
                        Exp = (int)scriptData.GetInt("InGameExp"),
                        CharacterAreaInfoList = JsonConvert.DeserializeObject<List<CharacterInfo>>(scriptData.GetString("InGameCharacterAreaInfoList")),
                        PrepareAreaInfoList = JsonConvert.DeserializeObject<List<CharacterInfo>>(scriptData.GetString("InGamePrepareAreaInfoList"))
                    };

                    _inGameSaveData = data;
                    Debug.Log("Player Data Load Successfully !");
                    Debug.Log($"Chapter : {_inGameSaveData.Chapter}, Wave : {_inGameSaveData.Wave}, Coin : {_inGameSaveData.Coin}, Level : {_inGameSaveData.Level}, Exp : {_inGameSaveData.Exp}");

                    IsLoadedData = true;

                    StageManager.Instance.SetChapterData(data.Chapter);
                    StageManager.Instance.currentWave = _inGameSaveData.Wave;
                    SceneManager.LoadScene("InGame");
                }
                else
                {
                    Debug.Log("Error Player Data Load");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void CheckHasInGameData()
    {
        new LogEventRequest()
           .SetEventKey("HasInGameData")
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       askInGameContinue.gameObject.SetActive(true);
                   }
                   else
                   {
                       IsLoadedData = false;
                       //SceneManager.LoadScene("MainScene");
                       StartCoroutine(Co_Test());
                   }
               }
               else
               {
                   Debug.Log("Error CheckHasInGameData");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    IEnumerator Co_Test()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainScene");
    }
}
