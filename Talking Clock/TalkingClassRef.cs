using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkingClock
{
    public class TalkingClassRef
    {
        private static string hoursText = String.Empty;
        private static string minutesText = String.Empty;
        private static string spokenSpeechPart = String.Empty;

        private static readonly string[] hours = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
        private static readonly string[] minutes = new string[] { "o'clock", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
        "thirteen", "fourteen", "quarter", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four",
        "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine","half" };

        public static string Time(string time)
        {
            DateTime dateTime;
            if (String.IsNullOrEmpty(time)) { time = DateTime.Now.ToString("HH:mm"); }
            if (CorrectFormat(time, out dateTime))
            {
                if (MoreThanHalfPast(dateTime.Minute)) { dateTime = dateTime.AddHours(1); }
                hoursText = GetSpokenHours(dateTime.Hour);
                minutesText = GetSpokenMinutes(dateTime.Minute);
                spokenSpeechPart = GetSpokenSpeechPart(dateTime);
                return CapitaliseFirstLetter(GetSpokenTime(dateTime));
            }
            else
            {
                throw new FormatException($"Incorrect format in {dateTime}.");
            }
        }
        private static bool CorrectFormat(string time, out DateTime dateTime)
        {
            //covering both allowed time formats
            string formatSingleHourDigit = "H:mm";  
            string formatDoubleHourDigit = "HH:mm";

            return (DateTime.TryParseExact(time, formatSingleHourDigit, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)
                   || DateTime.TryParseExact(time, formatDoubleHourDigit, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime));
        }
        private static string GetSpokenHours(int hourNumber)
        {
            if (hourNumber > 12) { hourNumber -= 12; }   
            else if (hourNumber == 0) { hourNumber = 12; }
            for (int i = 0; i < hours.Length; i++)
            {
                if (hourNumber - 1 == i)
                    return hours[i];
            }

            throw new Exception("Spoken hours not found");
        }
        private static bool MoreThanHalfPast(int minuteNumber)
        {
            if (minuteNumber > 30) { return true; }
            else { return false; }
        }
        private static string GetSpokenMinutes(int minuteNumber)
        {
            if (minuteNumber > 30) { minuteNumber = 60 - minuteNumber; } 
            for (int i = 0; i < minutes.Length; i++)
            {
                if (minuteNumber == i)
                    return minutes[i];
            }
            throw new Exception("Spoken minutes not found");
        }
        private static string GetSpokenSpeechPart(DateTime dateTime)
        {
            string spokenSpeechPart = String.Empty;
            if (dateTime.Minute == 00) { spokenSpeechPart = "o'clock"; }
            else if (dateTime.Minute > 00 && dateTime.Minute <= 30) { spokenSpeechPart = "past"; }
            else if (dateTime.Minute > 30) { spokenSpeechPart = "to"; }
            return spokenSpeechPart;
        }

        private static string GetSpokenTime(DateTime dateTime)
        {
            string spokenTime = String.Empty;

            if (spokenSpeechPart == "o'clock") { spokenTime = $"{hoursText} {spokenSpeechPart}"; }
            else if (spokenSpeechPart == "past") { spokenTime = $"{minutesText} {spokenSpeechPart} {hoursText}"; }
            else if (spokenSpeechPart == "to") { spokenTime = $"{minutesText} {spokenSpeechPart} {hoursText}"; }
            return spokenTime;
        }
        private static string CapitaliseFirstLetter(string text)
        {
            if (String.IsNullOrEmpty(text)) throw new ArgumentException();
            return text.First().ToString().ToUpper() + text.Substring(1);
        }

    }
}
