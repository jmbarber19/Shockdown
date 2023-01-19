using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [SerializeField]
    private MovementController movementController;
    public float time = 0.4f;
    public float damage = 1f;
    public float style = 1f;
    // public Vector2 playerKnockback = Vector2.zero;
    // public float playerKnockbackForce = 200f;
    [SerializeField]
    private Vector2 direction = Vector2.zero;
    public float force = 200f;
    public bool hasHit = false;

    public Vector2 GetDirection() {
        return new Vector2(
            movementController.FacingRight() ? direction.x : -direction.x,
            direction.y
        );
    }

    public void Reset() {
        hasHit = false;
        gameObject.SetActive(false);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }
}
