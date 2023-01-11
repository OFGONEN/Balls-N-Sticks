/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;


public class BallSpawnerPlinko : BallSpawner
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Spawn Velocity") ] Vector2 ball_spawn_velocity;
    [ SerializeField, LabelText( "Spawn Radius") ] float ball_spawn_radius;
    [ SerializeField, LabelText( "Spawn Delay") ] Vector2 ball_spawn_delay;

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	[ Button() ]
    public void StartSpawnSequence()
    {
		var sequence = recycledSequence.Recycle();

		sequence.AppendCallback( Spawn );
		sequence.AppendInterval( ball_spawn_delay.ReturnRandom() );
		sequence.SetLoops( ball_cache.BallDataCount );
	}
#endregion

#region Implementation
    void Spawn()
    {
		var ball = pool_ball.GetEntity();
        //Info: Direction can be changed if needed.
		ball.Spawn( transform.position + Random.insideUnitSphere * ball_spawn_radius,
            Vector3.down, ball_spawn_velocity.ReturnRandom(), ball_cache.GetBallData() );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		var position = transform.position;
		Handles.Label( position, "Ball Crate Spawn" );
		Gizmos.DrawWireSphere( position, ball_spawn_radius );
	}
#endif
#endregion
}