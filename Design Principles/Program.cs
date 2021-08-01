using System;

namespace Design_Principles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Design Principles");
            //Usecase 1: Single Responsibility Principle
            DisplayEmployeeDetail displayEmployeeDetail = new DisplayEmployeeDetail();
            displayEmployeeDetail.Display();
        }
    }
}
