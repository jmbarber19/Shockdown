using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator I;
    [Header("Move")]
    public GameObject stand;
    public GameObject jump;
    public GameObject fall;
    public GameObject moveForward;
    public GameObject moveBack;
    [Header("Attack Ground")]
    public GameObject stinger;
    public GameObject spike;
    public GameObject launcher;
    [Header("Attack Air")]
    public GameObject airStinger;
    public GameObject airSpike;
    public GameObject airLauncher;

    void Awake()
    {
        I = this;    
    }

    public void ClearAll() {
        stand.SetActive(false);
        jump.SetActive(false);
        fall.SetActive(false);
        moveForward.SetActive(false);
        moveBack.SetActive(false);
        stinger.SetActive(false);
        spike.SetActive(false);
        launcher.SetActive(false);
        airStinger.SetActive(false);
        airSpike.SetActive(false);
        airLauncher.SetActive(false);
    }
    
    public void setStand() { ClearAll(); stand.SetActive(true); }
    public void setJump() { ClearAll(); jump.SetActive(true); }
    public void setFall() { ClearAll(); fall.SetActive(true); }
    public void setMoveForward() { ClearAll(); moveForward.SetActive(true); }
    public void setMoveBack() { ClearAll(); moveBack.SetActive(true); }
    public void setStinger() { ClearAll(); stinger.SetActive(true); }
    public void setSpike() { ClearAll(); spike.SetActive(true); }
    public void setLauncher() { ClearAll(); launcher.SetActive(true); }
    public void setAirStinger() { ClearAll(); airStinger.SetActive(true); }
    public void setAirSpike() { ClearAll(); airSpike.SetActive(true); }
    public void setAirLauncher() { ClearAll(); airLauncher.SetActive(true); }
}
