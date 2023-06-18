using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroudView;

    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = backgroudView.bounds.size.y /2;
        Camera.main.orthographicSize = orthoSize;
    }
}
