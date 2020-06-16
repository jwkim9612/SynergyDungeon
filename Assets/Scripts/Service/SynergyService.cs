﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyService
{
    public static string GetNameByOrigin(Origin origin)
    {
        string name = "";

        switch (origin)
        {
            case Origin.None:
                name = "";
                break;
            case Origin.Archer:
                name = "궁수";
                break;
            case Origin.Paladin:
                name = "성기사";
                break;
            case Origin.Thief:
                name = "도적";
                break;
            case Origin.Warrior:
                name = "전사";
                break;
            case Origin.Wizard:
                name = "마법사";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }

    public static string GetNameByTribe(Tribe tribe)
    {
        string name = "";

        switch (tribe)
        {
            case Tribe.None:
                name = "";
                break;
            case Tribe.Beast:
                name = "야수";
                break;
            case Tribe.Devil:
                name = "악마";
                break;
            case Tribe.Dragon:
                name = "용";
                break;
            case Tribe.Elemental:
                name = "정령";
                break;
            case Tribe.Elf:
                name = "엘프";
                break;
            case Tribe.Human:
                name = "인간";
                break;
            case Tribe.Machine:
                name = "기계";
                break;
            case Tribe.Undead:
                name = "언데드";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }
}
