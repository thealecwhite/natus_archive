﻿using UnityEngine;

public class Backgrounds : MonoBehaviour
{
	public Sprite skySprite, parallaxSprite;
	private Transform sky, leftParallax, rightParallax;
	private SpriteRenderer skyRenderer, leftParallaxRenderer, rightParallaxRenderer;
	private int rightParallaxSide, leftParallaxSide;
	private Vector3 lastPosition;

	private void Start()
	{
		sky = new GameObject("Sky Background", typeof(SpriteRenderer)).transform;
		leftParallax = new GameObject("Left Parallax", typeof(SpriteRenderer)).transform;
		rightParallax = new GameObject("Right Parallax", typeof(SpriteRenderer)).transform;
		sky.gameObject.hideFlags = leftParallax.gameObject.hideFlags = rightParallax.gameObject.hideFlags = HideFlags.HideInHierarchy;

		skyRenderer = sky.GetComponent<SpriteRenderer>();
		leftParallaxRenderer = leftParallax.GetComponent<SpriteRenderer>();
		rightParallaxRenderer = rightParallax.GetComponent<SpriteRenderer>();

		skyRenderer.sprite = skySprite;
		leftParallaxRenderer.sprite = rightParallaxRenderer.sprite = parallaxSprite;

		skyRenderer.sortingOrder = -10;
		leftParallaxRenderer.sortingOrder = rightParallaxRenderer.sortingOrder = -5;
		skyRenderer.sortingLayerName = leftParallaxRenderer.sortingLayerName = rightParallaxRenderer.sortingLayerName = "Background";

		leftParallax.position = new Vector2(transform.position.x, leftParallax.position.y);
		rightParallax.position = new Vector2(transform.position.x, rightParallax.position.y);

		lastPosition = sky.transform.position;
	}

	private void LateUpdate()
	{
		float vertical = Camera.main.orthographicSize;
		float horizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

		Vector2 parallax = new Vector2((lastPosition - transform.position).x * -0.75f, (lastPosition - transform.position).y * -0.95f);

		sky.position = new Vector3(transform.position.x, transform.position.y);

		leftParallax.position = new Vector3(leftParallax.position.x + parallax.x, leftParallax.position.y + parallax.y);
		rightParallax.position = new Vector3(rightParallax.position.x + parallax.x, rightParallax.position.y + parallax.y);

		lastPosition = transform.position;

		// Left parallax looping

		if ((transform.position.x - horizontal) < (rightParallax.position.x - rightParallaxRenderer.sprite.bounds.size.x / 2f) + 0.05f)
		{
			float temp = (rightParallax.position.x - rightParallaxRenderer.sprite.bounds.size.x / 2f) - (leftParallaxRenderer.sprite.bounds.size.x / 2f);

			if (leftParallaxSide != -1)
			{
				leftParallax.position = new Vector3(temp, leftParallax.position.y);
				leftParallaxSide = -1;
			}
		}
		else if ((transform.position.x + horizontal) > (rightParallax.position.x - rightParallaxRenderer.sprite.bounds.size.x / 2f) - 0.05f)
		{
			float temp = (rightParallax.position.x + rightParallaxRenderer.sprite.bounds.size.x / 2f) + (leftParallaxRenderer.sprite.bounds.size.x / 2f);

			if (leftParallaxSide != 1)
			{
				leftParallax.position = new Vector3(temp, leftParallax.position.y);
				leftParallaxSide = 1;
			}
		}

		// Right parallax looping

		if ((transform.position.x - horizontal) < (leftParallax.position.x - leftParallaxRenderer.sprite.bounds.size.x / 2f) + 0.05f)
		{
			float temp = (leftParallax.position.x - leftParallaxRenderer.sprite.bounds.size.x / 2f) - (rightParallaxRenderer.sprite.bounds.size.x / 2f);

			if (rightParallaxSide != -1)
			{
				rightParallax.position = new Vector3(temp, rightParallax.position.y);
				rightParallaxSide = -1;
			}
		}
		else if ((transform.position.x + horizontal) > (leftParallax.position.x - leftParallaxRenderer.sprite.bounds.size.x / 2f) - 0.05f)
		{
			float temp = (leftParallax.position.x + leftParallaxRenderer.sprite.bounds.size.x / 2f) + (rightParallaxRenderer.sprite.bounds.size.x / 2f);

			if (rightParallaxSide != 1)
			{
				rightParallax.position = new Vector3(temp, rightParallax.position.y);
				rightParallaxSide = 1;
			}
		}
	}
}