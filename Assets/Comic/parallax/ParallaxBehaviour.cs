using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
	public ParallaxController parallaxController;
	public float effectMultiplier;
	public int page = 1;
	public float shake = 0f;
	private Vector3 lastShakeVector = Vector3.zero;

	// Start is called before the first frame update
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		float mouseRatioX = Input.mousePosition.x / Screen.width - 0.5f;
		float mouseRatioY = Input.mousePosition.y / Screen.height - 0.5f;
		Vector3 newTargetPosition = new Vector3(
			effectMultiplier * -mouseRatioX,
			effectMultiplier * -mouseRatioY * 0.70f,
			transform.position.z
		);
		Vector3 offset = parallaxController.GetWobbleOffset() * effectMultiplier + parallaxController.GetPageOffset(page);

		if (shake > 0f && parallaxController.TimeSincePageChange > 0.2f) {
			float shakeMulti = Mathf.Clamp01(1.5f - parallaxController.TimeSincePageChange);
			offset += parallaxController.CurrentShake * shakeMulti * shake * 0.5f;
		}
		transform.position = newTargetPosition + offset;
	}
}
