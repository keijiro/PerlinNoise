#pragma strict

//
// Perlin Noise and fBm functions for Unity
// https://github.com/keijiro/unity-perlin
//
// Based on the original implementation by Ken Perlin
// http://mrl.nyu.edu/~perlin/noise/
//
// Feel free to use this code in your projects.
//

static class Perlin {
    function Noise(x : float) {
        var X : byte = Mathf.FloorToInt(x);
        x -= Mathf.Floor(x);
        var u = fade(x);
        return lerp(u, (perm[X  ] & 1) ? x     :    -x,
                       (perm[X+1] & 1) ? x-1.0 : 1.0-x);
    }

    function Noise(coord : Vector2) {
        var X : byte = Mathf.FloorToInt(coord.x);
        var Y : byte = Mathf.FloorToInt(coord.y);
        var x = coord.x - Mathf.Floor(coord.x);
        var y = coord.y - Mathf.Floor(coord.y);
        var u = fade(x);
        var v = fade(y);
        var A  : byte = perm[X  ] + Y;
        var B  : byte = perm[X+1] + Y;
        return lerp(v, lerp(u, grad2d(perm[A  ], x  , y  ),
                               grad2d(perm[B  ], x-1, y  )),
                       lerp(u, grad2d(perm[A+1], x  , y-1),
                               grad2d(perm[B+1], x-1, y-1)));
    }

    function Noise(coord : Vector3) {
        var X : byte = Mathf.FloorToInt(coord.x);
        var Y : byte = Mathf.FloorToInt(coord.y);
        var Z : byte = Mathf.FloorToInt(coord.z);
        var x = coord.x - Mathf.Floor(coord.x);
        var y = coord.y - Mathf.Floor(coord.y);
        var z = coord.z - Mathf.Floor(coord.z);
        var u = fade(x);
        var v = fade(y);
        var w = fade(z);
        var A  : byte = perm[X  ] + Y;
        var B  : byte = perm[X+1] + Y;
        var AA : byte = perm[A  ] + Z;
        var BA : byte = perm[B  ] + Z;
        var AB : byte = perm[A+1] + Z;
        var BB : byte = perm[B+1] + Z;
        return lerp(w, lerp(v, lerp(u, grad3d(perm[AA  ], x  , y  , z   ),
                                       grad3d(perm[BA  ], x-1, y  , z   )),
                               lerp(u, grad3d(perm[AB  ], x  , y-1, z   ),
                                       grad3d(perm[BB  ], x-1, y-1, z   ))),
                       lerp(v, lerp(u, grad3d(perm[AA+1], x  , y  , z-1 ),
                                       grad3d(perm[BA+1], x-1, y  , z-1 )),
                               lerp(u, grad3d(perm[AB+1], x  , y-1, z-1 ),
                                       grad3d(perm[BB+1], x-1, y-1, z-1 ))));
    }

    function Fbm(x : float, octave : int) {
        var f = 0.0;
        var w = 0.5;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(x);
            x *= 2.0;
            w *= 0.5;
        }
        return f;
    }

    function Fbm(coord : Vector2, octave : int) {
        var f = 0.0;
        var w = 0.5;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(coord);
            coord *= 2.0;
            w *= 0.5;
        }
        return f;
    }

    function Fbm(coord : Vector3, octave : int) {
        var f = 0.0;
        var w = 0.5;
        for (var i = 0; i < octave; i++) {
            f += w * Noise(coord);
            coord *= 2.0;
            w *= 0.5;
        }
        return f;
    }

    private function fade(t : float) {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private function lerp(t : float, a : float, b : float) {
        return a + t * (b - a);
    }

    private function grad2d(hash : int, x : float, y : float) {
        return ((hash & 1) ? x : -x) + ((hash & 2) ? y : -y);
    }

    private function grad3d(hash : int, x : float, y : float, z : float) {
        var h = hash & 15;
        var u = h < 8 ? x : y;
        var v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
        return ((h & 1) ? u : -u) + ((h & 2) ? v : -v);
    }

    private var perm = [151,160,137,91,90,15,
        131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
        190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
        88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
        77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
        102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
        135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
        5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
        223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
        129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
        251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
        49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
        138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
        151];
}