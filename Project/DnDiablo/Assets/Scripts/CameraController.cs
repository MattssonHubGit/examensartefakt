using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    [SerializeField] private Transform trackingTarget;
    [SerializeField] private float trackingSpeed;
    [SerializeField] private GameObject cameraStand;

    [Header("Screen Shake")]
    [SerializeField] private float maxShake;
    [SerializeField] private float severity;
    private float shakeSeverity = 0;
    [Space]
    [SerializeField] private float debugShakeAmount = 0.5f;

    private Vector3 originalPos;

    enum CamState { TRACKING, SHAKING}
    private CamState currentState;


	// Use this for initialization
	void Awake () {

        #region SingleTon
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

        if (trackingTarget == null)
        {
            StartCoroutine(FindPlayer());
        }

        originalPos = transform.position;
	}
	
    IEnumerator FindPlayer()
    {
        yield return new WaitForEndOfFrame();
        trackingTarget = Player.Instance.transform;
    }

	// Update is called once per frame
	void Update () {
        FollowTarget();
        ShakeCamera();

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            AddShake(debugShakeAmount);
        }*/
	}

    private void FollowTarget()
    {
        if (trackingTarget != null)
        {
            cameraStand.transform.position = Vector3.Lerp(cameraStand.transform.position, trackingTarget.position, trackingSpeed);
        }

    }

    //Executes camera shake for it's duration
    private void ShakeCamera()
    {
        if (shakeSeverity > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeSeverity;

            shakeSeverity -= Time.deltaTime;
        }
        else
        {
            shakeSeverity = 0f;
            transform.localPosition = originalPos;
        }
    }

    //Sets the duration and severity of the camera shake
    public void AddShake( float shakeAmount)
    {
        shakeSeverity += shakeAmount;
        if (shakeSeverity > maxShake) // Dont' shake more than we want
        {
            shakeSeverity = maxShake;
        }
    }
}
