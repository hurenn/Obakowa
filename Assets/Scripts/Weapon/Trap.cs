using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : WeaponBase
{
    /// <summary>
    /// ˆÚ“®’âŽ~ŽžŠÔ
    /// </summary>
    public float stop_time = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        // ”’‰»‚µ‚½‚¨‚Î‚¯‚É“–‚½‚Á‚½Žž
        var ghost = other.gameObject.GetComponent<CharacterGhost>();
        if (ghost != null && ghost._current_status == GameDifinition.eGhostStatus.PhotographHit)
        {
            StartCoroutine("_TrapTimer", ghost);
        }
    }

    /// <summary>
    /// ˆê’èŽžŠÔ“®‚«‚ðŽ~‚ß‚é
    /// </summary>
    /// <param name="target_characer"></param>
    /// <returns></returns>
    private IEnumerator _TrapTimer(CharacterBase target_characer)
    {
        float time = 0;
        target_characer.enable_move++;

        while (time < stop_time)
        {
            time += Time.deltaTime;
            yield return null;
        }

        target_characer.enable_move--;
    }
}
