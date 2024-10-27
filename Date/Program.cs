using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Входные данные
        string[] startTimes = { "10:00", "11:00", "15:00", "15:30", "16:50" };
        int[] durations = { 60, 30, 10, 10, 40 }; 
        string beginWorkingTime = "08:00";
        string endWorkingTime = "18:00";
        int consultationTime = 30; 

        // Вызов функции для поиска свободных промежутков
        List<string> freeSlots = FindFreeTimeSlots(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

        
        Console.WriteLine("Свободные временные промежутки:");
        foreach (string slot in freeSlots)
        {
            Console.WriteLine(slot);
        }
    }

    public static List<string> FindFreeTimeSlots(string[] startTimes, int[] durations, string beginWorkingTime, string endWorkingTime, int consultationTime)
    {
        TimeSpan workStart = TimeSpan.Parse(beginWorkingTime);
        TimeSpan workEnd = TimeSpan.Parse(endWorkingTime);
        TimeSpan consultationDuration = TimeSpan.FromMinutes(consultationTime);

        List<string> freeSlots = new List<string>();
        TimeSpan current = workStart;

        // Основной цикл для проверки каждого занятого промежутка
        for (int i = 0; i < startTimes.Length; i++)
        {
            TimeSpan start = TimeSpan.Parse(startTimes[i]);
            TimeSpan end = start.Add(TimeSpan.FromMinutes(durations[i]));
            
            // Добавляем все возможные свободные промежутки по 30 минут перед занятым временем
            while (current.Add(consultationDuration) <= start)
            {
                freeSlots.Add($"{current:hh\\:mm}-{current.Add(consultationDuration):hh\\:mm}");
                current = current.Add(consultationDuration);
            }

            // Обновляем текущее время до конца текущего занятого промежутка
            if (current < end)
            {
                current = end;
            }
        }
        // Проверка на оставшийся свободный промежуток после последнего занятого интервала до конца рабочего дня
        while (current.Add(consultationDuration) <= workEnd)
        {
            freeSlots.Add($"{current:hh\\:mm}-{current.Add(consultationDuration):hh\\:mm}");
            current = current.Add(consultationDuration);
        }
        return freeSlots;
    }
}
