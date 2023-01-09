/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_ball_interface", menuName = "FF/Game/Interface/Ball Interface" ) ]
public class BallInterfaceCollidderReceiver : ScriptableObject , BallInterface
{
#region Fields
    [ ShowInInspector, ReadOnly ] Ball _ball;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void CacheBall( Collision collision )
    {
        _ball = collision.collider.GetComponent< ComponentHost >().HostComponent as Ball;
    }

	public void DoMultiply()
	{
		_ball.DoMultiply();
	}

	public void DoCached()
	{
		_ball.DoCached();
	}

	public void DoConvertCurrency( float cofactor )
	{
		_ball.DoConvertCurrency( cofactor );
	}

	public void DoDestoryed()
	{
		_ball.DoDestoryed();
	}

	public void DoUpgrade()
	{
		_ball.DoUpgrade();
	}

	public void Spawn( BallInterfaceSpawnData spawnData )
	{
		_ball.Spawn( spawnData.SpawnPosition, spawnData.SpawnDirection, spawnData.SpawnVelocity, spawnData.SpawnBallData );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}