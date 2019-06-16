namespace HAS.ColorConverter {
    /// <summary>
    /// A colour system is defined by the CIE x and y coordinates of
    /// its three primary illuminants and the x and y coordinates of
    /// the white point.
    /// </summary>
    public class ColourSystem {
        public string name;                     /* Colour system name */
        public double xRed, yRed,              /* Red x, y */
               xGreen, yGreen,          /* Green x, y */
               xBlue, yBlue,            /* Blue x, y */
               xWhite, yWhite,          /* White point x, y */
               gamma;                   /* Gamma correction for system */

        public static ColourSystem NTSCsystem = new ColourSystem() { name = "NTSC", xRed = 0.67, yRed = 0.33, xGreen = 0.21, yGreen = 0.71, xBlue = 0.14, yBlue = 0.08, xWhite = 0.3101, yWhite = 0.3162, gamma = 0 };
        public static ColourSystem EBUsystem = new ColourSystem() { name = "EBU (PAL/SECAM)", xRed = 0.64, yRed = 0.33, xGreen = 0.29, yGreen = 0.60, xBlue = 0.15, yBlue = 0.06, xWhite = 0.3127, yWhite = 0.3291, gamma = 0 };
        public static ColourSystem SMPTEsystem = new ColourSystem() { name = "SMPTE", xRed = 0.630, yRed = 0.340, xGreen = 0.310, yGreen = 0.595, xBlue = 0.155, yBlue = 0.070, xWhite = 0.3127, yWhite = 0.3291, gamma = 0 };
        public static ColourSystem HDTVsystem = new ColourSystem() { name = "HDTV", xRed = 0.670, yRed = 0.330, xGreen = 0.210, yGreen = 0.710, xBlue = 0.150, yBlue = 0.060, xWhite = 0.3127, yWhite = 0.3291, gamma = 0 };
        public static ColourSystem CIEsystem = new ColourSystem() { name = "CIE", xRed = 0.7355, yRed = 0.2645, xGreen = 0.2658, yGreen = 0.7243, xBlue = 0.1669, yBlue = 0.0085, xWhite = 0.33333333, yWhite = 0.33333333, gamma = 0 };
        public static ColourSystem Rec709system = new ColourSystem() { name = "CIE REC 709", xRed = 0.64, yRed = 0.33, xGreen = 0.30, yGreen = 0.60, xBlue = 0.15, yBlue = 0.06, xWhite = 0.3127, yWhite = 0.3291, gamma = 0 };
    }

    public delegate double SpectrumDelegate(double labmda);
}