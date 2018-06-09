using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenColor : MonoBehaviour {
    const float golden_ratio_conjugate = 0.618033988749895f;
	public static Color GenerateColor(float saturation,float value, float trans)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        float hue = Random.value;
        hue += golden_ratio_conjugate;
        hue = hue % 1f;


        float H = hue*360f;
        float S = saturation;
        float V = value;
        /*
        while (H < 0) { H += 360; };
        while (H >= 360) { H -= 360; };
        */
        float R, G, B;
        if (V <= 0)
        { R = G = B = 0; }
        else if (S <= 0)
        {
            R = G = B = V;
        }
        else
        {
            float hf = H / 60.0f;
            int i = (int)Mathf.Floor(hf);
            float f = hf - i;
            float pv = V * (1 - S);
            float qv = V * (1 - S * f);
            float tv = V * (1 - S * (1 - f));
            switch (i)
            {

                // Red is the dominant color

                case 0:
                    R = V;
                    G = tv;
                    B = pv;
                    break;

                // Green is the dominant color

                case 1:
                    R = qv;
                    G = V;
                    B = pv;
                    break;
                case 2:
                    R = pv;
                    G = V;
                    B = tv;
                    break;

                // Blue is the dominant color

                case 3:
                    R = pv;
                    G = qv;
                    B = V;
                    break;
                case 4:
                    R = tv;
                    G = pv;
                    B = V;
                    break;

                // Red is the dominant color

                case 5:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                case 6:
                    R = V;
                    G = tv;
                    B = pv;
                    break;
                case -1:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // The color is not defined, we should throw an error.

                default:
                    //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                    R = G = B = V; // Just pretend its black/white
                    break;
            }
        }
        float r = (float)Clamp((int)(R * 255.0f));
        float g = (float)Clamp((int)(G * 255.0f));
        float b = (float)Clamp((int)(B * 255.0f));

       
        return new Color(R,G,B,trans);

    }



    /// <summary>
    /// Clamp a value to 0-255
    /// </summary>
    static int Clamp(int i)
    {
        if (i < 0) return 0;
        if (i > 255) return 255;
        return i;
    }
}
