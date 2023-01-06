/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Ball : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] bool isPooled;
    [ SerializeField ] FloatGameEvent event_currency_gained;

  [ Title( "Shared" ) ]
    [ SerializeField ] BallCache ball_cache_data;
    [ SerializeField, ShowIf( "isPooled" ) ] BallPool ball_pool;
    [ SerializeField ] BallData ball_data;
  [ Title( "Components" ) ]
    [ SerializeField ] Renderer _renderer;
    [ SerializeField ] MeshFilter _meshFilter;
    [ SerializeField ] Collider _collider;
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] ParticleSpawner _particleSpawnner; // Upgrade, Destory, Cache, Currency

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( Vector3 position, Vector3 direction, float velocity, BallData data )
    {
		gameObject.SetActive( true );

		transform.position  = position;
		transform.forward   = direction;
		_rigidbody.velocity = direction * velocity;

		ball_data = data;
		UpdateBall();
	}

    public void DoUpgrade()
    {
		ball_data = ball_data.BallNextData;
		UpdateBall();
		PunchScale();

		_particleSpawnner.Spawn( 0 );
	}

    public void DoDestoryed()
    {
		_particleSpawnner.Spawn( 1 );
		DisableBall();
	}

    public void DoCached()
    {
		_particleSpawnner.Spawn( 2 );

		ball_cache_data.AddData( ball_data );
		DisableBall();
	}

    public void DoConvertCurrency()
    {
		_particleSpawnner.Spawn( 3 );
		DisableBall();

		event_currency_gained.Raise( ball_data.BallCurrency );
	}
#endregion

#region Implementation
    void UpdateBall()
    {
		var renderData = ball_data.BallRenderData;

		_renderer.sharedMaterial = renderData.ball_material;
		_meshFilter.mesh         = renderData.ball_mesh;
		_collider.sharedMaterial = ball_data.BallPhysicMaterial;
		_rigidbody.mass          = ball_data.BallMass;
	}

    void PunchScale()
    {
		_renderer.transform.DOPunchScale( Vector3.one * GameSettings.Instance.ball_punchScale_power, GameSettings.Instance.ball_punchScale_duration );
	}

    void DisableBall()
    {
        if( isPooled )
			ball_pool.ReturnEntity( this );
        else
			gameObject.SetActive( false );
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}