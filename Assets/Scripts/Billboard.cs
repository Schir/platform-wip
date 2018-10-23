using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update() 
    {
        transform.LookAt(Camera.main.transform.position, Vector3.down);
		transform.Rotate(0.0f, 180, 0.0f);
    }
}
