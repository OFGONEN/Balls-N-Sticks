/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEditor;

public class Character : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] FingerUpdate shared_finger_update; 
    [ SerializeField ] float percentageCofactor = 100f; 

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidBody;
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
		var position       = _rigidBody.position;
		var targetPosition = _rigidBody.position;

		targetPosition.z = Mathf.Lerp( position.z, position.z + GameSettings.Instance.game_forward.z, Time.fixedDeltaTime * GameSettings.Instance.character_movement_forward_speed );
		targetPosition.x = Mathf.Lerp( position.x, position.x + shared_finger_update.Delta.x, GameSettings.Instance.character_movement_lateral_speed * Time.fixedDeltaTime);

		targetPosition.x = Mathf.Clamp( targetPosition.x,
			GameSettings.Instance.character_movement_lateral_clamp.x,
			GameSettings.Instance.character_movement_lateral_clamp.y );

		_rigidBody.MovePosition( targetPosition );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
