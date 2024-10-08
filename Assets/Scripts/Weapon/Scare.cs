using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : MonoBehaviour
{
    /// <summary>
    /// 脅かし当たり判定
    /// </summary>
    [SerializeField]
    Collider _scared_collidion;

    private void OnTriggerEnter(Collider other)
    {
        var kid = other.GetComponent<CharacterKid>();
        if (kid)
        {
            var ghost = GetComponentInParent<CharacterGhost>();
            ghost.StartCoroutine("PlayAlphaAnimation", GameDifinition.eColor.Yellow);

            kid.Failed();
        }
    }
}
