using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    /// <summary>
    /// �F��ς��鎞��
    /// </summary>
    [SerializeField]
    float _color_change_time = 3.0f;

    /// <summary>
    /// �o�ߎ���
    /// </summary>
    float _current_timer = 0;

    /// <summary>
    /// ����ΏۃL�����N�^�[
    /// </summary>
    [SerializeField]
    private CharacterBase _character;

    // Update is called once per frame
    void Update()
    {
        if (!_character.enableColorChange)
        {
            _current_timer = 0;
            return;
        }

        _current_timer += Time.deltaTime;
        if(_current_timer > _color_change_time)
        {
            _current_timer = 0;
            _character.ChangeColor(true);
        }
    }

}
