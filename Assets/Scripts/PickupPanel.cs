using UnityEngine;
using System.Collections;

public class PickupPanel : MonoBehaviour
{
    private Camera targetCamera;

    void Start() {
        targetCamera = Camera.main;
        StartCoroutine(UpdateLookAtRoutine());
    }
    private IEnumerator UpdateLookAtRoutine() {
        WaitForSeconds waitInterval = new WaitForSeconds(1f);

        while (true) {
            if (targetCamera != null) {
                Vector3 direction = transform.position - targetCamera.transform.position;
                direction.y = 0;
                if (direction.sqrMagnitude > 0.001f) {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    StartCoroutine(SmoothRotate(targetRotation));
                }
            }

            yield return waitInterval;
        }
    }

    private IEnumerator SmoothRotate(Quaternion targetRotation) {
        float duration = 0.5f; // Time in seconds to complete the rotation
        float elapsed = 0f;
        Quaternion initialRotation = transform.rotation;

        while (elapsed < duration) {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure final rotation is set
    }

}
