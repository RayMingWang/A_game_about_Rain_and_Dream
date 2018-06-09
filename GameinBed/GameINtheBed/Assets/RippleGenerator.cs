using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleGenerator : MonoBehaviour {

    public int resolution = 256;
    public float perlin_scale = 1f;
    public float speed = 0.5f;
    public float thickness = 0.1f;
    public float completness = 0.1f;

    float start_Time;
    Texture m_Texture;
	// Use this for initialization
	void Start () {
        start_Time = Time.time;
        Random.InitState((int)System.DateTime.Now.Ticks);
        Debug.Log(start_Time);

        m_Texture = transform.GetComponent<Renderer>().sharedMaterial.mainTexture;
	}
    public Texture2D Color_Map()
    {


        
        Color[] colorMap = new Color[resolution * resolution];

        float color_perlin_offset = Random.value;
        {

            for (int w = 0; w < resolution; w++)
            {
                for (int h = 0; h < resolution; h++)
                {

                    if (
                        ((w - resolution/2) * (w - resolution / 2) + (h - resolution / 2) * (h - resolution / 2)) >
                         //speed* (Time.time-start_Time)
                         122 * 122
                        )
                    {
                        if (((w - resolution / 2) * (w - resolution / 2) + (h - resolution / 2) * (h - resolution / 2)) <
                         //speed * (Time.time - start_Time)+ thickness
                         122 * 122 + thickness
                         ) 
                        {
                            float perlineNoise = Mathf.PerlinNoise(w * perlin_scale, h * perlin_scale);
                            if (perlineNoise <= completness)
                            {
                                colorMap[w * resolution + h] = GoldenColor.GenerateColor(35f,95f,1f);
                                //colorMap[w * resolution + h] = new Color(1, 1, 1);
                                continue;
                            }
                        }
                    }

                    colorMap[w * resolution + h] = new Color(0, 0, 0);
                    


                }
            }
        }




        Texture2D texture = TextureFromColorMap(colorMap);
        return texture;
    }

    public Texture2D TextureFromColorMap(Color[] colorMap)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        texture.filterMode = FilterMode.Trilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
    // Update is called once per frame
    void Update () {
       Renderer textureRender = GetComponent<Renderer>();
        textureRender.sharedMaterial.mainTexture = Color_Map();
	}
}
