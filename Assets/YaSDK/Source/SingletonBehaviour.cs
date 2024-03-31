using UnityEngine;

namespace YaSDK.Source
{
   public abstract class SingletonBehaviour<T> : MonoBehaviour where T : class
   {
      public static T Instance { get; private set; }

      protected SingletonBehaviour() =>
         Instance = this as T;
   }
}