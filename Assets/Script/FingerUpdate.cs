/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

[ CreateAssetMenu( fileName = "shared_finger_update", menuName = "FF/Game/Finger Update" ) ]
public class FingerUpdate : ScriptableObject
{
#region Fields
    Vector2 finger_position_start;
    Vector2 finger_position_end;

    public Vector2 Direction    => ( finger_position_end - finger_position_start ).normalized;
    public Vector2 Delta        => finger_position_end - finger_position_start;
    public float DeltaMagnitude => Mathf.Abs( ( finger_position_end - finger_position_start ).magnitude );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnFingerDown( LeanFinger finger )
    {
		finger_position_start = finger.ScreenPosition;
		finger_position_end   = finger.ScreenPosition;
	}

    public void OnFingerUpdate( LeanFinger finger )
    {
		finger_position_start = finger_position_end;
		finger_position_end   = finger.ScreenPosition;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
