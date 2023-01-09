/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TweenPunchScale : MonoBehaviour
{
#region Fields
    [ SerializeField ] Transform punch_target;
    [ SerializeField ] float punch_power;
    [ SerializeField ] Vector3 punch_direction;
    [ SerializeField ] float punch_duration;
    [ SerializeField ] Ease punch_ease;
    [ SerializeField, LabelText( "Can Tween Be Interrupted" ) ] bool punch_isInterruptable;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
		recycledTween.Kill();
	}
#endregion

#region API
    public void DoPunchScale()
    {
        if( recycledTween.IsPlaying() && !punch_isInterruptable ) return;

		recycledTween.Recycle( punch_target.DOPunchScale(
			punch_direction * punch_power,
			punch_duration )
			.SetEase( punch_ease )
        );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}