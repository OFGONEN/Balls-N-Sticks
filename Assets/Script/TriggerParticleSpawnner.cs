/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class TriggerParticleSpawnner : MonoBehaviour
{
#region Fields
    [ SerializeField ] Collider _collider;
    [ SerializeField ] ParticleData _particleData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnColliderTrigger( Collider collider )
    {
		var contactPoint = ( collider.transform.position + _collider.transform.position ) / 2f;

		Transform parent = _particleData.parent ? transform : null;
		_particleData.particle_event.Raise( _particleData.alias, transform.position + _particleData.offset, parent, _particleData.size );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}