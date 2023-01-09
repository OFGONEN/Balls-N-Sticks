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
  [ Title( "Shared" ) ]
    [ SerializeField ] FingerUpdate shared_finger_update; 
    [ SerializeField ] SharedReferenceNotifier notif_stick_left; 
    [ SerializeField ] SharedReferenceNotifier notif_stick_right; 

  [ Title( "Components" ) ]
    [ SerializeField ] Transform gfx_parent_transform;
    [ SerializeField ] Rigidbody _rigidBody;
    [ SerializeField ] Animator _animator;

	Transform stick_left_transform;
	Transform stick_right_transform;

	Vector3 character_position;
	float character_rotation;

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

		character_position = transform.position;
	}

	private void Start()
	{
		stick_left_transform  = notif_stick_left.sharedValue as Transform;
		stick_right_transform = notif_stick_right.sharedValue as Transform;
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

	public void OnAnimatorIKUpdate( int layer )
	{
		_animator.SetIKPositionWeight( AvatarIKGoal.LeftHand, 1 );
		_animator.SetIKPositionWeight( AvatarIKGoal.RightHand, 1 );

		_animator.SetIKPosition( AvatarIKGoal.LeftHand, stick_left_transform.position );
		_animator.SetIKPosition( AvatarIKGoal.RightHand, stick_right_transform.position );
	}
#endregion

#region Implementation
	void CalculateMovement()
	{
		character_position.x += shared_finger_update.DeltaScaled.x * GameSettings.Instance.character_movement_lateral_speed * GameSettings.Instance.character_movement_lateral_cofactor;
		character_position.z += Time.deltaTime * GameSettings.Instance.character_movement_forward_speed;

		character_position.x = Mathf.Clamp( character_position.x,
			GameSettings.Instance.character_movement_lateral_clamp.x,
			GameSettings.Instance.character_movement_lateral_clamp.y );

		var targetRotation = character_rotation;
		targetRotation += shared_finger_update.DeltaScaled.x * GameSettings.Instance.character_movement_rotate_cofactor;

		targetRotation = Mathf.Sign( targetRotation ) * Mathf.Max( 0, Mathf.Abs( targetRotation ) - GameSettings.Instance.character_movement_rotate_deceleration );

		character_rotation = Mathf.Clamp( 
				Mathf.Lerp( character_rotation, targetRotation, Time.deltaTime * GameSettings.Instance.character_movement_rotate_speed ),
			GameSettings.Instance.character_movement_rotate_clamp.x,
			GameSettings.Instance.character_movement_rotate_clamp.y );
	}

	void Movement()
	{
		_rigidBody.MovePosition( character_position );
		_rigidBody.MoveRotation( Quaternion.Euler( 0, character_rotation, 0 ) ); //todo do this now

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
		Gizmos.DrawWireSphere( character_position, 0.1f );
	}
#endif
#endregion
}
