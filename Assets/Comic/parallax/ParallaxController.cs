using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParallaxController : MonoBehaviour
{

	public float WobbleSpeed = 1f;
	public float WobbleMultiplier = 0.55f;
	public int currentPage = 1;
	public float pageChangeSeconds = 1f;
	public float pageDistance = 20f;
	private float timeSincePageChange = 0f;
	private Vector3 lastShakeVector = Vector3.zero;
	private Vector3 currentShake = Vector3.zero;
	private int lastShakeUpdate = 0;

	private int targetPage = 1;
	private float pageChangeProgress = 0f;

	public float TimeSincePageChange { get => timeSincePageChange; }
	public Vector3 CurrentShake { get
		{
			if (lastShakeUpdate != Time.frameCount) updateShake();
			return currentShake;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		targetPage = currentPage;
	}

    // Update is called once per frame
    void Update()
    {
		float deltaTime = Time.deltaTime;
		timeSincePageChange += deltaTime;
		if (currentPage != targetPage)
		{
			pageChangeProgress = pageChangeProgress + deltaTime;
			if (pageChangeProgress >= pageChangeSeconds)
			{
				currentPage = targetPage;
				pageChangeProgress = 0f;
				timeSincePageChange = 0f;
			}
		}
		updateShake();
	}

	private void updateShake() {
		lastShakeUpdate = Time.frameCount;
		Vector3 newShake = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0f);
		newShake = (lastShakeVector * 2 + newShake) / 3.5f;
		lastShakeVector = currentShake;
		currentShake = newShake;
	}

	private float calculateWobble(float offset) {
		float ratio = 1.61803f;
		float phase1 = Mathf.Sin(offset + Time.time * WobbleSpeed);
		float phase2 = Mathf.Sin(offset * ratio + Time.time * WobbleSpeed * ratio);
		return (phase1 + phase2) / 2f * WobbleMultiplier;
	}

	public Vector3 GetWobbleOffset() {
		return new Vector3(calculateWobble(0)*1.1f, calculateWobble(20f)*0.8f, 0);
	}

	public Vector3 GetPageOffset(int page) {
		float progress = Mathf.SmoothStep(0, 1, pageChangeProgress / pageChangeSeconds);
		float absDist = -progress * pageDistance + ((page - currentPage) * pageDistance);
		Vector3 pageOffset = new Vector3(absDist, 0, 0);
		return pageOffset;
	}

	public void Continue() {
		Debug.Log("Continue");
		if (currentPage == 3)
		{
			SceneManager.LoadScene(2);
		}
		else
		{
			if (currentPage == targetPage)
			{
				targetPage += 1;
				Debug.Log("setting targetPage: " + targetPage);
				pageChangeProgress = 0f;
			}
		}
	}
}
