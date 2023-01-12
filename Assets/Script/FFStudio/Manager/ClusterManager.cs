/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ClusterManager : MonoBehaviour
	{
#region Fields
		public Cluster[] cluster_update_array;
		public Cluster[] cluster_fixedUpddate_array;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			for( var i = 0; i < cluster_update_array.Length; i++ )
				cluster_update_array[ i ].Init();

			for( var i = 0; i < cluster_fixedUpddate_array.Length; i++ )
				cluster_fixedUpddate_array[ i ].Init();
		}

		void Update()
		{
			for( var i = 0; i < cluster_update_array.Length; i++ )
				cluster_update_array[ i ].UpdateCluster();
		}

		void FixedUpdate()
		{
			for( var i = 0; i < cluster_fixedUpddate_array.Length; i++ )
				cluster_fixedUpddate_array[ i ].UpdateCluster();
		}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}