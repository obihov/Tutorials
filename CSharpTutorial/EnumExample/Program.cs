using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DataFormatType dataFormatType = DataFormatType.Xml | DataFormatType.Csv;

            switch (dataFormatType)
            {
                case DataFormatType.Xml | DataFormatType.Json: { Console.WriteLine("Xml and Json selected."); } break;
                case DataFormatType.Xml | DataFormatType.Csv: { Console.WriteLine("Xml and Csv selected."); } break;
                case DataFormatType.Json | DataFormatType.Csv: { Console.WriteLine("Json and Csv selected."); } break;
                case DataFormatType.Text | DataFormatType.Html: { Console.WriteLine("Text and Html selected."); } break;
                default: { Console.WriteLine("None selected."); } break;
            }
        }
    }

    [Flags]
    public enum DataFormatType
    {
        None = 0,
        Xml = 1,
        Json = 2,
        Csv = 4,
        Text = 8,
        Html = 16
    }

    [Flags]
    public enum DayBi : byte
    {
        None = 0b0,
        Sunday = 0b1,
        Monday = 0b10,
        Tuesday = 0b100,
        Wednesday = 0b1000,
        Thursday = 0b10000,
        Friday = 0b100000,
        Saturday = 0b1000000
    }


    [Flags]
    public enum DayDec : byte
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    [Flags]
    public enum DayHex : byte
    {
        None = 0x0,
        Sunday = 0x1,
        Monday = 0x2,
        Tuesday = 0x4,
        Wednesday = 0x8,
        Thursday = 0x10,    //16
        Friday = 0x20,      //32
        Saturday = 0x40     //64
    }
}
