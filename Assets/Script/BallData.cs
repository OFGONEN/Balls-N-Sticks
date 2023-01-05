/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_ball_data_", menuName = "FF/Game/Ball Data" ) ]
public class BallData : ScriptableObject
{
    [ SerializeField, LabelText( "Mesh Material" ) ] Material ball_material;
    [ SerializeField, LabelText( "Physic Material" ) ] PhysicMaterial ball_material_physic;
    [ SerializeField, LabelText( "Currency Range" ) ] Vector2 ball_currency_range;
    [ SerializeField, LabelText( "Next Data" ) ] BallData ball_nextData;

	public Material BallMaterial             => ball_material;
	public PhysicMaterial BallPhysicMaterial => ball_material_physic;
	public Vector2 BallCurrency              => ball_currency_range;
	public BallData BallNextData             => ball_nextData;
}