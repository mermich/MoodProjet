using System.Collections.Generic;
using System.Data.Common;
using System;

namespace MoodProjet.db
{
    public record MoodFace(int Id, string Key, string Picture, string Label, bool IsActive);

    public record MoodEntry(int Id, int MoodFaceId, DateTime Date, int MoodDeviceId, MoodFace moodFace = null, Device device = null);

    public record Device(int Id, string Label, bool IsActive);
}
