namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfSubjects = 5;
            int[] scores = new int[numberOfSubjects];
            int totalScore = 0;

            for (int i = 0; i < numberOfSubjects; i++)
            {
                Console.Write($"Enter score for subject {i + 1}: ");
                scores[i] = int.Parse(Console.ReadLine());
                totalScore += scores[i];
            }

            double averageScore = (double)totalScore / numberOfSubjects;
            Console.WriteLine($"\nAverage Score: {averageScore}");

            char grade;
            if (averageScore >= 90)
                grade = 'A';
            else if (averageScore >= 80)
                grade = 'B';
            else if (averageScore >= 70)
                grade = 'C';
            else if (averageScore >= 60)
                grade = 'D';
            else
                grade = 'F';

            Console.WriteLine($"Assigned Grade: {grade}");

            Console.WriteLine("\nFailing Marks:");
            bool hasFailingMarks = false;
            for (int i = 0; i < numberOfSubjects; i++)
            {
                if (scores[i] < 40) 
                {
                    Console.WriteLine($"Subject {i + 1}: {scores[i]}");
                    hasFailingMarks = true;
                }
            }

            if (!hasFailingMarks)
            {
                Console.WriteLine("None");
            }
        }
    }
}