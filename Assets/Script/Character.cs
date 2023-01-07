/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] FingerUpdate shared_finger_update; 

  [ Title( "Components" ) ]
    [ SerializeField ] Animator _animator;

    UnityMessage onFixedUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
		onFixedUpdate = ExtensionMethods.EmptyMethod;
	}
    private void Awake()
    {
		onFixedUpdate = ExtensionMethods.EmptyMethod;
	}

    private void FixedUpdate()
    {
		onFixedUpdate();
	}
#endregion

#region API
    //Info: EditorCall
    [ Button() ]
    public void OnLevelStart()
    {
		onFixedUpdate = Movement;
		_animator.SetTrigger( "run" );
	}
#endregion

#region Implementation
    void Movement()
    {
		var     position       = transform.position;
		Vector3 targetPosition = position;

		targetPosition.z += GameSettings.Instance.game_forward.z; // Forward
		targetPosition.x += shared_finger_update.Delta.x;

		position.x = Mathf.Lerp( position.x, targetPosition.x, Time.fixedDeltaTime * GameSettings.Instance.character_movement_lateral_speed );
		position.z = Mathf.Lerp( position.z, targetPosition.z, Time.fixedDeltaTime * GameSettings.Instance.character_movement_forward_speed );

		position.x = Mathf.Clamp( position.x,
			GameSettings.Instance.character_movement_lateral_clamp.x,
			GameSettings.Instance.character_movement_lateral_clamp.y );

		transform.position = position;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
