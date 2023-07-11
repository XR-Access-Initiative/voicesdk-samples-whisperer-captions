using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRAccess.Chirp;

namespace Whisperer
{
    public class SubtitlesToggle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image toggleImage;
        public Sprite onSprite;
        public Sprite offSprite;
        public Sprite onHoveredSprite;
        public Sprite offHoveredSprite;

        private bool isOn;
        private bool isHovered;

        private IEnumerator Start()
        {
            isOn = false;
            isHovered = false;
            toggleImage.sprite = offSprite;

            yield return new WaitUntil(() => (CaptionRenderManager.Instance != null && CaptionSystem.Instance != null));

            if (CaptionRenderManager.Instance.currentRenderer != null)
            {
                isOn = true;
                UpdateToggleSprite();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isOn = !isOn;
            ToggleCaptions();
            UpdateToggleSprite();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
            UpdateToggleSprite();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
            UpdateToggleSprite();
        }

        private void UpdateToggleSprite()
        {
            if (isOn)
            {
                toggleImage.sprite = isHovered ? onHoveredSprite : onSprite;
            }
            else
            {
                toggleImage.sprite = isHovered ? offHoveredSprite : offSprite;
            }
        }

        private void ToggleCaptions()
        {
            if (isOn)
            {
                CaptionRenderManager.Instance.EnableRenderer(PositioningMode.HeadLocked);
            }
            else
            {
                CaptionRenderManager.Instance.DestroyCurrentRenderer();
            }
        }
    }
}
