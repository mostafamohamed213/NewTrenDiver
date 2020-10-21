using Layers.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Claims;
using Layers.Base.Consts;
using System.ComponentModel;
using System.Globalization;

namespace Layers.Utilities.Users
{
    public static class UserUtility<TUId> where TUId : struct
    {
        public static User<TUId> CurrentUser
        {
            get
            {
                // If user Authenticated
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    User<TUId> currentUser = new User<TUId>();

                    ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;

                    if (identity != null)
                    {
                        // Get currentUser name
                        currentUser.UserName = identity.Name;

                        // Get user id claimn
                        Claim userIdClaim = identity.Claims.FirstOrDefault(claim => claim.Type == UserClaimsConsts.UserId);

                        // If claim is existing
                        if (userIdClaim != null)
                        {
                            TUId userId;
                            var converter = TypeDescriptor.GetConverter(typeof(TUId));

                            if (converter != null)
                            {
                                userId = (TUId)converter.ConvertFromString(userIdClaim.Value);
                                currentUser.UserId = userId;
                            }
                        }

                        // Get culture claim
                        Claim cultueclaim = identity.Claims.FirstOrDefault(claim => claim.Type == "Culture");

                        // If claim exist
                        if (cultueclaim != null)
                        {
                            // Set user culture
                            currentUser.Culture = new CultureInfo(cultueclaim.Value);
                        }

                        // Get user type claim
                        Claim userTypeClaim = identity.Claims.FirstOrDefault(Claim => Claim.Type == "UserType");

                        // If claam is existing
                        if (userTypeClaim != null) {
                            var converter = TypeDescriptor.GetConverter(typeof(int));
                            currentUser.UserType = (int)converter.ConvertFromString(userTypeClaim.Value);
                        }

                        return currentUser;
                    }
                }

                return null;

            }
        }
    }
}
