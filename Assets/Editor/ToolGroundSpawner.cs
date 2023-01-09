/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor.SceneManagement;

[ CreateAssetMenu( fileName = "tool_ground_spawner", menuName = "FF/Game/Tool/Ground Spawn" ) ]
public class ToolGroundSpawner : ScriptableObject
{
#region Fields
    List< GameObject > environemt_gameObject_list = new List< GameObject >();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
