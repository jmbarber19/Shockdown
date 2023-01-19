using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    const string hitboxTag = "hitbox";
    
    public GameObject rock1;
    public GameObject rock2;

    public Rigidbody2D rigidbody;
    public float bounceHitTime = 2f;
    
    private float hitBouncefreezeTime = 0f;

    private float currentHurtTime = 0f;
    private float hurtTime = 0.5f;
    
    private Vector2[] velocityHistory = { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == hitboxTag) {
            Attack attack = col.gameObject.GetComponent<Attack>();
            if (!attack.hasHit) {
                attack.hasHit = true;

                VFXSpawner.I.SpawnElectricEffect(col.ClosestPoint(transform.position), attack.GetDirection());
                ScoreController.I.AddAttack(attack.gameObject.name, attack.style);
                CameraShaker.AddCameraShake(attack.GetDirection(), 0.5f, 2f);
                GameFreezer.Freeze();
                SoundManager.I.Spawn_ran_enHit();

                currentHurtTime = hurtTime;

                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(attack.GetDirection() * attack.force);
                hitBouncefreezeTime = bounceHitTime;
            }
        }
    }

    void AppendVelocity(Vector2 velocity) {
        velocityHistory[0].x = velocityHistory[1].x;
        velocityHistory[0].y = velocityHistory[1].y;

        velocityHistory[1].x = velocityHistory[2].x;
        velocityHistory[1].y = velocityHistory[2].y;
        
        velocityHistory[2].x = velocityHistory[3].x;
        velocityHistory[2].y = velocityHistory[3].y;

        velocityHistory[3].x = velocityHistory[4].x;
        velocityHistory[3].y = velocityHistory[4].y;

        velocityHistory[4].x = velocity.x;
        velocityHistory[4].y = velocity.y;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Vector2 maxVelocity = rigidbody.velocity;
        for (int i = 0; i < velocityHistory.Length; i++) {
            if (velocityHistory[i].magnitude > maxVelocity.magnitude) {
                maxVelocity = velocityHistory[i];
            }
        }

        if (maxVelocity.magnitude > 8f && hitBouncefreezeTime > 0f) {
            GameFreezer.Freeze(0.5f);
            ContactPoint2D contact = collision.GetContact(0);
            CameraShaker.AddCameraShake(contact.normal, 0.5f, 2f);
            VFXSpawner.I.SpawnHitEffect(contact.point, contact.normal);
            SoundManager.I.Spawn_hit1();
            currentHurtTime = hurtTime;

            if (Vector2.Dot(Vector2.up, contact.normal) > 0.8f) { 
                ScoreController.I.AddGroundBounce();
            } else {
                ScoreController.I.AddWallBounce();
            }

            hitBouncefreezeTime = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHurtTime > 0f) {
            currentHurtTime -= Time.deltaTime;
            rock1.SetActive(false);
            rock2.SetActive(true);
        } else {
            rock1.SetActive(true);
            rock2.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (hitBouncefreezeTime > 0f) {
            hitBouncefreezeTime -= Time.deltaTime;
        }
        AppendVelocity(rigidbody.velocity);
    }
}
