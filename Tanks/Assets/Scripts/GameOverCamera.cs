using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCamera : MonoBehaviour {

    public Camera gameOverCamera;


    private void OnDisable()
    {
        gameOverCamera.gameObject.SetActive(true);

    }
}
