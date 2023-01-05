/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class SharedDataLibrary< T > : ScriptableObject
{
    [ SerializeField, LabelText( "Data Array " ) ] T[] data_array;

	public int DataCount => data_array.Length;

	public T GetData( int index )
    {
		return data_array[ index ];
	}
}