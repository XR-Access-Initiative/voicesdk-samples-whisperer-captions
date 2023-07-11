using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XRAccess.Chirp;

namespace Whisperer
{
    public class SubtitlesUI : MonoBehaviour
    {
        public TMP_Text fontSizeValue;
        public TMP_Text followLagValue;

        public Button previewButton;
        public CaptionSource previewSource;
        public string previewText;

        private CaptionOptions _options;
        private HeadLockedOptions _rendererOptions;

        [System.Serializable]
        public class FontSizeOption
        {
            public string name;
            public int size;
        }

        public FontSizeOption[] fontSizeOptions;
        private int _fontSizeIndex = 0;

        public IEnumerator Start()
        {
            yield return new WaitUntil(() => (CaptionRenderManager.Instance != null && CaptionSystem.Instance != null));

            _options = CaptionSystem.Instance.options;

            var currentrenderer = CaptionRenderManager.Instance.currentRenderer;
            _rendererOptions = (HeadLockedOptions)currentrenderer.options;

            AddToFontSize(1);
            followLagValue.text = _rendererOptions.lag.ToString("F2");
        }

        public void AddToFontSize(int value)
        {
            _fontSizeIndex = Math.Abs(_fontSizeIndex + value) % fontSizeOptions.Length;
            _options.fontSize = fontSizeOptions[_fontSizeIndex].size;
            fontSizeValue.text = fontSizeOptions[_fontSizeIndex].name;
            CaptionRenderManager.Instance.RefreshCaptions();
        }

        public void AddToLag(float value)
        {
            if (_rendererOptions.lag + value < 0)
                return;

            _rendererOptions.lag += value;
            followLagValue.text = _rendererOptions.lag.ToString("F2");
            CaptionRenderManager.Instance.RefreshCaptions();
        }

        public void PreviewSubtitles()
        {
            previewSource.ShowTimedCaption(previewText, 5f);
        }
    }
}