﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class InGameSaveData
{
    public int Chapter { get; set; }
    public int Wave { get; set; }
    public int Coin { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public List<CharacterInfo> CharacterAreaInfoList { get; set; }
    public List<CharacterInfo> PrepareAreaInfoList { get; set; }

    //public void SetInGameData(List<Character> characters, List<Enemy> enemies)
    //{
    //    this.Characters = characters;
    //    this.Enemies = enemies;
    //}
}
