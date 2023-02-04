using UnityEngine;

public class PlaySoundInt : MonoBehaviour, Interactable {
    public void Interact() {
        SoundManager.instance.Play(Sounds.DoorClosed);
    }
}
