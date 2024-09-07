using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 特殊アクションフォーマット
    /// </summary>
    private readonly string _ACTION_FORMAT = "特殊アクション：{0}";

    /// <summary>
    /// 対象キャラクター
    /// </summary>
    [SerializeField]
    private CharacterBase _character;

    /// <summary>
    /// 色表示
    /// </summary>
    [SerializeField]
    private Image _color_image;

    /// <summary>
    /// 武器テキスト
    /// </summary>
    [SerializeField]
    private TMP_Text _weapon_text;

    public void Start()
    {
        // NOTE:ゲーム管理が行うのが望ましい
        Setup();
    }

    public void Setup()
    {
        if (_character != null)
        {
            _character.color_change_action += SetColor;

            // NOTE:ゲーム管理が行うのが望ましい
            _character.currentColor = GameDifinition.eColor.Cyan;
        }
    }

    /// <summary>
    /// アクションチェンジ
    /// </summary>
    /// <param name="set_weapon"></param>
    public void ChangeAction(GameDifinition.eAction set_weapon)
    {
        switch (set_weapon)
        {
            case GameDifinition.eAction.None:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "なし");
                break;
            case GameDifinition.eAction.Rope:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "罠");
                break;
            case GameDifinition.eAction.Camera:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "カメラ");
                break;
            case GameDifinition.eAction.Poltergeist:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "ポルターガイスト");
                break;
        }
    }

    /// <summary>
    /// 色チェンジ
    /// </summary>
    /// <param name="set_color"></param>
    public void SetColor(GameDifinition.eColor set_color)
    {
        switch (set_color)
        {
            case GameDifinition.eColor.Cyan:
                _color_image.color = Color.cyan;
                break;
            case GameDifinition.eColor.Magenta:
                _color_image.color = Color.magenta;
                break;
            case GameDifinition.eColor.Yellow:
                _color_image.color = Color.yellow;
                break;
        }
    }
}
