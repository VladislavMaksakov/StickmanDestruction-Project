
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {


    private Image joystickBG;

    public Image joystickImage;

    public Image inputArea;

    public Vector2 inputVector;

	// Use this for initialization
	void Start () {
        joystickBG = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        inputArea = transform.GetChild(1).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
     
        OnDrag(ped);
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(inputArea.rectTransform,ped.position,ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);


            inputVector = new Vector2((pos.x * 2), (pos.y * 2));
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImage.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));

        }
     
    }



}
