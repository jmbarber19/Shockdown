using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroNumber : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Vector3 goalLanding;
    public float flingSpeed = 200f;
    public float intialMoveSpeed = 25f;
    
    private bool initialMoving = false;
    private bool isFlung = false;
    private float lifeTimer = 1f;

    void Start() {
        rigidbody.simulated = false;
        goalLanding.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialMoving) { 
            transform.position = Vector3.Lerp(transform.position, goalLanding, Time.deltaTime * intialMoveSpeed);
        } else if (isFlung) {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0f) {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void StartMoving() {
        if (!isFlung) {
            initialMoving = true;
        } else {
            initialMoving = false;
        }
    }

    public void Fling() {
        if (!isFlung) {
            rigidbody.simulated = true;
            Vector3 fling = (Quaternion.AngleAxis(Random.Range(-90f,90f), Vector3.back) * Vector2.up) * flingSpeed;
            rigidbody.AddForce(fling);
            rigidbody.AddTorque(
                Random.Range(500f,800f) * (Random.Range(0,1) * 2 - 1 )
            );
            isFlung = true;
            VFXSpawner.I.SpawnExplosionEffect(transform.position, fling);
            CameraShaker.AddCameraShake(fling.normalized, 2f);
            SoundManager.I.Spawn_hit2();
        }
    }

}
