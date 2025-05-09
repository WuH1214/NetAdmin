using NetAdmin.Domain.DbMaps.Dependency;

namespace YourSolution.AdmServer.Domain.DbMaps.Adm;

/// <summary>
///     女朋友表
/// </summary>
[Index(Chars.FLG_DB_INDEX_PREFIX             + nameof(Name), nameof(Name), false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Adm_GirlFriends))]
public record Adm_GirlFriends : VersionEntity
{
    /// <summary>
    /// 年龄
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int Age { get; init; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    /// 身高
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int Height { get; init; }

    /// <summary>
    /// 爱好
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Hobby { get; init; }
}