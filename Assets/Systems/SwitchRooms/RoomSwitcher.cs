using UnityEngine;

public class RoomSwitcher : MonoBehaviour {
    // public Transform targetRoom;
    // public SpriteRenderer fadeSprite;
    // [Range(0, 2f)]
    // public float fadeSpeed;
    // [Range(0, 2f)]
    // public float dimDuration;

    // private Transform player;
    // private Transform targetPosition;
    // private CinemachineVirtualCamera virtualCamera;
    // private CinemachineConfiner2D confiner;
    // private PolygonCollider2D polygonCollider;

    // void Start() {
    //     player = FindObjectOfType<PlayerController>().transform;
    //     virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    //     confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
    //     polygonCollider = targetRoom.GetComponentInChildren<PolygonCollider2D>();
    //     targetPosition = transform.GetChild(0);
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if (!other.CompareTag("Player")) return;


    //     gameObject.LeanValue(Color.black * 0, Color.black, fadeSpeed)
    //     .setOnUpdate(v => fadeSprite.color = v)
    //     .setOnComplete(() => {
    //         player.transform.position = targetPosition.position;
    //         confiner.m_BoundingShape2D = polygonCollider;
    //         gameObject.LeanValue(0, 0, dimDuration).setOnComplete(() => {
    //             gameObject.LeanValue(Color.black, Color.black * 0, fadeSpeed)
    //             .setOnUpdate(v => fadeSprite.color = v);
    //         });
    //     });
    // }
}
