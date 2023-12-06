using System;
using System.Collections.Generic;
using System.Linq;

namespace AkelonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var VacationDictionary = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new List<DateTime>(),
                ["Петров Петр Петрович"] = new List<DateTime>(),
                ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
                ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
                ["Павлов Павел Павлович"] = new List<DateTime>(),
                ["Георгиев Георг Георгиевич"] = new List<DateTime>()
            };
            var WorkingDays = new List<String>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            // Список отпусков сотрудников
            List<DateTime> Vacations = new List<DateTime>();
            int AllVacationCount = 0;
            List<DateTime> dateList = new List<DateTime>();
            List<DateTime> SetDateList = new List<DateTime>();
            Random rnd = new Random();
            int[] vacationSteps = { 7, 14 };
            DateTime endDate = new DateTime();
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime end = new DateTime(DateTime.Today.Year, 12, 31);
            int range = (end - start).Days;
            int difference = 0;
            foreach (var VacationList in VacationDictionary)
            {

                //Random step = new Random();


                dateList = VacationList.Value;
                int vacationCount = 28;
                while (vacationCount > 0)
                {

                    var startDate = start.AddDays(rnd.Next(range));

                    if (WorkingDays.Contains(startDate.DayOfWeek.ToString()))
                    {
                        if (vacationCount <= 7)
                        {
                            endDate = startDate.AddDays(7);
                            difference = 7;
                        }
                        else
                        {
                            ///*string[]*/ int[] vacationSteps = { 7, 14 };
                            int vacIndex = rnd.Next(vacationSteps.Length);

                            //var endDate = new DateTime(DateTime.Now.Year, 12, 31);
                            //difference = 0;
                            /*if (vacationSteps[vacIndex] == "7")
                            {*/
                            endDate = startDate.AddDays(/*7*/ vacationSteps[vacIndex]);
                            difference = vacationSteps[vacIndex];
                            //}
                            /*if (vacationSteps[vacIndex] == "14")
                            {
                                endDate = startDate.AddDays(14);
                                difference = 14;
                            }*/
                        }


                        // Проверка условий по отпуску
                        bool CanCreateVacation = false;
                        bool existStart = false;
                        bool existEnd = false;
                        if (!Vacations.Any(element => element >= startDate && element <= endDate))
                        {
                            if (!Vacations.Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate))
                            {
                                existStart = dateList.Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
                                existEnd = dateList.Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
                                if (!existStart || !existEnd)
                                    CanCreateVacation = true;
                            }
                        }

                        if (CanCreateVacation)
                        {
                            for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                Vacations.Add(dt);
                                dateList.Add(dt);
                            }
                            AllVacationCount++;
                            vacationCount -= difference;
                        }
                    }
                }
            }
            foreach (var VacationList in VacationDictionary)
            {
                SetDateList = VacationList.Value;
                Console.WriteLine("Дни отпуска " + VacationList.Key + " : ");
                //for (int i = 0; i < SetDateList.Count; i++) { Console.WriteLine(SetDateList[i]); } 
                SetDateList.ForEach(x => Console.WriteLine(x));
            }
            Console.ReadKey();
        }
    }
}
