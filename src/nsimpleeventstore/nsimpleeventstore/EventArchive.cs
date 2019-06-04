using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace nsimpleeventstore
{
    /*
     * An event archive contains a collection of events in a single file.
     * The archive can only be written and read at once. No access to individual events in the
     * archive is possible.
     *
     * The order of events is retained in the archive.
     *
     * An event archive can be used to (de)persist events in conjunction with an
     * in-memory event store.
     */
    public static class EventArchive
    {
        public static void Write(string filename, IEnumerable<Event> events)  {
            var serializedEvents = events.Select(EventSerialization.Serialize).ToList();
            var jsonEvents = JsonConvert.SerializeObject(serializedEvents);
            File.WriteAllText(filename, jsonEvents);
        }

        
        public static IEnumerable<Event> Read(string filename)  {
            var jsonEvents = File.ReadAllText(filename);
            var serializedEvents = JsonConvert.DeserializeObject<List<string>>(jsonEvents);
            return serializedEvents.Select(EventSerialization.Deserialize);
        }
    }
}