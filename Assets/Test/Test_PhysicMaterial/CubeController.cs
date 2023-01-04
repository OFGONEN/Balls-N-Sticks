using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public CharacterController Cube_Controller;

    public float speed = 6f;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw( "Horizontal" );
        float vertical = Input.GetAxisRaw( "Vertical" );
        Vector3 direction = new Vector3( horizontal, 0f, vertical).normalized;
        
        if( direction.magnitude >= 0.1f )
            Cube_Controller.Move( direction * speed * Time.deltaTime ) ;
    }
}
