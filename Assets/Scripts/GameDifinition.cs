using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDifinition
{
    public static string SHOOT_PATH = "Prefab/Weapon/Shoot";
    public static string PHOTOGRAPH_PATH = "Prefab/Weapon/Photograph";

    public enum eColor
    {
        White,          // ��
        Magenta,        // ��
        Cyan,           // ��
        Yellow,         // ��
    }

    public enum eAction
    {
        None,
        Rope,           // ���[�v
        Camera,         // �J����
        Poltergeist,    // �|���^�[�K�C�X�g
    }

    /// <summary>
    /// ���΂��̂�����
    /// </summary>
    public enum eGhostStatus
    {
        ShootHit,
        TrapHit,
        PhotographHit,
        Failed,
    }

    /// <summary>
    /// ���̐F��Ԃ�
    /// </summary>
    /// <param name="current_color"></param>
    /// <returns>eColor�ŕԋp</returns>
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
    /// ���݂̐F��RGB�ŕԂ�
    /// </summary>
    /// <param name="current_color"></param>
    /// <returns>Color�ŕԋp</returns>
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
