using Microsoft.AspNetCore.Identity;

using Riven.Identity;
using Riven.Identity.Permissions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    public static class RivenIdentityOptionsExtensions
    {
        /// <summary>
        /// Riven Identity 配置
        /// </summary>
        /// <param name="identityOptions"></param>
        /// <returns></returns>
        public static IdentityOptions ConfigureRivenIdentity(this IdentityOptions identityOptions)
        {
            IdentityInfo.Init(identityOptions);
            return identityOptions;
        }
    }
}
