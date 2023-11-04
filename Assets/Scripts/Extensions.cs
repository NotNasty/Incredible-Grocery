using UnityEngine;

namespace IncredibleGrocery
{
    public static class Extensions
    {
        public static Color ChangeAlphaChanel(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}