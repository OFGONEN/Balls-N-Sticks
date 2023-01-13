/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_ball_data_", menuName = "FF/Game/Ball Data" ) ]
public class BallData : ScriptableObject
{
    [ SerializeField, LabelText( "Applied Force" ) ] float ball_force;
    [ SerializeField, LabelText( "Applied Force Mode" ) ] ForceMode ball_force_mode = ForceMode.Impulse;
    [ SerializeField, LabelText( "Mass" ) ] float ball_mass;
    [ SerializeField, LabelText( "Drag" ) ] float ball_drag;
    [ SerializeField, LabelText( "Angular Drag" ) ] float ball_angularDrag;
    [ SerializeField, LabelText( "Max Speed" ) ] float ball_speed_max;
    [ SerializeField, LabelText( "Render Data" ) ] BallRenderData[] ball_render_data_array;
    [ SerializeField, LabelText( "Physic Material" ) ] PhysicMaterial ball_material_physic;
    [ SerializeField, LabelText( "Currency Range" ) ] Vector2 ball_currency_range;
    [ SerializeField, LabelText( "Next Data" ) ] BallData ball_nextData;

	public float BallAppliedForce            => ball_force;
	public ForceMode BallAppliedForceMode    => ball_force_mode;
	public float BallSpeedMax                => ball_speed_max;
	public float BallMass                    => ball_mass;
	public float BallDrag                    => ball_drag;
	public float BallAngularDrag             => ball_angularDrag;
	public BallRenderData BallRenderData     => ball_render_data_array.ReturnRandom();
	public PhysicMaterial BallPhysicMaterial => ball_material_physic;
	public float BallCurrency                => ball_currency_range.ReturnRandom();
	public BallData BallNextData             => ball_nextData;
}

[ System.Serializable ]
public struct BallRenderData
{
	public Material ball_material;
	public Mesh ball_mesh;
}