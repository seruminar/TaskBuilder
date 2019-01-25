using System;

using CMS.Membership;

using TaskBuilder.Attributes;
using TaskBuilder.ValueBuilders;

namespace TaskBuilder.Functions.Implementations
{
    [Function("Get user")]
    public struct GetUserFunction : IInvokable, IDispatcher2
    {
        public void Invoke()
        {
            // Receive parameters
            var userIdentifier = UserIdentifier();

            // Set up parameters
            int userId;
            int.TryParse(userIdentifier, out userId);

            if (userId > 0)
            {
                UserInfo = UserInfoProvider.GetUserInfo(userId);
            }

            UserInfo = UserInfoProvider.GetUserInfo(userIdentifier);

            if (UserInfo != null)
            {
                Dispatch();
            }
            else
            {
                Dispatch2();
            }
        }

        [Dispatch("User found")]
        public Action Dispatch { get; set; }

        [Dispatch("User not found")]
        public Action Dispatch2 { get; set; }

        [Input(typeof(StringValueBuilder), new object[] { "userIdentifier" })]
        public Func<string> UserIdentifier { get; set; }

        [Output]
        public UserInfo UserInfo { get; set; }
    }
}