/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "notif_ball_cache", menuName = "FF/Game/Ball Cache Data" ) ]
public class BallCacheData : SharedIntNotifier
{
    List< BallData > ball_data_list;

    public List< BallData > BallDataList => ball_data_list;

    //Info: Call From manager_asset on Awake
    public void OnAwake( int size )
    {
		ball_data_list = new List< BallData >( size );
		sharedValue    = 0;
	}

    //Info: Call From manager_asset on respond to event_level_unload_start
    public void Clear()
    {
		ball_data_list.Clear();
		SetValue_NotifyAlways( 0 );
	}

    public void Add( BallData data )
    {
		ball_data_list.Add( data );
		SharedValue = ball_data_list.Count;
	}
}