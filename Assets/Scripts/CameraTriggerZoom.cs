using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraTriggerZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform player;
    [SerializeField] private DockScript dockScript;

    [SerializeField] private float fovIN = 45f;
    [SerializeField] private float duration = 1f;

    private float fovOUT;
    private Quaternion originRotation;
    private bool focusPlayer;

    private Tween fovTween;
    private Tween lookAtTween;

    private void Start()
    {
        fovOUT = cam.fieldOfView;
        originRotation = cameraTransform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !dockScript.isActiveAndEnabled) return;

        fovTween?.Kill();
        fovTween = cam.DOFieldOfView(fovIN, duration)
            .SetEase(Ease.InSine);

        lookAtTween = cameraTransform.DOLookAt(player.position,duration).SetEase(Ease.InOutSine);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        fovTween?.Kill();
        fovTween = cam.DOFieldOfView(fovOUT, duration)
            .SetEase(Ease.InSine);


        cameraTransform.DORotateQuaternion(originRotation, duration)
            .SetEase(Ease.InSine);
    }

}