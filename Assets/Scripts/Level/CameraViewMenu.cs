using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraViewMenu : MonoBehaviour
{
    [SerializeField] Image backgroudView;

    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = backgroudView.rectTransform.rect.height / 2;
        Camera.main.orthographicSize = orthoSize;
    }
}
