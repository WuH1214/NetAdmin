using NetAdmin.Domain.DbMaps.Dependency;
using YourSolution.AdmServer.Domain.DbMaps.Adm;

namespace YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

/// <summary>
///     响应：查询女朋友
/// </summary>
public sealed record QueryGirlFriendsRsp : Adm_GirlFriends
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Adm_GirlFriends.Age"/>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Age { get; init; }

    /// <inheritdoc cref="Adm_GirlFriends.Height"/>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Height { get; init; }

    /// <inheritdoc cref="Adm_GirlFriends.Name"/>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Name { get; init; }

    /// <inheritdoc cref="Adm_GirlFriends.Hobby"/>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Hobby { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}