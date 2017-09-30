using System;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class FinishingAvailableSpec
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public PrintTypes PrintType { get; set; }
        public int PaperWeight { get; set; }
        public FinishingAvailableBookletSpec Book { get; set; }
    }

    [Flags]
    public enum PrintTypes
    {
        Offset = 1,
        Digital = 2
    }

    public class FinishingAvailableBookletSpec
    {
        public int Pp { get; set; }
        public Orientations Orientation { get; set; }
    }
    public enum Orientations
    {
        Portrait = 0,
        Landscape = 1
    }
}
