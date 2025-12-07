using System.Data;

namespace ADOFromConsole.Helper
{
    public static  class DataRecordExtension
    {
        public static string GetFormattedDate(this IDataRecord record, string fieldName, string format="dd/MM/yyyy",string defaultValue = "N/A")
        {
            var ordinal = record.GetOrdinal(fieldName);
            
            if(record.IsDBNull(ordinal)) return defaultValue;

            DateTime date = record.GetDateTime(ordinal);
            return date.ToString(format);

        }
    }
}
