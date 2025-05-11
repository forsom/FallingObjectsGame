using UnityEngine;
using UnityEngine.UI;

public class SafeAreaManager : MonoBehaviour
{
    public float sim;
    [SerializeField] private RectTransform _uiRect;
    private RectTransform rectTransform;
    private Vector2 size;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        float widthRatio = _uiRect.rect.width / Screen.width;
        float heightRatio = _uiRect.rect.height / Screen.height;

        float offsetTop = (Screen.safeArea.yMax - Screen.height) * heightRatio;
        float offsetBottom = Screen.safeArea.yMin * heightRatio;
        float offsetLeft = Screen.safeArea.xMin * widthRatio;
        float offsetRight = (Screen.safeArea.xMax - Screen.width) * widthRatio;

        rectTransform.offsetMax = new Vector2(offsetRight, offsetTop);
        rectTransform.offsetMin = new Vector2(offsetLeft, offsetBottom);
        CanvasScaler canvasScaler = _uiRect.GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(canvasScaler.referenceResolution.x,canvasScaler.referenceResolution.y + Mathf.Abs(offsetBottom));
    }

}
