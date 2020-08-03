using BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement;

namespace BeltsAndLeaders.Server.Business.Commands.Achievements.CreateAchievement
{
    public interface ICreateAchievementCommand : ICommand<CreateAchievementCommandRequestModel, ulong> { }
}
