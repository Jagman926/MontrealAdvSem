using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSelectScript : MonoBehaviour 
{

	[SerializeField]
    private Button button;
	[SerializeField]
    private Image buttonFill;
    [SerializeField]
    private Color offColor;
    [SerializeField]
    private Color onColor;
    private SpriteRenderer spriteRenderer;
    //Collider list
    private List<Collider2D> colliderList;

	//Timer
	private float timer;
	private float maxTime;

	//Fill
	private float fill;

    void Start()
    {
        colliderList = new List<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		maxTime = 1.5f;
		timer = 0;
		fill = 0;
    }

	void Update()
	{
		buttonFill.fillAmount = fill;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		timer += Time.deltaTime;
		fill = timer/maxTime;
		if(timer > maxTime)
		{
			ClickButton();
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {
		timer = 0;
		fill = 0;
    }

	private void ClickButton()
	{
		button.onClick.Invoke();
	}

	private void Fill()
	{

	}
}
