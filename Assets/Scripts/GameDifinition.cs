using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDifinition
{
    public static string SHOOT_PATH = "Prefab/Weapon/Shoot";
    public static string PHOTOGRAPH_PATH = "Prefab/Weapon/Photograph";

    public enum eColor
    {
        White,          // 白
        Magenta,        // 赤
        Cyan,           // 青
        Yellow,         // 黄
    }

    public enum eAction
    {
        None,
        Rope,           // ロープ
        Camera,         // カメラ
        Poltergeist,    // ポルターガイスト
    }

    /// <summary>
    /// おばけのやられ状態
    /// </summary>
    public enum eGhostStatus
    {
        ShootHit,
        TrapHit,
        PhotographHit,
        Failed,
    }

    /// <summary>
    /// 次の色を返す
    /// </summary>
    /// <param name="current_color"></param>
    /// <returns>eColorで返却</returns>
    public static eColor GetChangeColor(eColor current_color)
    {
        switch (current_color)
        {
            case eColor.Cyan:
                return eColor.Magenta;
            case eColor.Magenta:
                return eColor.Yellow;
            case eColor.Yellow:
                return eColor.Cyan;
        }

        return eColor.White;
    }

    /// <summary>
    /// 現在の色をRGBで返す
    /// </summary>
    /// <param name="current_color"></param>
    /// <returns>Colorで返却</returns>
    public static Color GetRGBColor(eColor current_color)
    {
        switch (current_color)
        {
            case eColor.Cyan:
                return Color.cyan;
            case eColor.Magenta:
                return Color.magenta;
            case eColor.Yellow:
                return Color.yellow;
        }

        return Color.white;
    }
}
