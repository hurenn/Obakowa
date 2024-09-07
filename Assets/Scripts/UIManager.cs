using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// ����A�N�V�����t�H�[�}�b�g
    /// </summary>
    private readonly string _ACTION_FORMAT = "����A�N�V�����F{0}";

    /// <summary>
    /// �ΏۃL�����N�^�[
    /// </summary>
    [SerializeField]
    private CharacterBase _character;

    /// <summary>
    /// �F�\��
    /// </summary>
    [SerializeField]
    private Image _color_image;

    /// <summary>
    /// ����e�L�X�g
    /// </summary>
    [SerializeField]
    private TMP_Text _weapon_text;

    public void Start()
    {
        // NOTE:�Q�[���Ǘ����s���̂��]�܂���
        Setup();
    }

    public void Setup()
    {
        if (_character != null)
        {
            _character.color_change_action += SetColor;

            // NOTE:�Q�[���Ǘ����s���̂��]�܂���
            _character.currentColor = GameDifinition.eColor.Cyan;
        }
    }

    /// <summary>
    /// �A�N�V�����`�F���W
    /// </summary>
    /// <param name="set_weapon"></param>
    public void ChangeAction(GameDifinition.eAction set_weapon)
    {
        switch (set_weapon)
        {
            case GameDifinition.eAction.None:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "�Ȃ�");
                break;
            case GameDifinition.eAction.Rope:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "�");
                break;
            case GameDifinition.eAction.Camera:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "�J����");
                break;
            case GameDifinition.eAction.Poltergeist:
                _weapon_text.text = string.Format(_ACTION_FORMAT, "�|���^�[�K�C�X�g");
                break;
        }
    }

    /// <summary>
    /// �F�`�F���W
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
