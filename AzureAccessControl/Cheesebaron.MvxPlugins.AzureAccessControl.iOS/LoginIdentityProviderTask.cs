﻿//---------------------------------------------------------------------------------
// Copyright 2013 Tomasz Cielecki (tomasz@ostebaronen.dk)
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.
//---------------------------------------------------------------------------------

using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Touch.Platform;
using Cirrious.CrossCore.Touch.Views;

namespace Cheesebaron.MvxPlugins.AzureAccessControl.iOS
{
    public class LoginIdentityProviderTask 
        : MvxTouchTask
        , ILoginIdentityProviderTask
    {
        public void LogIn(string url, Action<RequestSecurityTokenResponse> onLoggedIn, Action assumeCancelled)
        {
            var webAuthController = new AccessControlWebAuthController {RawUrl = url};

            webAuthController.FinishedLoggingIn += async (sender, args) =>
            {
                var token = await RequestSecurityTokenResponse.FromJSON(args.RequestToken);
                onLoggedIn(token);
            };
            webAuthController.Canceled += (sender, args) => assumeCancelled();

            var modalHost = Mvx.Resolve<IMvxTouchModalHost>();
            modalHost.PresentModalViewController(webAuthController, true);
        }
    }
}
