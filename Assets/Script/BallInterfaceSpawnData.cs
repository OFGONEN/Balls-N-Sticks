/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_ball_interface_spawn", menuName = "FF/Game/Interface/Ball Spawn Data" ) ]
public class BallInterfaceSpawnData : ScriptableObject
{
    [ SerializeField ] Vector3 spawn_position;
    [ SerializeField ] Vector3 spawn_direction;
    [ SerializeField ] float spawn_velocity;
    [ SerializeField ] BallData spawn_ball_data;

	public Vector3 SpawnPosition  => spawn_position;
	public Vector3 SpawnDirection => spawn_direction;
	public float SpawnVelocity    => spawn_velocity;
	public BallData SpawnBallData => spawn_ball_data;
}