/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using FFStudio;
using Sirenix.OdinInspector;

public class UIMoneyCounter : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ SerializeField ] string text_suffix; // TextRenderer 
    [ SerializeField ] Vector2 text_rotation_range; // TextRenderer 
  [ Title( "Components" ) ]
    [ SerializeField ] GameObject gfx_parent;
    [ SerializeField ] TextMeshProUGUI _textRenderer;
    [ SerializeField ] TweenPunchScale _tweenPunchScale; // TextRenderer 

	UnityFloatMessage onMoneyGained;

	float money;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		onMoneyGained = MoneyFirstGained;
	}
#endregion

#region API
    public void OnMoneyGained( float value )
    {
		onMoneyGained( value );
	}

    public void OnLevelUnloadStart()
    {
		gfx_parent.SetActive( false );
		onMoneyGained = MoneyFirstGained;
	}
#endregion

#region Implementation
    void MoneyFirstGained( float value )
    {
		onMoneyGained = MoneyGained;
		gfx_parent.SetActive( true );

		money += value;
		UpdateMoneyRenderer();
	}

    void MoneyGained( float value )
    {
		money += value;
		UpdateMoneyRenderer();
	}

    void UpdateMoneyRenderer()
    {
		_textRenderer.text = text_suffix + money.ToString( "F1" );
		_textRenderer.rectTransform.localEulerAngles = Vector3.forward * text_rotation_range.ReturnRandom();

		_textRenderer.rectTransform.localScale = Vector3.one;
		_tweenPunchScale.DoPunchScale();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}