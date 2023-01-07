/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;
using Sirenix.OdinInspector;

public class DistanceChecker : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedReferenceNotifier notif_transform_reference;
    [ SerializeField ] float transform_distance;
    [ SerializeField ] UnityEvent onReferenceDistance;

    Transform _transform;

    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnDisable()
    {
		onUpdate = ExtensionMethods.EmptyMethod;
	}

    private void Awake()
    {
		onUpdate = ExtensionMethods.EmptyMethod;
	}

    private void Update()
    {
		onUpdate();
	}
#endregion

#region API
    public void CacheReference()
    {
		_transform = notif_transform_reference.sharedValue as Transform;
    }

    public void StartDistanceChecking()
    {
		onUpdate = ExtensionMethods.EmptyMethod;
		enabled  = true;
	}

    public void StopDistanceChecking()
    {
		onUpdate = ExtensionMethods.EmptyMethod;
		enabled  = false;
	}
#endregion

#region Implementation
    void DistanceCheck()
    {
        if( Vector3.Distance( transform.position, _transform.position ) <= transform_distance )
			onReferenceDistance.Invoke();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
