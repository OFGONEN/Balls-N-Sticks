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

	Vector3 movement_position;

	UnityMessage onFixedUpdate;
    UnityMessage onUpdate;
	UnityMessage onFingerDown;
	UnityMessage onFingerUp;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
		EmptyDelegates();
	}

    private void Awake()
    {
		EmptyDelegates();

		movement_position = transform.position;
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
		onFingerDown = FirstFingerDown;
	}

	public void OnFingerDown()
	{
		onFingerDown();
	}

	public void OnFingerUp()
	{
		onFingerUp();
	}
#endregion

#region Implementation
	void CalculateMovement()
	{
		var targetPosition    = movement_position;
		    targetPosition.z += GameSettings.Instance.game_forward.z;
		    targetPosition.x += shared_finger_update.DeltaScaled.x;

		movement_position.z = Mathf.Lerp( movement_position.z, targetPosition.z, Time.deltaTime * GameSettings.Instance.character_movement_forward_speed );
		movement_position.x = Mathf.Lerp( movement_position.x, targetPosition.x, Time.deltaTime * GameSettings.Instance.character_movement_lateral_speed );

		movement_position.x = Mathf.Clamp( movement_position.x,
			GameSettings.Instance.character_movement_lateral_clamp.x,
			GameSettings.Instance.character_movement_lateral_clamp.y );
	}

	void Movement()
	{
		_rigidBody.MovePosition( movement_position );
		// _rigidBody.MoveRotation() //todo do this now

		//! This does not work. Delta Time or Fixed Delta time. It DOES NOT WORK. Character just jumps positions.
		// var position          = _rigidBody.position;
		// var targetPosition    = _rigidBody.position;
		//     targetPosition.z += GameSettings.Instance.game_forward.z;
		//     targetPosition.x += shared_finger_update.DeltaScaled.x;

		// position.z = Mathf.Lerp( position.z, targetPosition.z, Time.deltaTime * GameSettings.Instance.character_movement_forward_speed );
		// position.x = Mathf.Lerp( position.x, targetPosition.x, Time.deltaTime * GameSettings.Instance.character_movement_lateral_speed );

		// position.x = Mathf.Clamp( position.x,
		// 	GameSettings.Instance.character_movement_lateral_clamp.x,
		// 	GameSettings.Instance.character_movement_lateral_clamp.y );

		// _rigidBody.MovePosition( position );
	}

	void FirstFingerDown()
	{
		_animator.SetTrigger( "run" );
		onUpdate      = CalculateMovement;
		onFixedUpdate = Movement;

		onFingerDown = ExtensionMethods.EmptyMethod;
	}

	void EmptyDelegates()
	{
		onFingerDown  = ExtensionMethods.EmptyMethod;
		onFingerUp    = ExtensionMethods.EmptyMethod;
		onUpdate      = ExtensionMethods.EmptyMethod;
		onFixedUpdate = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere( movement_position, 0.1f );
	}
#endif
#endregion
}
