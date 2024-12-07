using System.Collections.Concurrent;

namespace AOC24;

public class InputScraper : IInputSource
{
    private const string CachePath = @".\cache";
    private readonly string _session = Environment.GetEnvironmentVariable("AOC24", EnvironmentVariableTarget.User) 
                                       ?? throw new NullReferenceException("Session environment variable not set!");
    private bool _hasLazyLoaded;
    
    private HttpClient _client = new()
    {
        BaseAddress = new Uri("https://adventofcode.com/2024/day/"),
    };

    private readonly ConcurrentDictionary<int, string> _cache = [];

    public InputScraper()
    {
        _client.DefaultRequestHeaders.Add("Cookie", $"session={_session}");
        
        if (!Directory.Exists(CachePath))
        {
            Directory.CreateDirectory(CachePath);
        }
    }

    public async ValueTask<string> GetInput(int day, CancellationToken token = default)
    {
        if (!_hasLazyLoaded)
        {
            await LoadCache();
        }

        if (_cache.TryGetValue(day, out var input))
        {
            return input;
        }

        using var response = await _client.GetAsync(@$"{day}/input", token);
        response.EnsureSuccessStatusCode();

        input = await response.Content.ReadAsStringAsync(token);
        _cache.TryAdd(day, input);

        await FlushCacheToFile();

        return input;
    }

    private async Task LoadCache()
    {
        var files = Directory.EnumerateFiles(CachePath);

        List<Task> loadTasks = [];

        foreach (var file in files)
        {
            var fileName = file.Split('\\').Last();
            var key = int.Parse(fileName);
            
            var readTask = Task.Run(async () =>
            {
                var text = await File.ReadAllTextAsync(file);
                _cache.TryAdd(key,text.TrimEnd('\n'));
            });
            
            loadTasks.Add(readTask);
        }

        await Task.WhenAll(loadTasks);
        _hasLazyLoaded = true;
    }

    private async Task FlushCacheToFile()
    {
        List<Task> flushTasks = [];

        foreach (var kvp in _cache)
        {
            if (!File.Exists(kvp.Key.ToString()))
            {
                var writeTask = File.WriteAllTextAsync(@$"{CachePath}/{kvp.Key}", kvp.Value);
                flushTasks.Add(writeTask);
            }
        }

        await Task.WhenAll(flushTasks);
    }
}