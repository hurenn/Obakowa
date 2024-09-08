using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    /// <summary>
    /// AI�̈ړ��惊�X�g
    /// </summary>
    [SerializeField]
    private Transform[] _ai_pos_list = null;

    /// <summary>
    /// ���̈ړ���
    /// </summary>
    private Vector3 _next_position = new Vector3(0, 0, 0);

    /// <summary>
    /// AI�ړ��@�\
    /// </summary>
    private NavMeshAgent _agent;

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

    public void Start()
    {
        _Setup();
    }

    /// <summary>
    /// �Z�b�g�A�b�v
    /// </summary>
    private void _Setup()
    {
        _agent = _character.GetComponent<NavMeshAgent>();
        _agent.speed = _character.velocity;
        _SetNextGoal();
    }

    // Update is called once per frame
    void Update()
    {
        _GoalUpdate();
        _ColorUpdate();
    }

    /// <summary>
    /// �F�`�F���W�`�F�b�N
    /// </summary>
    private void _ColorUpdate()
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

    /// <summary>
    /// �S�[���`�F�b�N
    /// </summary>
    private void _GoalUpdate()
    {
        if(_agent == null)
        {
            return;
        }

        // �S�[���߂��܂ŗ�����؂�ւ�
        if(_agent.remainingDistance < 0.5f)
        {
            _SetNextGoal();
        }
    }

    /// <summary>
    /// ���̈ʒu�����擾
    /// </summary>
    /// <returns></returns>
    private void _SetNextGoal()
    {
        if (_agent == null)
        {
            return;
        }
        if (_ai_pos_list == null || _ai_pos_list.Length <= 0)
        {
            Debug.Log("AI�̖ړI�n���ݒ肳��Ă��܂���");
            return;
        }

        int num = Random.Range(0, _ai_pos_list.Length);
        _next_position = _ai_pos_list[num].position;

        _agent.destination = _next_position;
    }

}
