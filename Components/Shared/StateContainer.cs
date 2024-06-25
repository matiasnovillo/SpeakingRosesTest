using SpeakingRosesTest.Areas.CMS.UserBack.Entities;

namespace SpeakingRosesTest.Components.Shared
{
    public class StateContainer
    {
        public User User { get; set; } = new User();

        public bool ShowOrHideSideNav { get; set; } = true;
    }
}
