using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text targetText; // 또는 public TMPro.TextMeshProUGUI targetText; 

    [Header("색상 설정")]
    public Color normalColor = Color.white; // 평상시 기본 색상
    public Color hoverColor = Color.yellow; // 마우스를 올렸을 때 기본 색상

    [Range(0f, 1f)]
    public float normalAlpha = 150f / 255f; // 150/255 = 0.588...
    [Range(0f, 1f)]
    public float hoverAlpha = 1f; // 255/255 = 1.0

    void Start()
    {
        // 텍스트 컴포넌트가 인스펙터에서 할당되지 않았다면, 이 오브젝트에서 찾습니다.
        if (targetText == null)
        {
            targetText = GetComponentInChildren<Text>();
        }

        // 시작 시 기본 색상과 지정된 알파 값으로 설정합니다.
        if (targetText != null)
        {
            Color startColor = normalColor;
            startColor.a = normalAlpha; // 알파 값 적용
            targetText.color = startColor;
        }
    }

    // 마우스 포인터가 버튼 위로 진입했을 때 호출됩니다 (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetText != null)
        {
            Color newColor = hoverColor;
            newColor.a = hoverAlpha; // 알파 값 적용 (255)
            targetText.color = newColor;
        }
    }

    // 마우스 포인터가 버튼 밖으로 나갔을 때 호출됩니다
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetText != null)
        {
            Color newColor = normalColor;
            newColor.a = normalAlpha; // 알파 값 적용 (150)
            targetText.color = newColor;
        }
    }
}