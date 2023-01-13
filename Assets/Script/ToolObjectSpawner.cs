/* Created by and for usage of FF Studios (2021). */
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using FFStudio;

public class ToolObjectSpawner : MonoBehaviour
{
#region Fields
  [ Title( "Configure" ) ]
	public SpawnData[] spawn_data_array;

	Vector3 spawn_position;
	int spawn_index;
	List< GameObject > spawned_object_array = new List< GameObject >( 128 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void Spawn( int spawnIndex )
    {
		EditorSceneManager.MarkAllScenesDirty();

		spawned_object_array.Clear();
		spawn_index = spawnIndex;
		var data = spawn_data_array[ spawnIndex ];

		var spawnCount = data.spawn_count_range.ReturnRandom();
		spawn_position = transform.position.SetY( data.spawn_height_range.ReturnRandom() );

		var siblingIndex = GameObject.Find( "--- Patterns_Start ---" ).transform.GetSiblingIndex();

        for( var i = 0; i < spawnCount; i++ )
        {
			var spawnObject = PrefabUtility.InstantiatePrefab( data.spawn_object ) as GameObject;
			spawnObject.transform.position = spawn_position.OffsetX( data.spawn_lateral_offset_range.ReturnRandom() ).SetY( data.spawn_height_range.ReturnRandom() );
			spawnObject.transform.SetSiblingIndex( siblingIndex + 1 );

			spawned_object_array.Add( spawnObject );
			spawn_position = spawn_position + Vector3.forward * data.spawn_forward_offset_range.ReturnRandom();
		}
	}

    [ Button() ]
    public void SpawnAgain()
    {
		DeleteLastSpawned();
		Spawn( spawn_index );
	}

    [ Button() ]
    public void DeleteLastSpawned()
    {
		EditorSceneManager.MarkAllScenesDirty();

        for( var i = 0; i < spawned_object_array.Count; i++ )
        {
			GameObject.DestroyImmediate( spawned_object_array[ i ] );
		}

		spawned_object_array.Clear();
	}
#endregion

#region Implementation
#endregion
}

[ System.Serializable ]
public struct SpawnData
{
    public GameObject spawn_object;
    public Vector2 spawn_count_range;
    public Vector2 spawn_forward_offset_range;
    public Vector2 spawn_lateral_offset_range;
    public Vector2 spawn_height_range;
}
#endif