using System;
using System.Collections.Generic;
using System.Text;

namespace KcmsChallengeAPP.Interfaces
{
    public interface IAppVersionAndBuild
    {
        string GetVersionNumber();
        string GetBuildNumber();
    }
}
