using MoodProjet.Devices;
using MoodProjet.MoodFaces;
using System;

namespace MoodProjet.MoodEntries
{
	public record MoodEntry(int Id, int MoodFaceId, DateTime Date, int MoodDeviceId, MoodFace moodFace = null, Device device = null);
}
