using System;
using FileHelpers;

namespace Task4_FolderMonitor.BL.Utils
{
    [DelimitedRecord(",")]
    public class SaleRecord
    {
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime Date;
        public string ClientFullName;
        public string GoodsName;
        public int Cost;
    }
}