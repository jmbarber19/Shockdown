using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawner : MonoBehaviour
{
    public static VFXSpawner I;
    [SerializeField]
    private GameObject HitEffect;
    [SerializeField]
    private GameObject ElectricEffect;
    [SerializeField]
    private GameObject CancelEffect;
    [SerializeField]
    private GameObject ExplosionEffect;

    void Awake() {
        I = this;
    }

    public void SpawnHitEffect(Vector3 position, Vector2 normal) {
        GameObject result = GameObject.Instantiate(HitEffect, position, Quaternion.FromToRotation(Vector3.up, normal));
    }
    public void SpawnElectricEffect(Vector3 position, Vector2 normal) {
        GameObject result = GameObject.Instantiate(ElectricEffect, position, Quaternion.FromToRotation(Vector3.up, normal));
    }
    public void SpawnCancelEffect(Vector3 position) {
        GameObject result = GameObject.Instantiate(CancelEffect, position, Quaternion.identity);
    }
    public void SpawnExplosionEffect(Vector3 position, Vector2 normal) {
        GameObject result = GameObject.Instantiate(ExplosionEffect, position, Quaternion.FromToRotation(Vector3.up, normal));
    }
}
