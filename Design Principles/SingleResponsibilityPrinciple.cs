using System;
using System.Collections.Generic;
using System.Text;

namespace Design_Principles
{
    //Interface Segregation Principle
    interface ITaxCalculator
    {
        public decimal CalculateIncomeTax(decimal basicPay);
    }
    interface ISpecialAllowances
    {
        public decimal TravelandFoodAllowance(string employeeType);
    }
    public class SingleResponsibilityPrinciple
    {
        public static List<SingleResponsibilityPrinciple> list = new List<SingleResponsibilityPrinciple>();
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Deduction { get; set; }
        public string EmployeeType { get; set; }
        public SingleResponsibilityPrinciple(string employeeName, int age, decimal basicPay, decimal Deduction, string employeeType)
        {
            this.EmployeeName = employeeName;
            this.Age = age;
            this.BasicPay = basicPay;
            this.Deduction = Deduction;
            this.EmployeeType = employeeType;
        }
        //Method to Get Employee Details
        public static List<SingleResponsibilityPrinciple> GetInput()
        {
            Console.WriteLine("Enter the number of Employees");
            int n = Convert.ToInt32(Console.ReadLine());
            while (n-- != 0)
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
                Console.WriteLine("Enter the Employee Type:");
                string employeeType = Console.ReadLine();
                SingleResponsibilityPrinciple singleResponsibilityPrinciple = new SingleResponsibilityPrinciple(employeeName, age, basicPay, Deduction, employeeType);
                list.Add(singleResponsibilityPrinciple);
            }
            return list;
        }
    }

    public class DisplayEmployeeDetail
    {
        //Display Employee Details
        public void Display()
        {
            List<SingleResponsibilityPrinciple> employeeDetails = SingleResponsibilityPrinciple.GetInput();
            decimal netPay = 0;
            Console.WriteLine("\n--------------------- Display Employee Details ---------------------");
            foreach (var i in employeeDetails)
            {
                Console.WriteLine("\nEmployeeName: " + i.EmployeeName);
                Console.WriteLine("Enter the Age: " + i.Age);
                Console.WriteLine("Enter the Basic Pay: " + i.BasicPay);
                Console.WriteLine("Enter the Deduction: " + i.Deduction);
                Console.WriteLine("Enter the Employee Type: " + i.EmployeeType);
                //Liskov Substitution Principle
                if (i.EmployeeType == "FullTime")
                {
                    CalculateFunction fullTimeEmployee = new FullTimeEmployee();
                    netPay = fullTimeEmployee.CalculateNetPay(i);
                }
                else
                {
                    FullTimeEmployee partTimeEmployee = new PartTimeEmployee();
                    netPay = partTimeEmployee.CalculateNetPay(i);
                }

                Console.WriteLine("Enter the NetPay: " + netPay);
            }

        }
    }
    //Interface Segregation Principle
    public class TaxCalculate: ITaxCalculator
    {
        public  decimal CalculateIncomeTax(decimal basicPay)
        {
            if(basicPay< 5000000)
            {
                return 0;
            }
            decimal incomeTax = (basicPay * 5 )/ 100 ;
            return incomeTax;
        }
    }
    //Open Closed Principle
    public abstract class CalculateFunction
    {
        //Calculate NetPay
        public abstract decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails);
    }
    //Calculate Netpay for fulltime Employee
    public class FullTimeEmployee : CalculateFunction,ISpecialAllowances
    {
        TaxCalculate TaxCalculate = new TaxCalculate();
        public decimal TravelandFoodAllowance(string employeeType)
        {
            decimal allowances = 0;
            if(employeeType== "FullTime")
            {
                allowances = 1000;
            }
            return allowances;
        }
        public override decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails)
        {
            decimal netPay = employeeDetails.BasicPay - employeeDetails.Deduction + TravelandFoodAllowance(employeeDetails.EmployeeType) - TaxCalculate.CalculateIncomeTax(employeeDetails.BasicPay);
            return netPay;
        }

    }
    //Calculate Netpay for Parttime Employee
    public class PartTimeEmployee : FullTimeEmployee
    {
        TaxCalculate TaxCalculate = new TaxCalculate();
        public override decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails)
        {
            decimal netPay = employeeDetails.BasicPay - (employeeDetails.Deduction / 2) - TaxCalculate.CalculateIncomeTax(employeeDetails.BasicPay);
            return netPay;
        }
    }

}
