﻿using UnityEngine;

namespace HAS.ColorConverter {
    public class Global {
        /// <summary>
        /// Given 1976 coordinates u', v', determine 1931 chromaticities x, y
        /// </summary>
        /// <param name="up"></param>
        /// <param name="vp"></param>
        /// <param name="xc"></param>
        /// <param name="yc"></param>
        public static void upvp_to_xy(double up, double vp, ref double xc, ref double yc) {
            xc = (9 * up) / ((6 * up) - (16 * vp) + 12);
            yc = (4 * vp) / ((6 * up) - (16 * vp) + 12);
        }

        /// <summary>
        /// Given 1931 chromaticities x, y, determine 1976 coordinates u', v'
        /// </summary>
        /// <param name="xc"></param>
        /// <param name="yc"></param>
        /// <param name="up"></param>
        /// <param name="vp"></param>
        public static void xy_to_upvp(double xc, double yc, ref double up, ref double vp) {
            up = (4 * xc) / ((-2 * xc) + (12 * yc) + 3);
            vp = (9 * yc) / ((-2 * xc) + (12 * yc) + 3);
        }

        /// <summary>
        ///Given an additive tricolour system CS, defined by the CIE x
        ///and y chromaticities of its three primaries (z is derived
        ///trivially as 1-(x+y)), and a desired chromaticity (XC, YC,
        ///ZC) in CIE space, determine the contribution of each
        ///primary in a linear combination which sums to the desired
        ///chromaticity.  If the  requested chromaticity falls outside
        ///the Maxwell  triangle (colour gamut) formed by the three
        ///primaries, one of the r, g, or b weights will be negative.
        ///Caller can use constrain_rgb() to desaturate an
        ///outside-gamut colour to the closest representation within
        ///the available gamut and/or norm_rgb to normalise the RGB
        ///components so the largest nonzero component has value 1.
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="xc"></param>
        /// <param name="yc"></param>
        /// <param name="zc"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void xyz_to_rgb(ColourSystem cs,
                double xc, double yc, double zc,
                ref double r, ref double g, ref double b) {
            double xr, yr, zr, xg, yg, zg, xb, yb, zb;
            double xw, yw, zw;
            double rx, ry, rz, gx, gy, gz, bx, by, bz;
            double rw, gw, bw;

            xr = cs.xRed; yr = cs.yRed; zr = 1 - (xr + yr);
            xg = cs.xGreen; yg = cs.yGreen; zg = 1 - (xg + yg);
            xb = cs.xBlue; yb = cs.yBlue; zb = 1 - (xb + yb);

            xw = cs.xWhite; yw = cs.yWhite; zw = 1 - (xw + yw);

            /* xyz . rgb matrix, before scaling to white. */
            rx = (yg * zb) - (yb * zg); ry = (xb * zg) - (xg * zb); rz = (xg * yb) - (xb * yg);
            gx = (yb * zr) - (yr * zb); gy = (xr * zb) - (xb * zr); gz = (xb * yr) - (xr * yb);
            bx = (yr * zg) - (yg * zr); by = (xg * zr) - (xr * zg); bz = (xr * yg) - (xg * yr);

            /* White scaling factors.
               Dividing by yw scales the white luminance to unity, as conventional. */
            rw = ((rx * xw) + (ry * yw) + (rz * zw)) / yw;
            gw = ((gx * xw) + (gy * yw) + (gz * zw)) / yw;
            bw = ((bx * xw) + (by * yw) + (bz * zw)) / yw;

            /* xyz . rgb matrix, correctly scaled to white. */
            rx = rx / rw; ry = ry / rw; rz = rz / rw;
            gx = gx / gw; gy = gy / gw; gz = gz / gw;
            bx = bx / bw; by = by / bw; bz = bz / bw;

            /* rgb of the desired point */
            r = (rx * xc) + (ry * yc) + (rz * zc);
            g = (gx * xc) + (gy * yc) + (gz * zc);
            b = (bx * xc) + (by * yc) + (bz * zc);
        }

        /// <summary>
        /// Test whether a requested colour is within the gamut
        /// achievable with the primaries of the current colour
        /// system.This amounts simply to testing whether all the
        /// primary weights are non-negative.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int inside_gamut(double r, double g, double b) {
            return (r >= 0) && (g >= 0) && (b >= 0) ? 1 : 0;
        }

        /// <summary>
        ///If the requested RGB shade contains a negative weight for
        ///one of the primaries, it lies outside the colour gamut
        ///accessible from the given triple of primaries.Desaturate
        ///it by adding white, equal quantities of R, G, and B, enough
        ///to make RGB all positive.  The function returns 1 if the
        ///components were modified, zero otherwise.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int constrain_rgb(ref double r, ref double g, ref double b) {
            double w;

            /* Amount of white needed is w = - min(0, *r, *g, *b) */
            w = (0 < r) ? 0 : r;
            w = (w < g) ? w : g;
            w = (w < b) ? w : b;
            w = -w;

            /* Add just enough white to make r, g, b all positive. */
            if (w > 0) {
                r += w; g += w; b += w;
                return 1;                     /* Colour modified to fit RGB gamut */
            }
            return 0;                         /* Colour within RGB gamut */
        }

        /// <summary>
        ///Transform linear RGB values to nonlinear RGB values.Rec.
        ///709 is ITU-R Recommendation BT. 709 (1990) ``Basic
        ///Parameter Values for the HDTV Standard for the Studio and
        ///for International Programme Exchange'', formerly CCIR Rec.
        ///709. For details see
        ///http://www.poynton.com/ColorFAQ.html
        ///http://www.poynton.com/GammaFAQ.html
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="c"></param>
        public static void gamma_correct(ColourSystem cs, ref double c) {
            double gamma;

            gamma = cs.gamma;

            if (gamma == 0) {
                /* Rec. 709 gamma correction. */
                double cc = 0.018;

                if (c < cc) {
                    c *= ((1.099 * System.Math.Pow(cc, 0.45)) - 0.099) / cc;
                }
                else {
                    c = (1.099 * System.Math.Pow(c, 0.45)) - 0.099;
                }
            }
            else {
                /* Nonlinear colour = (Linear colour)^(1/gamma) */
                c = System.Math.Pow(c, 1.0 / gamma);
            }
        }

        public static void gamma_correct_rgb(ColourSystem cs, ref double r, ref double g, ref double b) {
            gamma_correct(cs, ref r);
            gamma_correct(cs, ref g);
            gamma_correct(cs, ref b);
        }

        /// <summary>
        ///Normalise RGB components so the most intense(unless all    
        ///are zero) has a value of 1.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void norm_rgb(ref double r, ref double g, ref double b) {
            double greatest = System.Math.Max(r, System.Math.Max(g, b));
            if (greatest > 0) {
                r /= greatest;
                g /= greatest;
                b /= greatest;
            }
        }

        public const double minLambda = 380;
        public const double maxLambda = 780;

        /// <summary>
        ///Calculate the CIE X, Y, and Z coordinates corresponding to
        ///a light source with spectral distribution given by  the
        ///function SPEC_INTENS, which is called with a series of
        ///wavelengths between 380 and 780 nm(the argument is
        ///expressed in meters), which returns emittance at  that
        ///wavelength in arbitrary units.The chromaticity
        ///coordinates of the spectrum are returned in the x, y, and z
        ///arguments which respect the identity:
        /// </summary>
        /// <param name="spec_intens"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void spectrum_to_xyz(SpectrumDelegate spec_intens, ref double x, ref double y, ref double z) {
            int i;
            double lambda, X = 0, Y = 0, Z = 0, XYZ;

            /* CIE colour matching functions xBar, yBar, and zBar for
               wavelengths from 380 through 780 nanometers, every 5
               nanometers.  For a wavelength lambda in this range:

                    cie_colour_match[(lambda - 380) / 5][0] = xBar
                    cie_colour_match[(lambda - 380) / 5][1] = yBar
                    cie_colour_match[(lambda - 380) / 5][2] = zBar

                To save memory, this table can be declared as floats
                rather than doubles; (IEEE) float has enough
                significant bits to represent the values. It's declared
                as a double here to avoid warnings about "conversion
                between floating-point types" from certain persnickety
                compilers. */

            double[,] cie_colour_match = new double[,] {
        {0.0014,0.0000,0.0065}, {0.0022,0.0001,0.0105}, {0.0042,0.0001,0.0201},
        {0.0076,0.0002,0.0362}, {0.0143,0.0004,0.0679}, {0.0232,0.0006,0.1102},
        {0.0435,0.0012,0.2074}, {0.0776,0.0022,0.3713}, {0.1344,0.0040,0.6456},
        {0.2148,0.0073,1.0391}, {0.2839,0.0116,1.3856}, {0.3285,0.0168,1.6230},
        {0.3483,0.0230,1.7471}, {0.3481,0.0298,1.7826}, {0.3362,0.0380,1.7721},
        {0.3187,0.0480,1.7441}, {0.2908,0.0600,1.6692}, {0.2511,0.0739,1.5281},
        {0.1954,0.0910,1.2876}, {0.1421,0.1126,1.0419}, {0.0956,0.1390,0.8130},
        {0.0580,0.1693,0.6162}, {0.0320,0.2080,0.4652}, {0.0147,0.2586,0.3533},
        {0.0049,0.3230,0.2720}, {0.0024,0.4073,0.2123}, {0.0093,0.5030,0.1582},
        {0.0291,0.6082,0.1117}, {0.0633,0.7100,0.0782}, {0.1096,0.7932,0.0573},
        {0.1655,0.8620,0.0422}, {0.2257,0.9149,0.0298}, {0.2904,0.9540,0.0203},
        {0.3597,0.9803,0.0134}, {0.4334,0.9950,0.0087}, {0.5121,1.0000,0.0057},
        {0.5945,0.9950,0.0039}, {0.6784,0.9786,0.0027}, {0.7621,0.9520,0.0021},
        {0.8425,0.9154,0.0018}, {0.9163,0.8700,0.0017}, {0.9786,0.8163,0.0014},
        {1.0263,0.7570,0.0011}, {1.0567,0.6949,0.0010}, {1.0622,0.6310,0.0008},
        {1.0456,0.5668,0.0006}, {1.0026,0.5030,0.0003}, {0.9384,0.4412,0.0002},
        {0.8544,0.3810,0.0002}, {0.7514,0.3210,0.0001}, {0.6424,0.2650,0.0000},
        {0.5419,0.2170,0.0000}, {0.4479,0.1750,0.0000}, {0.3608,0.1382,0.0000},
        {0.2835,0.1070,0.0000}, {0.2187,0.0816,0.0000}, {0.1649,0.0610,0.0000},
        {0.1212,0.0446,0.0000}, {0.0874,0.0320,0.0000}, {0.0636,0.0232,0.0000},
        {0.0468,0.0170,0.0000}, {0.0329,0.0119,0.0000}, {0.0227,0.0082,0.0000},
        {0.0158,0.0057,0.0000}, {0.0114,0.0041,0.0000}, {0.0081,0.0029,0.0000},
        {0.0058,0.0021,0.0000}, {0.0041,0.0015,0.0000}, {0.0029,0.0010,0.0000},
        {0.0020,0.0007,0.0000}, {0.0014,0.0005,0.0000}, {0.0010,0.0004,0.0000},
        {0.0007,0.0002,0.0000}, {0.0005,0.0002,0.0000}, {0.0003,0.0001,0.0000},
        {0.0002,0.0001,0.0000}, {0.0002,0.0001,0.0000}, {0.0001,0.0000,0.0000},
        {0.0001,0.0000,0.0000}, {0.0001,0.0000,0.0000}, {0.0000,0.0000,0.0000}
    };

            for (i = 0, lambda = 380; lambda < 780.1; i++, lambda += 5) {
                double Me;

                Me = spec_intens(lambda);
                X += Me * cie_colour_match[i, 0];
                Y += Me * cie_colour_match[i, 1];
                Z += Me * cie_colour_match[i, 2];
            }
            XYZ = (X + Y + Z);
            x = X / XYZ;
            y = Y / XYZ;
            z = Z / XYZ;
        }

        /// <summary>
        ///Calculate, by Planck's radiation law, the emittance of a black body
        ///of temperature bbTemp at the given wavelength(in metres).
        /// </summary>
        /// <param name="wavelength"></param>
        /// <returns></returns>
        public static double BlackBodySpectrum(double wavelength) {
            double wlm = wavelength * 1e-9;   /* Wavelength in meters */
            return (3.74183e-16 * System.Math.Pow(wlm, -5.0)) /
                   (System.Math.Exp(1.4388e-2 / (wlm * 5000)) - 1.0);
        }

        public static SpectrumDelegate GenerateDeltaSpectrum(double lambda) {
            return new SpectrumDelegate(wavelength => {
                return 4 * System.Math.Exp(-16 * System.Math.Pow(wavelength - lambda, 2)) / System.Math.Sqrt(System.Math.PI);
            });
        }

        /// <summary>
        /// Convert wavelength in nm to color
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static Color ConvertWavelengthToColor(double lambda) {
            double x = 0;
            double y = 0;
            double z = 0;
            double r = 0;
            double g = 0;
            double b = 0;

            spectrum_to_xyz(GenerateDeltaSpectrum(lambda), ref x, ref y, ref z);
            xyz_to_rgb(ColourSystem.SMPTEsystem, x, y, z, ref r, ref g, ref b);
            if (constrain_rgb(ref r, ref g, ref b) > 0) {
                norm_rgb(ref r, ref g, ref b);
            }
            else {
                norm_rgb(ref r, ref g, ref b);
            }

            return new Color((float)r,(float)g,(float)b);
        }
    }
}