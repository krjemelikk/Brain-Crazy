mergeInto(LibraryManager.library, {

   ConsoleLogExtern: function (message, messageType, backgroundColor, callerInfo) {
      console.log(
         `%c `, `background: ${UTF8ToString(backgroundColor)}`,
         `YandexSDK - ${UTF8ToString(messageType)}: `,
         '\n' + '\n' + `Message: ${UTF8ToString(message)}`,
         '\n' + '\n' + `The code was called by: ${UTF8ToString(callerInfo)}`);
   },
   
   GameReadyExtern: function () {
      ysdk.features.LoadingAPI.ready();
   },

   LoadEnvironmentDataExtern: function () {
      var environmentData = {
         Device: ysdk.deviceInfo.type,
         Language: ysdk.environment.i18n.lang,
         TopLevelDomain: ysdk.environment.i18n.tld,
      };

      var environmentDataJson = JSON.stringify(environmentData);
      myGameInstance.SendMessage("YandexSDK", "OnEnvironmentDataLoaded", environmentDataJson);
   },

   ShowRewardedAdExtern: function () {
      ysdk.adv.showRewardedVideo({
         callbacks: {
            onOpen: () => {
               myGameInstance.SendMessage("YandexSDK", "PauseGame");
            },

            onRewarded: () => {
               console.log("Rewarded");
               myGameInstance.SendMessage("YandexSDK", "OnRewardedAdShown");
            },

            onClose: () => {
               myGameInstance.SendMessage("YandexSDK", "UnPauseGame");
            },

            onError: (error) => {
               console.log('Error while open Rewarded ad', error);
            },
         }
      })
   },

   ShowInterstitialAdExtern: function () {
      ysdk.adv.showFullscreenAdv({
         callbacks: {
            onOpen: () => {
               myGameInstance.SendMessage("YandexSDK", "PauseGame");
            },

            onClose: (wasShown) => {
               console.log('Interstitial ad closed. Was shown?', wasShown);
               myGameInstance.SendMessage("YandexSDK", "UnPauseGame");
            },

            onError: (error) => {
               console.log('Error while open Interstitial ad:', error);
            }
         }
      })
   },

   HideAdBannerExtern: function () {
      ysdk.adv.hideBannerAdv();
   },

   ShowAdBannerExtern: function () {
      ysdk.adv.showBannerAdv();
   },

   SaveProgressExtern: function (data) {
      var json = JSON.parse(UTF8ToString(data));
      player.setData(json).then(() => {
         console.log('Progress saved');
      });
   },

   LoadProgressExtern: function () {
      player.getData().then(data => {
         var json = JSON.stringify(data);
         myGameInstance.SendMessage("YandexSDK", "OnProgressLoaded", json);
      });
   },

   SetToLeaderBoardExtern: function (value, leaderboardName) {
      ysdk.getLeaderboards().then(lb => {
         lb.setLeaderboardScore(leaderboardName, value);
      });
   },

   LoadProductDataExtern: function () {
      var CurrencyImageURL;
      var productList = [];

      payments.getCatalog().then(products => {
         for (i = 0; i < products.length; i++) {

            if (i == 0) {
               CurrencyImageURL = products[i].getPriceCurrencyImage('medium');
            }

            let productData = {
               Id: products[i].id,
               Tittle: products[i].title,
               Price: products[i].priceValue,
               ProductImageURL: products[i].imageURI,
            }

            productList.push(productData);
         }

         var dataJson = JSON.stringify({
            CurrencyImageURL: CurrencyImageURL,
            Products: productList,
         });
         myGameInstance.SendMessage("YandexSDK", "OnProductDataLoaded", dataJson);
      });
   },

   PurchaseExtern: function (id, withConsume) {
      var purchaseId = UTF8ToString(id);

      payments.purchase(purchaseId).then(purchase => {
         SendProductData(purchase);
      }).catch(err => {
         console.log('Purchase Failed', err.message);
      })
   },

   CheckPurchaseExtern: function (id, withConsume) {
      var purchaseId = UTF8ToString(id);

      payments.getPurchases().then(purchases => {
         let product = purchases.find(purchase => purchase.productID === purchaseId)

         if (product) {
            SendProductData(product);
         }
      }).catch(err => {
         console.log('Check Purchase Failed', err.message);
      })
   },

   ConsumePurchaseExtern: function (token) {
      var purchaseToken = UTF8ToString(token);
      payments.consumePurchase(purchaseToken);
   },

   SendProductData: function (product) {
      var json = JSON.stringify({
         Id: product.id,
         Token: product.purchaseToken,
      });

      let methodName = withConsume ? "OnPurchaseWithConsumeComplete" : "OnPurchaseComplete";
      myGameInstance.SendMessage("YandexSDK", methodName, json);
   },

});