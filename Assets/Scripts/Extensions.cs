using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public static class Extensions
    {
        public static void SetAlphaChanel(this Graphic graphic, float alpha)
        {
            var curColor = graphic.color;
            graphic.color = new Color(curColor.r, curColor.g, curColor.b, alpha);
        }
    }
}