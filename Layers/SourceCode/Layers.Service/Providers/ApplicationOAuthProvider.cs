using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Layers.Service.Models;
using Layers.Base.Consts;
using Layers.Business.Contracts.Base;
using Layers.Utilities.IOC;

namespace Layers.Service.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private IUserManager _userManager;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
            _userManager = UnityIOCContainer.Instance.ResolveType<IUserManager>();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            

            AuthenticationProperties properties = CreateProperties(user.UserName);

            //For Testing
            //using (IReadUnitOfWork readUnitOfWork = new ReadUnitOfWork())
            //{
            //    using (IWriteUnitOfWork writeUnitOfWork = new WriteUnitOfWork())
            //    {
            //        var readRepository = new ReadRepository<Read.User, int>(readUnitOfWork);
            //        var writeRepository = new WriteRepository<Write.User, int>(writeUnitOfWork);
            //        UserManager _userManager = new UserManager(readRepository, writeRepository);
            //        var _user = _userManager.GetItem(user.UserId);
            //        properties.Dictionary.Add("UserId", _user.Value.Id.ToString());
            //        properties.Dictionary.Add("LastName", _user.Value.LastName);
            //    }
            //}

            // get user 
            var _user = _userManager.GetItem(user.UserId);

            // add userId and userType to token as claims
            properties.Dictionary.Add("UserId", _user.Value.Id.ToString());
            properties.Dictionary.Add("UserType", _user.Value.UserType.ToString());

            oAuthIdentity.AddClaim(new Claim(UserClaimsConsts.UserId, user.UserId.ToString()));
            oAuthIdentity.AddClaim(new Claim(UserClaimsConsts.UserType, _user.Value.UserType.ToString()));


            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}