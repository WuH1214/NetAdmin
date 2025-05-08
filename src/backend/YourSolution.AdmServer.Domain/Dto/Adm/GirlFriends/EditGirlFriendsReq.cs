using NetAdmin.Domain.DbMaps.Dependency;

namespace YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

/// <summary>
///     请求：编辑女朋友
/// </summary>
public record EditGirlFriendsReq : CreateGirlFriendsReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}