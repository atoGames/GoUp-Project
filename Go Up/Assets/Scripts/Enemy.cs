using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public LayerMask _Block;
    public SpriteRenderer grf;

     public bool ChangeDir = false;
    public float dev = 0.90f;
    public float Speed = 1f;
    public float circleAngle = 0f;

    void Start () {
        grf = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {
        OnGround ();

        if (ChangeDir) {
            transform.Translate (Vector2.right * Speed * Time.deltaTime);
            grf.flipX = true;
            circleAngle = -200;

        } else {
            transform.Translate (-Vector2.right * Speed * Time.deltaTime);
            grf.flipX = false;
            circleAngle = 200;
        }

    }
   
    void OnGround () {

        var dir = (Vector2.down + (Vector2) transform.position); // 
        dir.x = Mathf.Sin (Mathf.Deg2Rad * circleAngle);
        dir.y = Mathf.Cos (Mathf.Deg2Rad * circleAngle);

        if (Physics2D.Raycast (this.transform.position, dir, 3f, _Block)) {
            Debug.DrawRay (this.transform.position, dir, Color.red);
//            Debug.Log (" Raycast Runng!");

        } else {
            if (ChangeDir == true)
                   ChangeDir = false;
            else
                    ChangeDir = true;

        }

    }

}