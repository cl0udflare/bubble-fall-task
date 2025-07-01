using System;
using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using UnityEngine;

namespace Gameplay.Core.Ball.Utils
{
    public static class BallColorUtils
    {
        public static List<BallColor> Colors() => 
            new((BallColor[])Enum.GetValues(typeof(BallColor)));

        public static Color ToColor(BallColor color)
        {
            return color switch
            {
                BallColor.Red => Color.red,
                BallColor.Green => Color.green,
                BallColor.Blue => Color.blue,
                BallColor.Yellow => Color.yellow,
                BallColor.Purple => new Color(0.6f, 0f, 0.8f),
                BallColor.Orange => new Color(1f, 0.5f, 0f),
                _ => Color.white
            };
        }
    }
}