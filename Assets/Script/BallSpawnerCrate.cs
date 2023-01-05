/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEditor;

public class BallSpawnerCrate : BallSpawner
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Count Range") ] Vector2Int ball_spawn_count;
    [ SerializeField, LabelText( "Level Range") ] Vector2Int ball_spawn_level;
    [ SerializeField, LabelText( "Spawn Velocity") ] Vector2 ball_spawn_velocity;
    [ SerializeField, LabelText( "Spawn Radius") ] float ball_spawn_radius;
    [ SerializeField, LabelText( "Spawn Offset") ] Vector3 ball_spawn_offset;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	public void Spawn()
	{
		var count    = ball_spawn_count.ReturnRandom();
		var position = transform.position + ball_spawn_offset;

		for( var i = 0; i < count; i++ )
		{
			var ball          = pool_ball.GetEntity();
			var spawnPosition = position + Random.insideUnitSphere * ball_spawn_radius;

			ball.Spawn( spawnPosition, ( spawnPosition - position ).normalized, ball_spawn_velocity.ReturnRandom(), library_ball_data.GetData( ball_spawn_level.ReturnRandom() ) );
		}
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		var position = transform.position + ball_spawn_offset;
		Handles.Label( position, "Ball Crate Spawn" );
		Gizmos.DrawWireSphere( position, ball_spawn_radius );
	}
#endif
#endregion
}