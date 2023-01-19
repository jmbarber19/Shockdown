using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private MovementController movementController;
    [SerializeField]
    private Attack launcher;
    [SerializeField]
    private Attack spike;
    [SerializeField]
    private Attack stinger;
    [SerializeField]
    private Attack aerialLauncher;
    [SerializeField]
    private Attack aerialSpike;
    [SerializeField]
    private Attack aerialStinger;

    [SerializeField]
    private float airBounce = 200f;

    [SerializeField]
    private float groundBounce = 200f;

    private float currentAttackTime = 0f;

    public bool IsAttacking() {
        return currentAttackTime > 0f;
    }

    // Set current state to not doing any movement 
    public void CancelAttacks() {
        ClearAttacks();
        currentAttackTime = 0f;
    }

    void DoAttack(Attack attack) {
        attack.Activate();
        currentAttackTime = attack.time;
    }

    // Reset all attacks to not active
    void ClearAttacks() {
        launcher.Reset();
        spike.Reset();
        stinger.Reset();
        aerialLauncher.Reset();
        aerialSpike.Reset();
        aerialStinger.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IntroController.I.IsDonePlaying() || ScoreController.I.IsGameOver()) { return; }

        if (IsAttacking()) {
            currentAttackTime -= Time.deltaTime;
            if (!IsAttacking()) {
                ClearAttacks();
            }
        }

        if (!IsAttacking()) {
            if (Input.GetKeyDown(KeyCode.K)) {
                Vector2 facingDirection = (movementController.FacingRight() ? Vector2.right : Vector2.left);
                if (movementController.grounded) {
                    if (movementController.forward == 1) {
                        DoAttack(stinger);
                        PlayerAnimator.I.setStinger();
                        SoundManager.I.Spawn_shock3();
                        movementController.rigidbody.AddForce(facingDirection * groundBounce);
                    } else if (movementController.forward == 0) {
                        DoAttack(launcher);
                        PlayerAnimator.I.setLauncher();
                        SoundManager.I.Spawn_shock8();
                    } else {
                        DoAttack(spike);
                        PlayerAnimator.I.setSpike();
                        SoundManager.I.Spawn_shock5();
                        movementController.rigidbody.AddForce(-facingDirection * groundBounce);
                    }
                } else {
                    // Bounce
                    movementController.rigidbody.velocity = Vector2.zero;

                    if (movementController.forward == 1) {
                        DoAttack(aerialStinger);
                        PlayerAnimator.I.setAirStinger();
                        SoundManager.I.Spawn_shock4();
                        movementController.rigidbody.AddForce((facingDirection + Vector2.up).normalized * airBounce);
                    } else if (movementController.forward == 0) {
                        DoAttack(aerialLauncher);
                        PlayerAnimator.I.setAirLauncher();
                        SoundManager.I.Spawn_shock6();
                        movementController.rigidbody.AddForce(Vector2.up * airBounce * 2f);
                    } else {
                        DoAttack(aerialSpike);
                        PlayerAnimator.I.setAirSpike();
                        SoundManager.I.Spawn_shock7();
                        movementController.rigidbody.AddForce(Vector2.up * airBounce);
                    }
                }
            }

        }
    }
}
