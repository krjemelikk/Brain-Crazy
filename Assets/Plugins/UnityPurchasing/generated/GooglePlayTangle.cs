#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("f/xVGwaP7CzUD3KhHUs2QbwieS+XII+8WxM/b3cKn5A3BM7aUSfeIFV5UzqkH3A/uaGAvqBJPHSQqAA295FnMwwFU9gdkU0rwxH1CDXmtLOA1vJkk8DPztSY0CVJT8nRxrMzy5vuPlhrs/izfnhQixxb8JE3h2B+DCa15SBvICKRByhsNVQQfwekxS30t8A5aKTZNXp+KNPAmxR7NJpLOzY6DH75Me0TAA2HrRDiimJbJMUnUCIe/iJV0/G+4NwsvrXSmPp+pc5Mz8HO/kzPxMxMz8/OTtNMk9CuB4oArCLCMSsn8X2Mu8WByrLacviAVVm5YJ0zR9VAus2yNYstji/PiTH+TM/s/sPIx+RIhkg5w8/Pz8vOzTyCZazW9dJLy8zNz87P");
        private static int[] order = new int[] { 12,6,2,11,11,7,7,8,13,10,13,13,13,13,14 };
        private static int key = 206;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
