using UnityEngine;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.EditorServices
{
   public class EditorGameReadyAPI : IGameReadyAPIService
   {
      public void GameReadyAPI()
      {
         Debug.Log("<color=blue>Advertisement: </color>Game ready API called");
      }
   }
}