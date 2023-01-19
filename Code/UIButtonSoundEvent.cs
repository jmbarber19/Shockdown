using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler {

    public void OnPointerEnter( PointerEventData ped ) {
        SoundManager.I.Spawn_ui1();
    }
}