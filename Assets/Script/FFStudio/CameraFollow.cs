/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class CameraFollow : MonoBehaviour
    {
#region Fields
    [ Title( "Setup" ) ]
        [ SerializeField ] SharedReferenceNotifier notifier_reference_transform_target;
        [ SerializeField ] SharedReferenceNotifier notif_plinko_reference;
        [ SerializeField ] GameEvent event_camera_transition_done;

        Transform transform_target;
        Vector3 followOffset;

        UnityMessage updateMethod;
#endregion

#region Properties
#endregion

#region Unity API
        void OnDisable()
        {
			updateMethod = ExtensionMethods.EmptyMethod;
		}

        void Awake()
        {
            updateMethod = ExtensionMethods.EmptyMethod;
        }

        void Start()
        {
            transform_target = notifier_reference_transform_target.SharedValue as Transform;
			ResetCameraPositionAndRotation( transform_target );
		}

        void FixedUpdate()
        {
            updateMethod();
        }
#endregion

#region API
        public void LevelRevealedResponse()
        {
            updateMethod = FollowTarget;
        }

        public void OnFinishLineResponse()
        {
			updateMethod = ExtensionMethods.EmptyMethod;

			var plinkoPosition = ( notif_plinko_reference.sharedValue as Transform ).position;

			transform.DOMove( plinkoPosition + GameSettings.Instance.camera_endLevel_offset,
				GameSettings.Instance.camera_endLevel_transition_duration )
				.SetEase( GameSettings.Instance.camera_endLevel_transition_ease )
				.SetDelay( GameSettings.Instance.camera_endLevel_transition_delay )
				.OnComplete( event_camera_transition_done.Raise );

			transform.DORotate( GameSettings.Instance.camera_endLevel_rotation,
				GameSettings.Instance.camera_endLevel_transition_duration )
				.SetEase( GameSettings.Instance.camera_endLevel_transition_ease )
				.SetDelay( GameSettings.Instance.camera_endLevel_transition_delay );
		}
#endregion

#region Implementation
        void FollowTarget()
        {
			// Info: Simple follow logic.
			var position = transform.position;
			var targetPosition = transform_target.position + GameSettings.Instance.camera_follow_offset_position;

			transform.position = position
				.SetX( Mathf.Lerp( position.x, targetPosition.x, Time.fixedDeltaTime * GameSettings.Instance.camera_follow_speed_depth ) )
				.SetY( targetPosition.y )
				.SetZ( Mathf.Lerp( position.z, targetPosition.z, Time.fixedDeltaTime * GameSettings.Instance.camera_follow_speed_lateral ) );
		}

        void ResetCameraPositionAndRotation( Transform target )
        {
			transform.position    = target.position + GameSettings.Instance.camera_follow_offset_position;
			transform.eulerAngles = GameSettings.Instance.camera_follow_offset_rotation;
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
        [ Button() ]
        void ResetCameraPositionAndRotation()
        {
			UnityEditor.EditorUtility.SetDirty( gameObject );
			ResetCameraPositionAndRotation( GameObject.FindGameObjectWithTag( "Player" ).transform );
		}

        [ Button() ]
        void SaveCameraPositionAndRotation()
        {
			UnityEditor.EditorUtility.SetDirty( gameObject );
			UnityEditor.EditorUtility.SetDirty( GameSettings.Instance );
			var targetPosition = GameObject.FindGameObjectWithTag( "Player" ).transform.position;

			GameSettings.Instance.camera_follow_offset_position = transform.position - targetPosition;
			GameSettings.Instance.camera_follow_offset_rotation = transform.eulerAngles;
		}
#endif
#endregion
    }
}