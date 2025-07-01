using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private DeltaTimeType _deltaType;

        private readonly Dictionary<int, string> CachedNumberStrings = new();

        private int[] _frameRateSamples;
        private int _cacheNumbersAmount = 300;
        private int _averageFromAmount = 30;
        private int _averageCounter;
        private int _currentAveraged;

        public void Awake()
        {
            for (int i = 0; i < _cacheNumbersAmount; i++)
                CachedNumberStrings[i] = i.ToString();

            _frameRateSamples = new int[_averageFromAmount];
        }

        private void Update()
        {
            Sample();
            Average();
            AssignToUI();
        }

        private void Sample()
        {
            int currentFrame = (int)Math.Round(1f / _deltaType switch
            {
                DeltaTimeType.Smooth => Time.smoothDeltaTime,
                DeltaTimeType.Unscaled => Time.unscaledDeltaTime,
                _ => Time.unscaledDeltaTime
            });
            
            _frameRateSamples[_averageCounter] = currentFrame;
        }

        private void Average()
        {
            float average = _frameRateSamples.Aggregate(0f, (current, frameRate) => current + frameRate);

            _currentAveraged = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }

        private void AssignToUI()
        {
            _text.text = _currentAveraged switch
            {
                var x and >= 0 when x < _cacheNumbersAmount => CachedNumberStrings[x],
                var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                < 0 => "< 0",
                _ => "?"
            };
        }

        private enum DeltaTimeType
        {
            Smooth = 0,
            Unscaled = 1
        }
    }
}