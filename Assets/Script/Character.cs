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
    [ SerializeField ] Transform gfx_parent_transform;
    [ SerializeField ] Rigidbody _rigidBody;
    [ SerializeField ] Animator _animator;

    UnityMessage onFixedUpdate;
    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
		onFixedUpdate = ExtensionMethods.EmptyMethod;
		onUpdate      = ExtensionMethods.EmptyMethod;
	}
    private void Awake()
    {
		onFixedUpdate = ExtensionMethods.EmptyMethod;
		onUpdate      = ExtensionMethods.EmptyMethod;
	}

    private void FixedUpdate()
    {
		onFixedUpdate();
	}

	private void Update()
	{
		onUpdate();
	}
#endregion

#region API
    //Info: EditorCall
    [ Button() ]
    public void OnLevelStart()
    {
		onFixedUpdate = Movement;
		onUpdate      = Rotate;
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

	void Rotate()
	{
		var rotation = gfx_parent_transform.localEulerAngles;
		var targetRotation = shared_finger_update.Delta.x * GameSettings.Instance.character_movement_rotate_cofactor;

		gfx_parent_transform.localEulerAngles = rotation.SetY( Mathf.Lerp( targetRotation, rotation.y, Time.deltaTime ) );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
