using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_script : MonoBehaviour
{

    public int x_rotation;
    public int y_rotation;
    public int z_rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     public void Update()
    {
        transform.Rotate(x_rotation,y_rotation,z_rotation);
    }
}
