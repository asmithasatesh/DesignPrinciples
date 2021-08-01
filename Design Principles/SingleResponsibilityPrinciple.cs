using System;
using System.Collections.Generic;
using System.Text;

namespace Design_Principles
{

    public class SingleResponsibilityPrinciple
    {
        public static List<SingleResponsibilityPrinciple> list = new List<SingleResponsibilityPrinciple>();
        public  string employeeName { get; set; }
        public int age { get; set; }
        public  decimal basicPay { get; set; }
        public  decimal Deduction { get; set; }
        public SingleResponsibilityPrinciple(string employeeName, int age, decimal basicPay, decimal Deduction)
        {
            this.employeeName = employeeName;
            this.age = age;
            this.basicPay = basicPay;
            this.Deduction = Deduction;
        }
        public static List<SingleResponsibilityPrinciple> GetInput()
        {
            Console.WriteLine("Enter the number of Employees");
            int n= Convert.ToInt32(Console.ReadLine());
            while(n--!=0)
            {
                //Employee Salary Computation
                Console.WriteLine("\nEnter the EmployeeName:");
                string employeeName = Console.ReadLine();
                Console.WriteLine("Enter the Age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Basic Pay:");
                decimal basicPay = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Enter the Deduction:");
                decimal Deduction = Convert.ToDecimal(Console.ReadLine());
                SingleResponsibilityPrinciple singleResponsibilityPrinciple = new SingleResponsibilityPrinciple(employeeName, age, basicPay, Deduction);
                list.Add(singleResponsibilityPrinciple);
            }
            return list;
        }
    }

    public class DisplayEmployeeDetail
    {
        public void Display()
        {
            List<SingleResponsibilityPrinciple> employeeDetails = SingleResponsibilityPrinciple.GetInput();
            Console.WriteLine("\n--------------------- Display Employee Details ---------------------");
            foreach (var i in employeeDetails)
            {
                Console.WriteLine("\nEmployeeName: "+i.employeeName);
                Console.WriteLine("Enter the Age: "+i.age);
                Console.WriteLine("Enter the Basic Pay: "+i.basicPay);
                Console.WriteLine("Enter the Deduction: "+i.Deduction);
                decimal netPay = CalculateFunction.CalculateNetPay(i);
                Console.WriteLine("Enter the NetPay: " + netPay);
            }

        }
    }
    public class CalculateFunction
    {
        public static decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails)
        {
            decimal netPay = employeeDetails.basicPay - employeeDetails.Deduction;
            return netPay;
        }
    }

}
