using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 待修改。
/// </summary>
public class PerlinNoiseGenerator {


    private static float m_persistence = 0.5f; //持续度
    private static int m_octaves = 4; //倍频，循环次数，越大细节描述约清楚

    /// <summary>
    /// 获取一个（-1，1）之间的随机数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static float Noise(int x, int y)    // 根据(x,y)获取一个初步噪声值
    {
        int n = x + y * 57;
        n = ( n << 13 ) ^ n;
        return (float)( 1.0 - ( ( n * ( n * n * 15731 + 789221 ) + 1376312589 ) & 0x7fffffff ) / 1073741824.0 );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    static float SmoothedNoise(int x, int y)   //光滑噪声
    {
        float corners = ( Noise( x - 1, y - 1 ) + Noise( x + 1, y - 1 ) + Noise( x - 1, y + 1 ) + Noise( x + 1, y + 1 ) ) / 16;
        float sides = ( Noise( x - 1, y ) + Noise( x + 1, y ) + Noise( x, y - 1 ) + Noise( x, y + 1 ) ) / 8;
        float center = Noise( x, y ) / 4;
        return corners + sides + center;
    }
    static float Cosine_Interpolate(double a, double b, double x)  // 余弦插值
    {
        double ft = x * 3.1415927;
        double f = ( 1 - Mathf.Cos( (float)ft ) ) * 0.5;
        return (float)( a * ( 1 - f ) + b * f );
    }

    static double InterpolatedNoise(float x, float y)   // 获取插值噪声
    {
        int integer_X = (int)x;
        float fractional_X = x - integer_X;
        int integer_Y = (int)y;
        float fractional_Y = y - integer_Y;
        double v1 = SmoothedNoise( integer_X, integer_Y );
        double v2 = SmoothedNoise( integer_X + 1, integer_Y );
        double v3 = SmoothedNoise( integer_X, integer_Y + 1 );
        double v4 = SmoothedNoise( integer_X + 1, integer_Y + 1 );
        double i1 = Cosine_Interpolate( v1, v2, fractional_X );
        double i2 = Cosine_Interpolate( v3, v4, fractional_X );
        return Cosine_Interpolate( i1, i2, fractional_Y );
    }

    public static double PerlinNoise(float x, float y)    // 最终调用：根据(x,y)获得其对应的PerlinNoise值
    {
        double result = 0;
        for (int i = 0; i < m_octaves; i++) {
            double frequency = Mathf.Pow( 2, i );
            double amplitude = Mathf.Pow( (float)m_persistence, i );
            result = result + InterpolatedNoise( x * (float)frequency, y * (float)frequency ) * amplitude;
        }

        return result;
    }


}
