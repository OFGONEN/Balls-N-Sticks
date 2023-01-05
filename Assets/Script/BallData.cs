/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_ball_data_", menuName = "FF/Game/Ball Data" ) ]
public class BallData : ScriptableObject
{
    [ SerializeField, LabelText( "Mass" ) ] float ball_mass;
    [ SerializeField, LabelText( "Mesh Material" ) ] Material ball_material;
    [ SerializeField, LabelText( "Physic Material" ) ] PhysicMaterial ball_material_physic;
    [ SerializeField, LabelText( "Currency Range" ) ] Vector2 ball_currency_range;
    [ SerializeField, LabelText( "Next Data" ) ] BallData ball_nextData;

	public float BallMass                    => ball_mass;
	public Material BallMaterial             => ball_material;
	public PhysicMaterial BallPhysicMaterial => ball_material_physic;
	public float BallCurrency                => ball_currency_range.ReturnRandom();
	public BallData BallNextData             => ball_nextData;
}