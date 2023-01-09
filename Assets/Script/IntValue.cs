/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntValue : MonoBehaviour
{
#region Fields
    [ SerializeField ] int value;
    [ SerializeField ] UnityEvent< int > onUpdateUnityEvent;
    [ SerializeField ] UnityEvent< int > onComparisionUnityEvent;
    int value_current;
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		value_current = value;
	}
#endregion

#region API
    public void Add( int addition )
    {
		value_current += addition;
		onUpdateUnityEvent.Invoke( value_current );
	}

    public void Substract( int substract )
    {
		value_current -= substract;
		onUpdateUnityEvent.Invoke( value_current );
	}

	public void CheckIfEqual( int value )
	{
		if( value_current == value )
			onComparisionUnityEvent.Invoke( value_current );
	}

	public void CheckIfMore( int value )
	{
		if( value > value_current )
			onComparisionUnityEvent.Invoke( value_current );
	}

	public void CheckIfEqualOrMore( int value )
	{
		if( value >= value_current )
			onComparisionUnityEvent.Invoke( value_current );
	}

	public void ForceUpdate()
	{
		onUpdateUnityEvent.Invoke( value_current );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnValidate()
	{
		ForceUpdate();
	}
#endif
#endregion
}