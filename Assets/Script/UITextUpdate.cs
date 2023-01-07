/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using TMPro;

public class UITextUpdate : MonoBehaviour
{
#region Fields
    [ SerializeField ] string prefix;
    [ SerializeField ] string suffix;
    [ SerializeField ] TextMeshProUGUI _textRenderer;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Update( int value )
    {
		_textRenderer.text = prefix + value.ToString() + suffix;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
