using System.Runtime.InteropServices;
using YaSDK.Source.SDK.Services.Interfaces;

namespace YaSDK.Source.SDK.Services.YandexServices
{
   internal class YandexSDKLeaderboard : SingletonBehaviour<YandexSDKLeaderboard>, ILeaderboardService
   {
      [DllImport("__Internal")]
      private static extern void SetToLeaderBoardExtern(int value, string leaderboardName);

      public void SetToLeaderBoard(int value, string leaderboardName = "leaderboard") =>
         SetToLeaderBoardExtern(value, leaderboardName);
   }
}