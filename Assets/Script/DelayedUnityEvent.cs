/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;
using DG.Tweening;

public class DelayedUnityEvent : MonoBehaviour
{
#region Fields
    [ SerializeField ] UnityEvent _unityEvent;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void InvokeUnityEvent()
    {
		_unityEvent.Invoke();
	}

	public void InvokeWithDelayUnityEvent( float delay )
	{
		recycledTween.Kill();
		recycledTween.Recycle( DOVirtual.DelayedCall( delay, _unityEvent.Invoke ) );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
