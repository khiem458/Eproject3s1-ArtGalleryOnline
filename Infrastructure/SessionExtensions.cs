using System.Text.Json;

namespace ArtGalleryOnline.Infrastructure
{
    public static class SessionExtensions
    {
<<<<<<< HEAD
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
=======
        public static void SetJson(this ISession session,string key,object value)
        {
            session.SetString(key,JsonSerializer.Serialize(value));
        }
        public static T? GetJson<T>(this ISession session,string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null? default(T) : JsonSerializer.Deserialize<T>(sessionData);
>>>>>>> 37403b2cf1321335730aaed83669456188fe7316
        }
    }
}
