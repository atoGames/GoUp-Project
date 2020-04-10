using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_Follow : MonoBehaviour {
    public Transform Player;

    void LateUpdate () {

        if(Game_Manager.Instance.mGame == GameMode.PLAY_MODE)
         { 
             
             if(Player.position.y < transform.position.y)
             return;

             Follow_Player ();

         }
    }
    [SerializeField]
    private Vector3 Offset = new Vector3(0 , -0.6f , -10);
    [SerializeField]
    private float Speed = 2f;
    public float MinY = -0.6f;
    public float MaxY = 30f;

    private void Follow_Player () {

        Vector3 posCam = Player.position + Offset;

        posCam.x = 0;
        posCam.y = Mathf.Lerp (transform.position.y, posCam.y, Speed * Time.deltaTime);
        posCam.z = -10;


        // Clamp Camera an Y
        posCam.y = Mathf.Clamp( posCam.y , MinY , MaxY);
        transform.position = posCam;

    }
}