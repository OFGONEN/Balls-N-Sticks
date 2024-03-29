/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BallInterface
{
	public void Spawn( BallInterfaceSpawnData spawnData );
	public void DoMultiply();
	public void DoUpgrade();
	public void DoDestoryed();
	public void DoCached();
	public void DoConvertCurrency( float cofactor );
	public void OnFinishLineTrigger();
	public void DoApplyForce( Transform forceOrigin );
}