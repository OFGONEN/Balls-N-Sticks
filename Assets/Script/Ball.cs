/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Ball : MonoBehaviour , IClusterEntity
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] bool isPooled;
    [ SerializeField ] FloatGameEvent event_currency_gained;
    [ SerializeField ] Cluster cluster_ball;

  [ Title( "Shared" ) ]
    [ SerializeField ] BallCache ball_cache_data;
    [ SerializeField ] BallPool ball_pool;
    [ SerializeField ] BallData ball_data;
  [ Title( "Components" ) ]
    [ SerializeField ] Renderer _renderer;
    [ SerializeField ] MeshFilter _meshFilter;
    [ SerializeField ] Collider _collider;
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] ParticleSpawner _particleSpawnner; // Upgrade, Destory, Cache, Currency, Spawn

	UnityMessage onFinishLine = ExtensionMethods.EmptyMethod;
	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		Subscribe_Cluster();
	}

	private void OnDisable()
	{
		UnSubscribe_Cluster();
	}
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

		onFinishLine = ExtensionMethods.EmptyMethod;

		_particleSpawnner.Spawn( 4 );
	}

	public void DoMultiply()
	{
		var ball = ball_pool.GetEntity();

		ball.Spawn( transform.position + GameSettings.Instance.ball_multiply_offset, Vector3.forward, _rigidbody.velocity.magnitude, ball_data );
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

    public void DoConvertCurrency( float cofactor )
    {
		_particleSpawnner.Spawn( 3 );
		DisableBall();

		event_currency_gained.Raise( ball_data.BallCurrency * cofactor );
	}

	public void OnFinishLineTrigger()
	{
		onFinishLine = DoCached;
	}

	public void OnFinishLine()
	{
		onFinishLine();
	}

    public void UpdateBall()
    {
		var renderData = ball_data.BallRenderData;

		_renderer.sharedMaterial = renderData.ball_material;
		_meshFilter.mesh         = renderData.ball_mesh;
		_collider.sharedMaterial = ball_data.BallPhysicMaterial;
		_rigidbody.mass          = ball_data.BallMass;
		_rigidbody.drag          = ball_data.BallDrag;
		_rigidbody.angularDrag   = ball_data.BallAngularDrag;
	}

	public void DoApplyForce( Transform forceOrigin )
	{
		_rigidbody.AddForce( forceOrigin.forward * ball_data.BallAppliedForce, ForceMode.Impulse );
	}

	public void Subscribe_Cluster()
	{
		cluster_ball.Subscribe( this );
	}

	public void UnSubscribe_Cluster()
	{
		cluster_ball.UnSubscribe( this );
	}

	public void OnUpdate_Cluster()
	{
		var velocity = _rigidbody.velocity;
		_rigidbody.velocity = velocity.normalized * Mathf.Min( ball_data.BallSpeedMax, velocity.magnitude );
	}

	public int GetID()
	{
		return GetInstanceID();
	}
#endregion

#region Implementation
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