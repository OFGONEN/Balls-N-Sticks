/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEditor;

public class ToolBallSpawner : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float ball_spawn_radius;
    [ SerializeField ] Vector2 ball_count_range;
    [ InfoBox( "Level Starts On 1" ) ]
    [ SerializeField ] Vector2Int ball_level_range;

  [ Title( "Configure" ) ]
    [ SerializeField ] GameObject[] ball_gameObject_array;

    List< GameObject > ball_spawned_list = new List< GameObject >( 16);
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void Spawn()
    {
		ball_spawned_list.Clear();

		var position   = transform.position.SetY( 0 );
		var spawnCount = ball_count_range.ReturnRandom();

		var siblingIndex = GameObject.Find( "--- Entities_Start ---" ).transform.GetSiblingIndex();

        for( var i = 0; i < spawnCount; i++ )
        {
			var ballGameObject = PrefabUtility.InstantiatePrefab( ball_gameObject_array[ ball_level_range.ReturnRandom() - 1 ] ) as GameObject;
			ballGameObject.transform.position = position + Random.insideUnitCircle.ConvertV3_Z() * ball_spawn_radius;
			ballGameObject.transform.SetSiblingIndex( siblingIndex + 1 );

			ball_spawned_list.Add( ballGameObject );
		}
	}

    [ Button() ]
    public void SpawnAgain()
    {
		DeleteLastSpawned();
		Spawn();
	}

    [ Button() ]
    public void DeleteLastSpawned()
    {
        for( var i = 0; i < ball_spawned_list.Count; i++ )
        {
			GameObject.DestroyImmediate( ball_spawned_list[ i ] );
		}

		ball_spawned_list.Clear();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnValidate()
    {
		transform.position = transform.position.SetY( 0 );
	}

    private void OnDrawGizmos()
    {
		Handles.DrawWireArc( transform.position, Vector3.up, Vector3.left, 360, ball_spawn_radius );
	}
#endif
#endregion
}