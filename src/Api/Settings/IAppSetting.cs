namespace Api.Settings;

public interface IAppSetting<T> where T : class, new()
{
    T Value { get; }
}
