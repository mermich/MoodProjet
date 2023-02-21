using System;

namespace MoodProjet.Charts
{
    public class MoodByHour
    {
        public MoodByHour(int face1Count, int face2Count, int face3Count, int face4Count, int heure)
        {
            Face1Count = face1Count;
            Face2Count = face2Count;
            Face3Count = face3Count;
            Face4Count = face4Count;
            Heure = heure;
        }

        public int Face1Count { get; set; }

        public int Face2Count { get; set; }

        public int Face3Count { get; set; }

        public int Face4Count { get; set; }

        public int Heure { get; set; }
    }
}
