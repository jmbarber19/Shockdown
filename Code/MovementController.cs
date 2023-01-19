using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AttackController attackController;
    [SerializeField]
    private Target target;
    [SerializeField]
    private Transform holder;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float oppositeDirectionMultiplier = 0.6f;
    [SerializeField]
    private float airForce = 40f;
    [SerializeField]
    private float jumpSpeed = 4f;

    public Rigidbody2D rigidbody;
    public bool grounded = false;
    public int forward = 0;

    public bool FacingRight() {
        return target.transform.position.x > transform.position.x;
    }

    void CalculateGrounded(Collision2D collision) {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float dot = Vector2.Dot(
                Vector2.up,
                collision.contacts[i].normal
            );
            if (dot > 0.95f) {
                if (!grounded && attackController.IsAttacking()) {
                    ScoreController.I.AddCancel();
                    SoundManager.I.Spawn_cancel1();
                    VFXSpawner.I.SpawnCancelEffect(transform.position);
                    attackController.CancelAttacks();
                }
                grounded = true;
                return;
            }
        }
        grounded = false;
    }

    void OnCollisionStay2D(Collision2D collision) {
        CalculateGrounded(collision);
    }
    void OnCollisionEnter2D(Collision2D collision) {
        CalculateGrounded(collision);
    }
    void OnCollisionExit2D(Collision2D collision) {
        CalculateGrounded(collision);
    }

    void Update()
    {
        if (!IntroController.I.IsDonePlaying() || ScoreController.I.IsGameOver()) { return; }

        forward = 0;
        if (target.transform.position.x > transform.position.x) { // Target Right
            holder.localScale = new Vector3(1, 1, 1);
            forward += Input.GetKey(KeyCode.D) ? 1 : 0;
            forward += Input.GetKey(KeyCode.A) ? -1 : 0;
        } else { // Target Left
            holder.localScale = new Vector3(-1, 1, 1);
            forward += Input.GetKey(KeyCode.D) ? -1 : 0;
            forward += Input.GetKey(KeyCode.A) ? 1 : 0;
        }

        if (!attackController.IsAttacking()) {
            if (grounded) {
                if (forward == 1) { 
                    PlayerAnimator.I.setMoveForward();
                } else if (forward == -1) {
                    PlayerAnimator.I.setMoveBack();
                } else {
                    PlayerAnimator.I.setStand();
                }
            } else {
                if (rigidbody.velocity.y > 0f) {
                    PlayerAnimator.I.setJump();
                } else {
                    PlayerAnimator.I.setFall();
                }
            }
        }

        if (!attackController.IsAttacking()) {
            float multiplier = forward > 0f ? 1f : oppositeDirectionMultiplier;
            if (grounded) {
                if (Input.GetKey(KeyCode.A)) {rigidbody.velocity = new Vector2(-speed * multiplier, rigidbody.velocity.y); }
                if (Input.GetKey(KeyCode.D)) {rigidbody.velocity = new Vector2(speed * multiplier, rigidbody.velocity.y); }
            } else {
                if (Input.GetKey(KeyCode.A)) { rigidbody.AddForce(airForce * multiplier * Vector2.left); }
                if (Input.GetKey(KeyCode.D)) { rigidbody.AddForce(airForce * multiplier * Vector2.right); }
            }
        }

        if (grounded && Input.GetKeyDown(KeyCode.J)) {
            grounded = false;
            rigidbody.AddForce(jumpSpeed * Vector2.up);
            SoundManager.I.Spawn_jump1();
            if (attackController.IsAttacking()) {
                SoundManager.I.Spawn_cancel1();
                ScoreController.I.AddCancel();
                VFXSpawner.I.SpawnCancelEffect(transform.position);
                attackController.CancelAttacks();
            }
        }
    }
}
