float2 hash( float2 p )
{
    //p = mod(p, 4.0); // tile
    p = float2(dot(p,float2(127.1,311.7)),
    dot(p,float2(269.5,183.3)));
    return frac(sin(p)*18.5453);
}

// return distance, and cell id
float2 voronoi( in float2 x )
{
    float2 n = floor( x );
    float2 f = frac( x );

    float3 m = 8;
    for( int j=-1; j<=1; j++ )
    for( int i=-1; i<=1; i++ )
    {
        float2  g = float2( float(i), float(j) );
        float2  o = hash( n + g );
        float2  r = g - f + o;
        // float2  r = g - f + (0.5+0.5*sin(_Time.y+6.2831*o));
        float d = dot( r, r );
        if( d<m.x )
        m = float3( d, o );
    }

    return float2( sqrt(m.x), m.y+m.z );
}

float2 grad( float2 z )  // replace this anything that returns a random vector
{
    // 2D to 1D  (feel free to replace by some other)
    int n = z.x+z.y*11111;

    // Hugo Elias hash (feel free to replace by another one)
    n = (n<<13)^n;
    n = (n*(n*n*15731+789221)+1376312589)>>16;

    return float2(cos(float(n)),sin(float(n)));                    
}

float gnoise( in float2 p )
{
    float2 i = float2(floor( p ));
    float2 f =       frac( p );
    
    float2 u = f*f*(3.0-2.0*f); // feel free to replace by a quintic smoothstep instead

    return lerp( lerp( dot( grad( i+float2(0,0) ), f-float2(0.0,0.0) ), 
    dot( grad( i+float2(1,0) ), f-float2(1.0,0.0) ), u.x),
    lerp( dot( grad( i+float2(0,1) ), f-float2(0.0,1.0) ), 
    dot( grad( i+float2(1,1) ), f-float2(1.0,1.0) ), u.x), u.y) + 0.5;
}