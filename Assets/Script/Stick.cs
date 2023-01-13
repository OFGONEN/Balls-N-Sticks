/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Stick : MonoBehaviour
{
#region Fields
	[ SerializeField ] Transform stick_side_left;
	[ SerializeField ] Transform stick_side_right;
	[ SerializeField ] TweenPunchScale stick_side_left_punchScale;
	[ SerializeField ] TweenPunchScale stick_side_right_punchScale;
	[ SerializeField ] ParticleSpawnEvent event_particle_spawn;
	[ SerializeField ] Transform stick_collider_collision;
	[ SerializeField ] Transform stick_collider_trigger;

	float CollisionLength => ( stick_length + GameSettings.Instance.stick_length_default );
	float TriggerLength   => ( stick_length + GameSettings.Instance.stick_length_default ) * GameSettings.Instance.stick_forceCollider_cofactor;

	RecycledSequence recycledSequence = new RecycledSequence();
	[ ShowInInspector, ReadOnly ] float stick_length;
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
		var particlePositionLeft  = stick_side_left.position + stick_side_left.right * -1f * stick_length / 2f;
		var particlePositionRight = stick_side_right.position + stick_side_right.right * stick_length / 2f;

		event_particle_spawn.Raise( "stick_grow", particlePositionLeft );
		event_particle_spawn.Raise( "stick_grow", particlePositionRight );

		stick_length = Mathf.Min( stick_length + delta, GameSettings.Instance.stick_length_max );
		TweenUpdateStick();
	}

	[ Button() ]
	public void OnStickLengthLost( float delta )
	{
		stick_length = Mathf.Max( stick_length - delta, 0 );

		UpdateStick();

		stick_side_left_punchScale.DoPunchScale();
		stick_side_right_punchScale.DoPunchScale();
	}
#endregion

#region Implementation
	void TweenUpdateStick()
	{
		recycledSequence.Kill();

		var scale = stick_side_left.localScale;

		var sequence = recycledSequence.Recycle();
		sequence.Append( stick_side_left.DOScale( scale.SetX( stick_length / 2f ),
			GameSettings.Instance.stick_update_duration )
			.SetEase( GameSettings.Instance.stick_update_ease ) );
		sequence.Join( stick_side_right.DOScale( scale.SetX( stick_length / 2f ),
			GameSettings.Instance.stick_update_duration )
			.SetEase( GameSettings.Instance.stick_update_ease ) );
		sequence.Join( stick_collider_collision.DOScale( CollisionLength,
			GameSettings.Instance.stick_update_duration )
			.SetEase( Ease.Linear ) );
		sequence.Join( stick_collider_trigger.DOScale( TriggerLength,
			GameSettings.Instance.stick_update_duration )
			.SetEase( Ease.Linear ) );

		// stick_side_left.localScale  = scale.SetX( stick_length / 2f );
		// stick_side_right.localScale = scale.SetX( stick_length / 2f );
	}

	void UpdateStick()
	{
		stick_collider_collision.localScale = stick_collider_collision.localScale.SetX( CollisionLength );
		stick_collider_trigger.localScale   = stick_collider_trigger.localScale.SetX( TriggerLength );

		var scale                       = stick_side_left.localScale;
		    stick_side_left.localScale  = scale.SetX( stick_length / 2f );
		    stick_side_right.localScale = scale.SetX( stick_length / 2f );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
