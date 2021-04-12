using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KcmsChallengeAPP.Droid.Implementations;
using KcmsChallengeAPP.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionAndBuild))]
namespace KcmsChallengeAPP.Droid.Implementations
{
    public class VersionAndBuild : IAppVersionAndBuild
    {
        readonly PackageInfo _appInfo;

        public VersionAndBuild()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }

        public string GetBuildNumber()
        {
            return _appInfo.LongVersionCode.ToString();
        }

        public string GetVersionNumber()
        {
            return _appInfo.VersionName;
        }
    }
}