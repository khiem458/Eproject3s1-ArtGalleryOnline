using System.Text.Json;

namespace ArtGalleryOnline.Infrastructure
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false, // Nếu muốn định dạng JSON rõ ràng, đặt thành true
            };

            session.SetString(key, JsonSerializer.Serialize(value, options));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (value == null)
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
