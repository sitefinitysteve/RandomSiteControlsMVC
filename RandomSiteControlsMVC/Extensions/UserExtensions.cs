using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Modules.UserProfiles;

namespace Telerik.Sitefinity.Security
{
    public static class UserExtensions
    {
        public static string GetAvatar(this Telerik.Sitefinity.Security.Model.User user, string defaultImageUrl = "")
        {
            Image image;
            UserProfilesHelper.GetAvatarImageUrl(user.Id, out image);

            if (image != null)
            {
                return image.Url;
            }
            else
            {
                return (String.IsNullOrEmpty(defaultImageUrl)) ? "/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png" : defaultImageUrl;
            }
        }
    }
}