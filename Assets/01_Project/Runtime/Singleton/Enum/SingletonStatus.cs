public enum SingletonStatus
{
    /// <summary>
    /// 未初始化
    /// </summary>
    None,
    /// <summary>
    /// 開始初始化
    /// </summary>
    Init,
    /// <summary>
    /// 存活
    /// </summary>
    Live,
    /// <summary>
    /// 關閉或刪除
    /// </summary>
    ShuttingDown
}
