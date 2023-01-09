/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


public class RelayIKPass : MonoBehaviour
{
#region Fields
    [ SerializeField, LabelText( "On IK Pass Event" ) ] UnityEvent< int > onIKPass;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    void OnAnimatorIK( int layerIndex )
    {
		onIKPass.Invoke( layerIndex );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
