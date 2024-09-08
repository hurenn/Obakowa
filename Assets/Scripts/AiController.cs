using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    /// <summary>
    /// AIの移動先リスト
    /// </summary>
    [SerializeField]
    private Transform[] _ai_pos_list = null;

    /// <summary>
    /// 次の移動先
    /// </summary>
    private Vector3 _next_position = new Vector3(0, 0, 0);

    /// <summary>
    /// AI移動機能
    /// </summary>
    private NavMeshAgent _agent;

    /// <summary>
    /// 色を変える時間
    /// </summary>
    [SerializeField]
    float _color_change_time = 3.0f;

    /// <summary>
    /// 経過時間
    /// </summary>
    float _current_timer = 0;

    /// <summary>
    /// 操作対象キャラクター
    /// </summary>
    [SerializeField]
    private CharacterBase _character;

    public void Start()
    {
        _Setup();
    }

    /// <summary>
    /// セットアップ
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
    /// 色チェンジチェック
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
    /// ゴールチェック
    /// </summary>
    private void _GoalUpdate()
    {
        if(_agent == null)
        {
            return;
        }

        // ゴール近くまで来たら切り替え
        if(_agent.remainingDistance < 0.5f)
        {
            _SetNextGoal();
        }
    }

    /// <summary>
    /// 次の位置情報を取得
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
            Debug.Log("AIの目的地が設定されていません");
            return;
        }

        int num = Random.Range(0, _ai_pos_list.Length);
        _next_position = _ai_pos_list[num].position;

        _agent.destination = _next_position;
    }

}
