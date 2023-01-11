/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "notif_ball_cache", menuName = "FF/Game/Ball Cache Data" ) ]
public class BallCache : SharedIntNotifier
{
    Stack< BallData > ball_data_stack;

    public int BallDataCount => ball_data_stack.Count;

    //Info: Call From manager_asset on Awake
    public void OnAwake( int size )
    {
		ball_data_stack = new Stack< BallData >( size );
		sharedValue     = 0;
	}

    //Info: Call From manager_asset on respond to event_level_unload_start
    public void Clear()
    {
		ball_data_stack.Clear();
		SetValue_NotifyAlways( 0 );
	}

	[ Button() ]
    public void AddData( BallData data )
    {
		ball_data_stack.Push( data );
		SharedValue = ball_data_stack.Count;
	}

	public BallData GetBallData() 
	{
		return ball_data_stack.Pop();
	}
}