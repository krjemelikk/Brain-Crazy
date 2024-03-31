#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("JHIcri09Ki95MQwori0kHK4tKBxbWwJNXFxASQJPQ0EDTVxcQElPTSoveTEiKDooOAf8RWu4WiXS2EehmzeRv24IPgbrIzGaYbByT+RnrDsMTUJIDE9JXlhFSkVPTVhFQ0IMXCgqPy55fx0/HD0qL3koJj8mbVxcU22EtNX95kqwCEc9/I+XyDcG7zMMb20cri0OHCEqJQaqZKrbIS0tLZLYX7fC/kgj51VjGPSOEtVU00fkpzWl8tVnQNkrhw4cLsQ0EtR8Jf9l9FqzHzhJjVu45QEuLy0sLY+uLUJIDE9DQkhFWEVDQl8MQ0oMWV9JHD0qL3koJj8mbVxcQEkMZUJPAh2sOAf8RWu4WiXS2EehAmyK22thU05ASQxfWE1CSE1eSAxYSV5BXwxNmRaB2CMiLL4nnQ06Alj5ECH3TjqE8FIOGeYJ+fUj+kf4jggPPduNgFxASQx+Q0NYDG9tHDI7IRwaHBgenRx0wHYoHqBEn6Mx8klf00tySZCjX61M6jd3JQO+ntRoZNxMFLI52Rq1YAFUm8Ggt/DfW7feWv5bHGPtCM7H/Ztc8yNpzQvm3UFUwcuZOztFSkVPTVhFQ0IMbVlYRENeRVhVHQaqZKrbIS0tKSksHE4dJxwlKi95h49dvmt/ee2DA22f1NfPXOHKj2ApLC+uLSMsHK4tJi6uLS0syL2FJfUaU+2refWLtZUebtf0+V2yUo1+VhyuLVocIioveTEjLS3TKCgvLi06HDgqL3koLz8hbVxcQEkMfkNDWBkeHRgcHxp2OyEfGRweHBUeHRgcAAxPSV5YRUpFT01YSQxcQ0BFT1WuLSwqJQaqZKrbT0gpLRyt3hwGKjOpr6k3tRFrG96Ft2yiAPidvD70KhwjKi95MT8tLdMoKRwvLS3THDErwFEVr6d/DP8U6J2TtmMmR9MH0HWLKSVQO2x6PTJY/5unDxdrj/lDWERDXkVYVR06HDgqL3koLz8hbVwMQ0oMWERJDFhESUIMTVxcQEVPTUujJJgM2+eAAAxDXJoTLRygm2/jubJWIIhrp3f4Ohsf5+gjYeI4Rf1ASQxlQk8CHQocCCoveSgnPzFtXByuKJccri+PjC8uLS4uLS4cISolSBkPOWc5dTGfuNvasLLjfJbtdHx+SUBFTUJPSQxDQgxYREVfDE9JXlUMTV9fWUFJXwxNT09JXFhNQk9JEQpLDKYfRtshruPyx48D1X9Gd0gCbIrba2FTJHIcMyoveTEPKDQcOiQHKi0pKSsuLToyRFhYXF8WAwNbaVIzYEd8um2l6FhOJzyvbasfpq3lNV7ZcSL5U3O33gkvlnmjYXEh3SEqJQaqZKrbIS0tKSksL64tLSxwAxyt7yokByotKSkrLi4crZo2rZ8KHAgqL3koJz8xbVxcQEkMb0leWFxASQxvSV5YRUpFT01YRUNCDG1ZI7ER3wdlBDbk0uKZlSL1cjD65xFYRUpFT01YSQxOVQxNQlUMXE1eWB8adhxOHSccJSoveSgqPy55fx0/7E8fW9sWKwB6x/YjDSL2ll81Y5kzvfcya3zHKcFyVagBxxqOe2B5wF5NT1hFT0kMX1hNWElBSUJYXwIcfIam+fbI0PwlKxucWVkN");
        private static int[] order = new int[] { 33,36,30,59,45,30,17,13,50,16,27,43,16,32,42,23,54,17,26,55,48,53,43,36,24,42,58,28,54,32,44,34,49,49,51,52,44,41,53,58,59,49,48,53,59,56,51,52,48,55,52,55,54,56,56,58,56,57,59,59,60 };
        private static int key = 44;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
