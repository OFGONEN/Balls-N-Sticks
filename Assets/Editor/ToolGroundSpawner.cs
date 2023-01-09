/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[ CreateAssetMenu( fileName = "tool_ground_spawner", menuName = "FF/Game/Tool/Ground Spawn" ) ]
public class ToolGroundSpawner : ScriptableObject
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] string level_code;
	
	[ FoldoutGroup( "Configure" ), SerializeField ] GameObject prefab_ground;
	[ FoldoutGroup( "Configure" ), SerializeField ] GameObject prefab_divider;
	[ FoldoutGroup( "Configure" ), SerializeField ] GameObject prefab_end;
	[ FoldoutGroup( "Configure" ), SerializeField ] float prefab_ground_length;
    List< GameObject > environemt_gameObject_list = new List< GameObject >();

	int spawn_index_start;
	int spawn_count;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	[ Button() ]
	public void SpawnEnvironment()
	{
		DeleteEnvironment();
		EditorSceneManager.MarkAllScenesDirty();

		var environmentStart = GameObject.Find( "--- Environment_Start ---" ).gameObject;
		// var environmentEnd = GameObject.Find( "--- Environment_End ---" ).gameObject;

		var startIndex = environmentStart.transform.GetSiblingIndex();
		// var endIndex = environmentEnd.transform.GetSiblingIndex();

		spawn_index_start = startIndex + 1;
		spawn_count = 0;

		for( var i = 0; i < level_code.Length; i++ )
		{
			if( level_code[ i ] == 'd' || level_code[ i ] == 'D' )
				SpawnGroundDivider();
			else
				SpawnGround( int.Parse( level_code[ i ].ToString() ) );
		}

		SpawnGroundEnd();
		SpawnPlinko();
	}

    [ Button() ]
    public void DeleteEnvironment()
    {
		EditorSceneManager.MarkAllScenesDirty();

		var environmentStart = GameObject.Find( "--- Environment_Start ---" ).gameObject;
		var environmentEnd   = GameObject.Find( "--- Environment_End ---" ).gameObject;

		var startindex = environmentStart.transform.GetSiblingIndex();
		var endIndex   = environmentEnd.transform.GetSiblingIndex();

		var sceneObjects = EditorSceneManager.GetActiveScene().GetRootGameObjects();

		for( var i = startindex + 1; i < endIndex; i++ )
        {
			environemt_gameObject_list.Add( sceneObjects[ i ] );
		}

        for( var i = 0; i < environemt_gameObject_list.Count; i++ )
        {
			GameObject.DestroyImmediate( environemt_gameObject_list[ i ] );
		}

		environemt_gameObject_list.Clear();
	}
#endregion

#region Implementation
	void SpawnGround( int count )
	{
		for( var i = 0; i < count; i++ )
		{
			var ground = PrefabUtility.InstantiatePrefab( prefab_ground ) as GameObject;
			ground.transform.position = Vector3.forward * ( spawn_count + i ) * prefab_ground_length;
			ground.transform.SetSiblingIndex( spawn_count + i + spawn_index_start );
		}

		spawn_count += count;
	}

	void SpawnGroundDivider()
	{
		var groundDivider = PrefabUtility.InstantiatePrefab( prefab_divider ) as GameObject;
		groundDivider.transform.position = Vector3.forward * ( spawn_count ) * prefab_ground_length;
		groundDivider.transform.SetSiblingIndex( spawn_count + spawn_index_start );

		spawn_count++;
	}

	void SpawnGroundEnd()
	{
		var groundDivider = PrefabUtility.InstantiatePrefab( prefab_end ) as GameObject;
		groundDivider.transform.position = Vector3.forward * ( spawn_count ) * prefab_ground_length;
		groundDivider.transform.SetSiblingIndex( spawn_count + spawn_index_start );

		spawn_count++;
	}

	void SpawnPlinko()
	{

	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
