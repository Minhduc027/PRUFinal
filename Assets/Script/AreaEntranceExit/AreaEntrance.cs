using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : BaseSingleton<AreaEntrance>
{

    private void Start()
    {
        PlayerController.Instance.transform.position = this.transform.position;
        CameraController.Instance.SetPlayerCameraFollow();
        UIFade.Instance.FadeToClear();
    }
}
