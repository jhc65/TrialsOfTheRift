﻿/*  Rift Boss Objective - Dana Thompson
 * 
 *  Desc:   Facilitates Rift Boss Objective
 * 
 */

using System.Collections;
using UnityEngine;

public class RiftBossObjective : Objective {

#region RiftBossObjective Methods
    override protected void SetUI() {
        calligrapher.RiftBossInit(e_color);
    }

    override protected void ResetUI() {
        calligrapher.RiftBossReset(e_color);
    }

    // Update UI and check for completion
    public void UpdateRiftBossHealth(float f) {
        calligrapher.UpdateRiftBossHealthUI(e_color, f);
        if (f <= 0) {
            StartCoroutine(DelayCompleteforExplosion());
        }
		else if (f <= Constants.ObjectiveStats.C_RiftBossMaxHealth * .1f) {
            maestro.PlayTeamEncouragement();
        }
    }

    private IEnumerator DelayCompleteforExplosion() {
        yield return new WaitForSecondsRealtime(4f);
        b_isComplete = true;
    }
#endregion

#region Unity Overrides
    void OnEnable() {
        maestro.PlayBeginRiftBoss();
    }
#endregion

}