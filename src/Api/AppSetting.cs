using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api;

public class AppSetting<T>(IOptions<T> options) : IAppSetting<T> where T : class, new()
{
    private readonly IOptions<T> _options = options;

    public T Value => _options.Value;
}
