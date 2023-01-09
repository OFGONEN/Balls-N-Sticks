/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Stick : MonoBehaviour
{
#region Fields
	[ SerializeField ] Transform stick_side_left;
	[ SerializeField ] Transform stick_side_right;

	float stick_length;
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		stick_length = CurrentLevelData.Instance.levelData.stick_length_start;

		UpdateStick();
	}
#endregion

#region API
	[ Button() ]
	public void OnStickLengthGained( float delta )
	{
		stick_length = Mathf.Min( stick_length + delta, GameSettings.Instance.stick_length_max );
		UpdateStick();
	}

	[ Button() ]
	public void OnStickLengthLost( float delta )
	{
		stick_length -= delta;
		// Spawn Little Sticks
		UpdateStick();
	}
#endregion

#region Implementation
	void UpdateStick()
	{
		var scale = stick_side_left.localScale;

		stick_side_left.localScale  = scale.SetX( stick_length / 2f );
		stick_side_right.localScale = scale.SetX( stick_length / 2f );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}